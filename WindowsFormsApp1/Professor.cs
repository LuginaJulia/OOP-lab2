﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Professor : Human
    {
        public int pubs { get; set; }
        public Professor()
        {
        }
    }
}
