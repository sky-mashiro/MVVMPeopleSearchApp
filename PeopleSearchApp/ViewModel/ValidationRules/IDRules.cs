using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using PeopleSearchApp.Model.DataAccess;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class IDRules: ValidationRule
    {

        //Used to check ID, valid and unique
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int id = 0;

            string _id = "";

            if (value is BindingExpression)
            {
                var be = value as BindingExpression;
                if (be != null)
                {
                    var item = be.DataItem as MainWindowViewModel;
                    _id = item.ID;
                }
            }
            else
                _id = value as string;

            try
            {
                if (string.IsNullOrEmpty(_id))
                    return new ValidationResult(false, "ID cannot be empty");

                id = Int32.Parse(_id);
            }
            catch
            {
                return new ValidationResult(false, "Pleas enter an Integer ID");
            }

            if(new PeopleRepository().GetAllID().Contains(id))
            {
                return new ValidationResult(false, "ID already exists" + Environment.NewLine + "Please enter a new one");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }


        //Created for unit test, since data model access is needed to check if id exists
        public ValidationResult Validate(object value, CultureInfo cultureInfo, PeopleRepository repo)
        {
            int id = 0;

            string _id = "";

            if (value is BindingExpression)
            {
                var be = value as BindingExpression;
                if (be != null)
                {
                    var item = be.DataItem as MainWindowViewModel;
                    _id = item.ID;
                }
            }
            else
                _id = value as string;

            try
            {
                if (string.IsNullOrEmpty(_id))
                    return new ValidationResult(false, "ID cannot be empty");

                id = Int32.Parse(_id);
            }
            catch
            {
                return new ValidationResult(false, "Pleas enter an Integer ID");
            }

            if (repo.GetAllID().Contains(id))
            {
                return new ValidationResult(false, "ID already exists" + Environment.NewLine + "Please enter a new one");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
