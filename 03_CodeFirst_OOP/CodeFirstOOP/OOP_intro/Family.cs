using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_intro
{
    public class Family
    {
        public List<Person> people = new List<Person>();

        public void AddMember(Person member)
        {
            people.Add(member);
        }

        public Person GetOldestMember()
        {
            return people.OrderBy(p => p.Age).Last();
        }
    }
}
