using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;

namespace WindowsFormsApp1
{
    class TextSerialization : ISerializer
    {
        public object Deserialize(Type objectType, string filePath)
        {
            string currObjInfoStr;
            string[] currObjMem, currObj;
            Fact currObject;
            int currObjIndex;
            Type currObjectType;
            PropertyInfo currObjProp;
            FieldInfo currObjField;
            List<Fact> newList = new List<Fact>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fs))
                {
                    while (!streamReader.EndOfStream)
                    {
                        currObjInfoStr = streamReader.ReadLine();
                        currObjMem = currObjInfoStr.Split('-');
                        currObjIndex = Form1.humanArr.FindIndex(x => x.FullName == currObjMem[0]);
                        currObject = Form1.humanFactories[currObjIndex].CreateHuman();
                        currObjectType = currObject.GetType();
                        int i = 1;
                        while (i < currObjMem.Length)
                        {
                            currObj = currObjMem[i].Split(':');
                            PropertyInfo[] currObjPropArr = currObjectType.GetProperties();
                            currObjProp = currObjectType.GetProperty(currObj[0]);
                            if (currObjProp != null)
                            {
                                object o = Convert.ChangeType(currObj[1], currObjProp.PropertyType);
                                currObjProp.SetValue(currObject, o);
                                i++;
                            }
                            else
                            {
                                FieldInfo[] currObjFieldArr = currObjectType.GetFields();
                                
                                currObjField = currObjectType.GetField(currObj[0]);

                                int index = Form1.humanArr.FindIndex(element => element == currObjField.FieldType);
                                if (index != -1)
                                {
                                    i++;
                                    Fact subObject = Form1.humanFactories[index].CreateHuman();
                                    Type subObjectType = subObject.GetType();
                                    PropertyInfo[] subObjectProperties = subObjectType.GetProperties();
                                    for (int j = 1; j <= subObjectProperties.Length; j++)
                                    {
                                        currObj = currObjMem[i].Split(':');
                                        currObjProp = subObjectType.GetProperty(currObj[0]);
                                        object o = Convert.ChangeType(currObj[1], currObjProp.PropertyType);
                                        currObjProp.SetValue(subObject, o);
                                        i++;
                                    }
                                    currObjField.SetValue(currObject, subObject);
                                }
                            }
                        }
                        newList.Add(currObject);
                    }
                }
            }
            return newList;
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        private string Tostring(Object str)
        {
            string[] words = str.ToString().Split(new char[] { ' ' });          
            return words[1];
        }

        public void Serialize(List<Fact> value, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                foreach (Fact obj in value)
                {
                    Type newObjectType = obj.GetType();
                    AddText(fs, System.Convert.ToString(newObjectType));
                    FieldInfo[] NewObjectFields= newObjectType.GetFields();
                    PropertyInfo[] NewObjectProperties = newObjectType.GetProperties();
                    foreach (PropertyInfo item in NewObjectProperties)
                    {
                        AddText(fs, "-"+Tostring(item) + ":" + item.GetValue(obj).ToString());
                    }
                    foreach (FieldInfo item in NewObjectFields)
                    {
                        int index = Form1.humanArr.FindIndex(element => element == item.FieldType);
                        if (index != -1)
                        {
                            Fact subObject = (Fact)item.GetValue(obj);
                            Type subObjectType = subObject.GetType();
                            AddText(fs, "-"+item.Name);
                            PropertyInfo[] subObjectProperties = subObjectType.GetProperties();

                            foreach (PropertyInfo subItem in subObjectProperties)
                            {
                                AddText(fs, "-"+Tostring(subItem) + ":" + subItem.GetValue(subObject).ToString());
                            }
                        }
                        else
                        {
                            AddText(fs, Tostring(item) + ":" + item.GetValue(obj).ToString() + " ");
                        }
                    }
                    AddText(fs, "\r\n");
                }
            }
        }
    }   
}
