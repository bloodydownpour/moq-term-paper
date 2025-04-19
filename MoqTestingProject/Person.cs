using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqTestingProject
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Person() { }

        public Person(int id, string name, string surname)
        {
            this.PersonId = id;
            this.Name = name;
            this.Surname = surname;
        }

        public override string ToString()
        {
            return PersonId.ToString() + ": " + Name + " " + Surname;
        }

    }
}
