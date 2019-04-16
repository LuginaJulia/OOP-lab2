using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApp1
{
    [XmlInclude(typeof(Human))]
    [XmlInclude(typeof(Teacher))]
    [XmlInclude(typeof(Student))]
    [XmlInclude(typeof(Professor))]
    [XmlInclude(typeof(Group))]
    class XMLSerial
    {
        private XmlSerializer formatter = new XmlSerializer(typeof(List<Fact>));
        public void XML_Serialize(string filename, List<Fact> objects)
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(List<Fact>));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, objects);
                   // MessageBox.Show("Успешно");
                }
                catch
                {
                   // MessageBox.Show("Ошибка");
                }
            }
        }
        XMLSerial()
        {
        }
    }
}
