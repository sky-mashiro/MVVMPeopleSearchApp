using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PeopleSearchApp.Model
{
    public class PeopleContext: DbContext
    {
        public PeopleContext(): base("PeopleDatabase")
        {
            Database.SetInitializer<PeopleContext>(new PeopleInitializer());
        }

        //Virstual is for unit test mock
        public virtual DbSet<Person> People { get; set; }
    }
}
