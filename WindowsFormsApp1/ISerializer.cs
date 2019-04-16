using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface ISerializer
    {
        void Serialize(List<Fact> value, string path);
        object Deserialize(Type value, string path);
    }
}
