using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    class XMLSerialization : ISerializer
    {
        public void Serialize(List<Fact> value, string filePath)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Fact>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, value);
            }
        }

        public object Deserialize(Type t, string filePath)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Fact>));
            List<Fact> newList;
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                newList = (List<Fact>)formatter.Deserialize(fs);
            }
            return newList;
        }
    }
}
