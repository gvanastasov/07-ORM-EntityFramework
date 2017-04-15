using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_intro
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public Person() : this("No name", 1) { }

        public Person(string name) : this(name, 1) { }

        public Person(int age) : this("No Name", age) { }


        public override string ToString()
        {
            return string.Format("{0} {1}", this.Name, this.Age);
        }
    }
}
