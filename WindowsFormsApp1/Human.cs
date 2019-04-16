using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    [Serializable]
    [XmlInclude(typeof(Human))]
    [XmlInclude(typeof(Teacher))]
    [XmlInclude(typeof(Student))]
    [XmlInclude(typeof(Professor))]
    [XmlInclude(typeof(Group))]
    public abstract class Fact
    {

    }
    [Serializable]
    public class Human : Fact
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Human()
        {
        }
    }
}
