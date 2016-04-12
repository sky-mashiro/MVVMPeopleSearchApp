using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PeopleSearchApp.Model
{
    class PeopleContext: DbContext
    {
        public PeopleContext(): base("PeopleDatabase")
        {
            Database.SetInitializer<PeopleContext>(new PeopleInitializer());
        }
        public DbSet<Person> People { get; set; }
    }
}
