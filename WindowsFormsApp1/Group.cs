using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Group : Fact
    {
        public int number { get; set; }
        public Teacher curator;
        public Student starosta;
        public Group()
        {
        }
    }
}
