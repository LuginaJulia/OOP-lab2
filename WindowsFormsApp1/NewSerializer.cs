using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    class NewSerializer
    {
        public ISerializer GetfileName_Serialization(string fileName)
        {
            while (true)
            {
                string fileExt = Path.GetExtension(fileName).TrimStart('.');
                if (fileExt.StartsWith("bin"))
                    return new BinarySerialization();
                if (fileExt.StartsWith("xml"))
                    return new XMLSerialization();
                if (fileExt.StartsWith("txt"))
                    return new TextSerialization();
                fileName = fileName.TrimEnd(("." + fileExt).ToCharArray());
            }
        }
    }
}
