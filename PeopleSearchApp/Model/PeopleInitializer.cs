using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace PeopleSearchApp.Model
{
    class PeopleInitializer: DropCreateDatabaseAlways<PeopleContext>
    //DropCreateDatabaseIfModelChanges<PeopleContext>
    //DropCreateDatabaseAlways<PeopleContext>
    {
        //Override the seed method to Seed data into database
        protected override void Seed(PeopleContext context)
        {
            ImageByteArray converter = new ImageByteArray();

            byte[] coderPhoto = converter.ImagePathToByteArray(Path.Combine(Environment.CurrentDirectory, @"resources\coder1.jpg"));

            Person coder = new Person
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
                photo = coderPhoto
            };
            
            coderPhoto = converter.ImagePathToByteArray(Path.Combine(Environment.CurrentDirectory, @"resources\coder2.jpg"));

            Person coder2 = new Person
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
                photo = coderPhoto
            };

            coderPhoto = converter.ImagePathToByteArray(Path.Combine(Environment.CurrentDirectory, @"resources\coder3.jpg"));

            Person coder3 = new Person
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
                photo = coderPhoto
            };

            coderPhoto = converter.ImagePathToByteArray(Path.Combine(Environment.CurrentDirectory, @"resources\coder4.jpg"));

            Person coder4 = new Person
            {
                ID = 4,
                firstName = "Xiaoyu",
                lastName = "Zhang",
                address = new Address
                {
                    street = "1300 Mill Ave",
                    city = "Mesa",
                    state = "AZ",
                    zip = "85202"
                },
                age = "23",
                interest = "Code",
                photo = coderPhoto
            };

            context.People.Add(coder);
            context.People.Add(coder2);
            context.People.Add(coder3);
            context.People.Add(coder4);

            //context.SaveChanges();

            base.Seed(context);
        }
    }
}
