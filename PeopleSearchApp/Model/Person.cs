using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PeopleSearchApp.Model
{
    class Person
    {
        [Key]
        public Int32 ID { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        public Address address { get; set; }

        public string age { get; set; }

        public string interest { get; set; }

        public byte[] photo { get; set; }

        public Person()
        {
            address = new Address();
        }
    }
}
