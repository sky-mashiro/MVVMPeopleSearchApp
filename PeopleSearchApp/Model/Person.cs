using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PeopleSearchApp.Model
{
    class Person
    {
        public int ID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public Address address { get; set; }

        public int age { get; set; }

        public string interest { get; set; }

        public byte[] photo { get; set; }
    }
}
