using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace PeopleSearchApp.Model.DataAccess
{
    class PeopleRepository
    {
        private PeopleContext context = new PeopleContext();

        public ObservableCollection<Person> SelectName(string keyWord)
        {
            var resultFirstName = from b in context.People
                         where b.firstName.Contains(keyWord)
                         select b;

            var resultLastName = from b in context.People
                                 where b.lastName.Contains(keyWord)
                                 select b;

            return new ObservableCollection<Person>(resultFirstName.Union(resultLastName));
        }

        public void AddRecord(Person someOne, string imagePath)
        {
            byte[] potrait = null;
            if (!imagePath.Equals("......"))
            {
                ImageByteArray converter = new ImageByteArray();
                potrait = converter.ImagePathToByteArray(imagePath);
            }

            Person newPerson = new Person
            {
                ID = someOne.ID,
                firstName = someOne.firstName,
                lastName = someOne.lastName,
                age = someOne.age,
                address = new Address
                {
                    street = "fasdf",
                    city = "fasdf",
                    state = "Az",
                    zip = "85281"
                },
                //address = new Address
                //{
                //    street = someOne.address.street,
                //    city = someOne.address.city,
                //    state = someOne.address.state,
                //    zip = someOne.address.zip
                //},
                interest = "Football",
                photo = potrait
            };
            context.People.Add(newPerson);
            context.SaveChanges();
            //return new ObservableCollection<Person>(resultFirstName.Union(resultLastName));
        }

        public ObservableCollection<Person> GetAllRecord()
        {
            return new ObservableCollection<Person>(context.People);
        }
    }
}
