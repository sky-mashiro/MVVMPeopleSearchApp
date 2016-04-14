using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;

using PeopleSearchApp.Model;
using PeopleSearchApp.Model.DataAccess;
using PeopleSearchApp.Command;
using PeopleSearchApp.ViewModel.ValidationRules;
using MahApps.Metro.Controls.Dialogs;

namespace PeopleSearchApp.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        //Metro style dialog
        public DialogCoordinator _dia;

        private ICommand _generalCommand;

        public ICommand SearchCommand
        {
            get { return _generalCommand = new RelayCommand(SearchExecute, SearchCanExecute); }
        }

        public ICommand AddCommand
        {
            get { return _generalCommand = new RelayCommand(AddExecute, AddCanExecute); }
        }

        public ICommand BrowseCommand
        {
            get { return _generalCommand = new RelayCommand(BrowseExecute, BrowseCanExecute); }
        }

        private PeopleRepository peopleRepo;

        //The collection of all people in database, binding to view
        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get
            {
                return _people;
            }
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        //The new person who will be added, some of its value is binding to view
        private Person _newPerson;
        public Person NewPerson
        {
            get
            {
                return _newPerson;
            }
            set
            {
                _newPerson = value;
                OnPropertyChanged();
            }
        }

        public USStates stateName { get; }

        //ID binding to view
        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        //Key word for search, binding to view
        public string keyWord { get; set; }

        //Image path for new person's photo, binding to view
        private string _imagePath;
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        //Constructor
        public MainWindowViewModel(DialogCoordinator dia)
        {
            //Initiate values
            _dia = dia;//For metro style dialog
            peopleRepo = new PeopleRepository();
            People = peopleRepo.GetAllRecord();
            NewPerson = new Person();

            ImagePath = "......";
            keyWord = "";

            //Use in the drop down list
            stateName = new USStates();
        }

        //Created specificly for Unit test(Since we need mock to test entity framwork)
        public MainWindowViewModel(DialogCoordinator dia, PeopleRepository repo)
        {
            //var context = new PeopleContext();
            _dia = dia;
            peopleRepo = repo;
            People = repo.GetAllRecord();

            NewPerson = new Person();

            ImagePath = "......";
            keyWord = "";

            stateName = new USStates();
        }

        public void SearchExecute(object param)
        {
            //Select in repo
            People = peopleRepo.SelectName(keyWord);
        }

        //Search always can execute
        public bool SearchCanExecute(object param)
        {
            return true;
        }

        public void AddExecute(object param)
        {
            //Get id
            NewPerson.ID = Int32.Parse(ID);

            NewPerson.lastName = NewPerson.lastName.Trim();
            NewPerson.firstName = NewPerson.firstName.Trim();

            //let repo talk to database, of course in code first manner
            peopleRepo.AddRecord(NewPerson, ImagePath);

            //After add, reset all related values
            ImagePath = "......";
            ID = "";
            NewPerson = new Person();

            //Show success message
            _dia.ShowMessageAsync(this, "Notification", "Record successfully added~");
            //System.Windows.Forms.MessageBox.Show("Successful Added~!");

            //Update people
            People = peopleRepo.GetAllRecord();

        }

        //check if all value is valid for add
        public bool AddCanExecute(object param)
        {
            //Check id
            if (new IDRules().Validate(ID, null).IsValid)
            {
                //check first name
                if (new FirstNameRules().Validate(NewPerson.firstName, null).IsValid)
                {
                    //check last name
                    if (new LastNameRules().Validate(NewPerson.lastName, null).IsValid)
                    {
                        //check age
                        if (new AgeRangeRule().Validate(NewPerson.age, null).IsValid)
                        {
                            //check zip
                            if (new ZipRules().Validate(NewPerson.address.zip, null).IsValid)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            //if any of above did not pass, return false
            return false;
        }

        //Created for unit test, since mock model needed here
        public bool AddCanExecute()
        {
            if (new IDRules().Validate(ID, null, peopleRepo).IsValid)
            {
                if (new FirstNameRules().Validate(NewPerson.firstName, null).IsValid)
                {
                    if (new LastNameRules().Validate(NewPerson.lastName, null).IsValid)
                    {
                        if (new AgeRangeRule().Validate(NewPerson.age, null).IsValid)
                        {
                            if (new ZipRules().Validate(NewPerson.address.zip, null).IsValid)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;

            //return new AgeRangeRule().Validate(NewPerson.age);
            //else return !Validation.GetHasError(param as DependencyObject);
        }

        //Browse to add a image
        public void BrowseExecute(object param)
        {
            //Open a dialog to add a iamge path
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.gif, *.png) | *.jpg; *.jpeg; *.jpe; *.gif; *.png";

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                //Check the image size
                ImagePath = imageDialog.FileName;
                var fileSize= new FileInfo(ImagePath).Length>>20;
                if (fileSize >= 2)
                {
                    ImagePath = "......";
                    _dia.ShowMessageAsync(this, "Notification", "Image cannot be bigger than 2MB!" + Environment.NewLine + "Please choose a new one^_^");
                    //System.Windows.Forms.MessageBox.Show("Image cannot be bigger than 2MB!" + Environment.NewLine + "Please choose a new one^_^");
                }
            }
        }
        public bool BrowseCanExecute(object param)
        {
            return true;
        }
    }
}
