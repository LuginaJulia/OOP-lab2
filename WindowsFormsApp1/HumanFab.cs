using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    abstract class FactCreator
    {
        abstract public Fact CreateHuman();
    }
    class HumanFab : FactCreator
    {
        override public Fact CreateHuman()
        {
            return new Human();
        }
    }
    class TeacherFab : HumanFab
    {
        override public Fact CreateHuman()
        {
            return new Teacher();
        }
    }
    class StudentFab : HumanFab
    {
        override public Fact CreateHuman()
        {
            return new Student();
        }
    }
    class ProfessorFab : HumanFab
    {
        override public Fact CreateHuman()
        {
            return new Professor();
        }
    }
    class GroupFab : FactCreator
    {
        override public Fact CreateHuman()
        {
            return new Group();
        }
    }
}
