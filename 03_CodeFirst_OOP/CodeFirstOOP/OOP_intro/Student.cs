using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_intro
{
    public class Student
    {
        public string Name { get; set; }

        public static int Count;

        public Student()
        {
            Count++;
        }
    }
}
