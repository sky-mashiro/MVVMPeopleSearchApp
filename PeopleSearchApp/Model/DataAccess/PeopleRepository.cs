using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace PeopleSearchApp.Model.DataAccess
{

    //Wrap with add, select functions, so viewmodel can call these functions from here
    public class PeopleRepository
    {
        private PeopleContext context;


        public PeopleRepository()
        {
            context = new PeopleContext();
        }

        //This constructor is for unit test only
        public PeopleRepository(PeopleContext _context)
        {
            context = _context;
        }

        //Select method
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

        //Add a record
        public void AddRecord(Person someOne, string imagePath)
        {
            byte[] potrait = null;
            //Check if there is a valid image path
            if (!string.IsNullOrEmpty(imagePath) && !imagePath.Equals("......"))
            {
                ImageByteArray converter = new ImageByteArray();
                potrait = converter.ImagePathToByteArray(imagePath);
            }

            someOne.photo = potrait;

            context.People.Add(someOne);
            context.SaveChanges();
            //return new ObservableCollection<Person>(resultFirstName.Union(resultLastName));
        }

        //Select *
        public ObservableCollection<Person> GetAllRecord()
        {
            return new ObservableCollection<Person>(context.People);
        }

        //Select all ID for duplicate check
        public List<int> GetAllID()
        {
            List<int> idList = new List<int>();

            idList = (from person in context.People
                     select person.ID).ToList();

            return idList;
        }
    }
}
