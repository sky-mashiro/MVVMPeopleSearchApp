using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;

using System.Windows.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;

using PeopleSearchApp.Model;
using PeopleSearchApp.Model.DataAccess;
using PeopleSearchApp.Command;

namespace PeopleSearchApp.ViewModel
{
    class MainWindowViewModel: ViewModelBase
    {
        public int count = 4;
        private ICommand _searchCommand;
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

        public ICommand SearchCommand
        {
            get { return _searchCommand = new RelayCommand(SearchExecute, SearchCanExecute); }
        }

        public ICommand AddCommand
        {
            get { return _searchCommand = new RelayCommand(AddExecute, AddCanExecute); }
        }

        public ICommand BrowseCommand
        {
            get { return _searchCommand = new RelayCommand(BrowseExecute, BrowseCanExecute); }
        }

        public MainWindowViewModel()
        {
            //var context = new PeopleContext();
            peopleRepo = new PeopleRepository();
            People = peopleRepo.GetAllRecord();

            NewPerson = new Person();
            ImagePath = "......";
            keyWord = "";
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
            NewPerson.ID = count++;
            peopleRepo.AddRecord(NewPerson, ImagePath);
            ImagePath = "......";
            People = peopleRepo.GetAllRecord();
        }
        bool AddCanExecute(object param)
        {
            return true;
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
                    MessageBox.Show("Image cannot be bigger than 2MB!" + Environment.NewLine + "Please choose a new one^_^");
                }
            }
        }
        bool BrowseCanExecute(object param)
        {
            return true;
        }
    }
}
