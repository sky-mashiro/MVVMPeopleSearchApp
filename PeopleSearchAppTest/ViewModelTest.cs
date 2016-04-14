using System;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using PeopleSearchApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PeopleSearchAppTest
{
    [TestClass]
    public class ViewModelTest
    {
        private MainWindowViewModel viewModel;

        [TestInitialize]
        public void setUp()
        {
            viewModel = new MainWindowViewModel(DialogCoordinator.Instance);
        }

        [TestMethod]
        public void TestAddWithValidValue()
        {
            //Record the People set size befor add
            int originalSize = viewModel.People.Count;
            //Initialize test value
            viewModel.ID = "1000";
            viewModel.NewPerson.firstName = "Unit";
            viewModel.NewPerson.lastName = "Test";
            viewModel.NewPerson.age = "20";

            //Check if the can excute method works well
            Assert.Equals(true, viewModel.AddCommand.CanExecute(null));
            //Excute add operation
            viewModel.AddCommand.Execute(null);

            //Set search keyword to see if record is really added
            viewModel.keyWord = "Unit";

            //Check if search can be executed
            Assert.Equals(true, viewModel.SearchCommand.CanExecute(null));
            //Execute
            viewModel.SearchCommand.Execute(null);

            //After Execution, People set should have been changed
            Assert.Equals(originalSize + 1, viewModel.People.Count);
        }
    }
}
