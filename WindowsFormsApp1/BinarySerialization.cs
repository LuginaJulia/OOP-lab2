using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApp1
{
    class BinarySerialization : ISerializer
    {
        public void Serialize(List<Fact> value, string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, value);
            }
        }

        public object Deserialize(Type t, string filePath)
        {
            List<Fact> newList;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                newList = (List<Fact>)formatter.Deserialize(fs);
            }
            return newList;
        }       
    }
}
