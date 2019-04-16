using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Student : Human
    {
        public string number { get; set; }
        public float gpa { get; set; }
        public Student()
        {
        }
    }
}
