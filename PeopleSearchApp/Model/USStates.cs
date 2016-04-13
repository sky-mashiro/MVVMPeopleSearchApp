using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearchApp.Model
{
    class USStates
    {
        public List<string> states { get; }

        public USStates()
        {
            states = new List<string>
            {
                "AZ",
                "LA"
            };
        }
    }
}
