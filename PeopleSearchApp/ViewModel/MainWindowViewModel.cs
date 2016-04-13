using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;

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
    class MainWindowViewModel: ViewModelBase
    {
        public DialogCoordinator _dia;

        public USStates stateName { get; }

        //private string _addressState;

        //public string AddressState
        //{
        //    get { return _addressState; }
        //    set
        //    {
        //        _addressState = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ICommand _generalCommand;

        private PeopleRepository peopleRepo;

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

        private Int32? _id;
        public Int32? ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

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

        public string keyWord { get; set; }

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

        private bool _idError;
        public bool idError { get { return _idError; } set { _idError = value; OnPropertyChanged(); } }

        private bool _ageError;
        public bool ageError { get { return _ageError; } set { _ageError = value; OnPropertyChanged(); } }
        private bool _zipError;
        public bool zipError { get { return _zipError; } set { _zipError = value; OnPropertyChanged(); } }

        private bool _firstNameError;
        public bool firstNameError { get { return _firstNameError; } set { _firstNameError = value;OnPropertyChanged(); } }
        private bool _lastNameError;
        public bool lastNameError { get { return _lastNameError; } set { _lastNameError = value; OnPropertyChanged(); } }
        public bool HasError
        {
            get { return !idError&&!ageError&&!zipError&&!firstNameError&&!lastNameError; }
        }

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

        public MainWindowViewModel(DialogCoordinator dia)
        {
            //var context = new PeopleContext();
            _dia = dia;
            peopleRepo = new PeopleRepository();
            People = peopleRepo.GetAllRecord();

            NewPerson = new Person();
            ageError = false;
            zipError = false;
            idError = true;
            firstNameError = true;
            lastNameError = true;
            ImagePath = "......";
            keyWord = "";

            stateName = new USStates();
        }

        void SearchExecute(object param)
        {
            //System.Windows.MessageBox.Show("Got here!");
            People = peopleRepo.SelectName(keyWord);
        }
        bool SearchCanExecute(object param)
        {
            return true;
        }

        void AddExecute(object param)
        {
            NewPerson.ID = ID.Value;
            peopleRepo.AddRecord(NewPerson, ImagePath);
            ImagePath = "......";
            //System.Windows.Forms.MessageBox.Show("Successful Added~!"+_dia.ToString());
            _dia.ShowMessageAsync(this, "Notification", "Record successfully added~");
            //System.Windows.Forms.MessageBox.Show("Successful Added~!");

            People = peopleRepo.GetAllRecord();
        }
        bool AddCanExecute(object param)
        {
            //if ((NewPerson.age < 0) || (NewPerson.age > 130))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            //new AgeRangeRule().Validate(NewPerson.age, null).IsValid
            return HasError;
            //return new AgeRangeRule().Validate(NewPerson.age);
            //else return !Validation.GetHasError(param as DependencyObject);
        }

        void BrowseExecute(object param)
        {
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.gif, *.png) | *.jpg; *.jpeg; *.jpe; *.gif; *.png";

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
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
        bool BrowseCanExecute(object param)
        {
            return true;
        }
    }
}
