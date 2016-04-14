using System;
using System.Text;
using System.Collections.Generic;
using PeopleSearchApp.Model;
using PeopleSearchApp.Model.DataAccess;
using Moq;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PeopleSearchAppTest
{
    /// <summary>
    /// Summary description for ModelTest
    /// </summary>
    [TestClass]
    public class ModelTest
    {
        //Test if repo is successfully constructed with seed data
        [TestMethod]
        public void TestIfRepoAddedSeedData()
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

            //Get everyone's id in the repo
            var people = repo.GetAllID();

            //Test
            Assert.AreEqual(3, people.Count);
            Assert.AreEqual(1, people[0]);
            Assert.AreEqual(2, people[1]);
            Assert.AreEqual(3, people[2]);

            //Get All record in the repo
            var people2 = repo.GetAllRecord();

            //Test
            Assert.AreEqual(3, people2.Count);
            Assert.AreEqual("Zhou", people2[0].lastName);
            Assert.AreEqual("Li", people2[1].lastName);
            Assert.AreEqual("Yi", people2[2].lastName);

        }

        [TestMethod]
        public void TestSelectRecord()
        {
            //Get seed data to run furthur test
            var seed = SeedData().AsQueryable();

            //Choose the record you want to select
            var expectd = seed.ElementAt(0);

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

            //Call select method
            var result = repo.SelectName(expectd.firstName);

            //Check if we get a result
            Assert.IsNotNull(result);

            //Check if we get the right record
            Assert.AreEqual(expectd.ID, result[0].ID);

        }


        [TestMethod]
        public void TestAddRecord()
        {
            //Create a record to be added
            var newPerson = new Person
            {
                ID = 10,
                firstName = "John",
                lastName = "Nash",
                address = new Address
                {
                    street = "Some street",
                    city = "some city",
                    state = "LA",
                    zip = "85281"
                },
                age = "87",
                interest = "Math",
                photo = null
            };

            //Create mock People set
            var mockPeople = new Mock<DbSet<Person>>();

            //Create mock context
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(m => m.People).Returns(mockPeople.Object);

            //Create repo
            var repo = new PeopleRepository(mockContext.Object);

            //Add the record
            repo.AddRecord(newPerson, null);

            //Check if add is called once and if the right record is added
            mockPeople.Verify(m => m.Add(It.Is<Person>(p => p.lastName == "Nash" && p.ID == 10)), Times.Once);

            //Check if save change method is called
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

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
