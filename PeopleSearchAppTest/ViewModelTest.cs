using System;
using System.Windows.Input;
using System.Collections.Generic;
using PeopleSearchApp.Model.DataAccess;
using Moq;
using System.Data.Entity;
using System.Linq;

using MahApps.Metro.Controls.Dialogs;
using PeopleSearchApp.ViewModel;
using PeopleSearchApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PeopleSearchAppTest
{
    [TestClass]
    public class ViewModelTest
    {
        //View model instance to be tested
        private MainWindowViewModel viewModel;


        //Initialize view model using mock set
        [TestInitialize]
        public void setUp()
        {
            //Get seed data to run furthur test
            var seed = SeedData().AsQueryable();

            //Create mock People set
            var mockPeople = new Mock<DbSet<Person>>();

            //Set mockPeople with value
            mockPeople.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(seed.Provider);
            mockPeople.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(seed.Expression);
            mockPeople.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(seed.ElementType);
            mockPeople.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(seed.GetEnumerator());

            //Create mock context
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(c => c.People).Returns(mockPeople.Object);

            //Construct repo for test
            var repo = new PeopleRepository(mockContext.Object);
            //System.Diagnostics.Debug.WriteLine("fasjdfhakjs");
            viewModel = new MainWindowViewModel(DialogCoordinator.Instance, repo);
        }

        //Test if select command can execute and has the right result
        [TestMethod]
        public void TestSelectCommand()
        {
            //Set the keyword for search
            var seed = SeedData();

            /*!!!Seed has only 3 element, so index cannot be bigger than 2!!!!!*/
            viewModel.keyWord = seed.ElementAt(2).lastName;

            //Set the expected id
            int expectedID = seed.ElementAt(2).ID;

            //Check if can execute is enabled
            Assert.AreEqual(true, viewModel.SearchCanExecute(null));

            //Execute search
            viewModel.SearchCommand.Execute(null);

            //Check if People set is updated
            Assert.AreEqual(1, viewModel.People.Count);

            //Check if the right record is seleced
            Assert.AreEqual(expectedID, viewModel.People[0].ID);
        }

        //Check if AddCanExecute works in different condition
        [TestMethod]
        public void TestAddCanExecute()
        {

            //Should initially be false
            Assert.AreEqual(false, viewModel.AddCanExecute(null));
            
            //Set all attribute to valid
            viewModel.ID = "100";
            viewModel.NewPerson.firstName = "first";
            viewModel.NewPerson.lastName = "last";

            //Check if can execute
            Assert.AreEqual(true, viewModel.AddCanExecute());

            //Invalid age
            viewModel.NewPerson.age = "aaa";
            Assert.AreEqual(false, viewModel.AddCanExecute());

            //valid age
            viewModel.NewPerson.age = "80";
            Assert.AreEqual(true, viewModel.AddCanExecute());

            //Invalid age range
            viewModel.NewPerson.age = "200";
            Assert.AreEqual(false, viewModel.AddCanExecute());

            //Valid age and invalid zip
            viewModel.NewPerson.age = "80";
            viewModel.NewPerson.address.zip = "88";

            Assert.AreEqual(false, viewModel.AddCanExecute());

            //valid zip
            viewModel.NewPerson.address.zip = "85281";
            Assert.AreEqual(true, viewModel.AddCanExecute());

            //Invalid ID
            viewModel.ID = "1";
            Assert.AreEqual(false, viewModel.AddCanExecute());

            //Valid ID
            viewModel.ID = "121";
            Assert.AreEqual(true, viewModel.AddCanExecute());

            //Invalid first name
            viewModel.NewPerson.firstName = " ";
            Assert.AreEqual(false, viewModel.AddCanExecute());

            //Valid first name
            viewModel.NewPerson.firstName = "first";
            Assert.AreEqual(true, viewModel.AddCanExecute());

            //Invalid last name
            viewModel.NewPerson.lastName = " ";
            Assert.AreEqual(false, viewModel.AddCanExecute());

            //Valid last name
            viewModel.NewPerson.lastName = "last";
            Assert.AreEqual(true, viewModel.AddCanExecute());

        }

        [TestMethod]
        public void TestBrowseExucte()
        {
            //Test if can browse
            Assert.AreEqual(true, viewModel.BrowseCanExecute(null));

        }

        //This method used to seed data to repo to do the test
        public List<Person> SeedData()
        {
            var seedData = new List<Person>
            {
                new Person
                {
                    ID = 1,
                    firstName = "Tong",
                    lastName = "Zhou",
                    address = new Address
                    {
                        street = "987 W.Washington St",
                        city = "Tempe",
                        state = "AZ",
                        zip = "85281"
                    } ,
                    age = "22",
                    interest = "Football",
                    photo = null
                },

                new Person
                {
                    ID = 2,
                    firstName = "Chenyang",
                    lastName = "Li",
                    address = new Address
                    {
                        street = "987 W.Washington St",
                        city = "Tempe",
                        state = "AZ",
                        zip = "85281"
                    },
                    age = "23",
                    interest = "Game",
                    photo = null
                },
                new Person
                {
                    ID = 3,
                    firstName = "Weili",
                    lastName = "Yi",
                    address = new Address
                    {
                        street = "1216 E Vista Del Cerro",
                        city = "Tempe",
                        state = "AZ",
                        zip = "85281"
                    },
                    age = "23",
                    interest = "Movie",
                    photo = null
                }
            };

            return seedData;
        }
    }
}
