using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleSearchApp.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 ID { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        //Virtual is for unit test 
        public virtual Address address { get; set; }

        public string age { get; set; }

        public string interest { get; set; }

        public byte[] photo { get; set; }

        public Person()
        {
            address = new Address();
        }
    }
}
