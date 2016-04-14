using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class LastNameRules : ValidationRule
    {
        //Used to validate the name
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = "";

            if (value is BindingExpression)
            {
                var be = value as BindingExpression;
                if (be != null)
                {
                    var item = be.DataItem as MainWindowViewModel;
                    name = item.NewPerson.lastName;
                }
            }
            else
            {
                name = value as string;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult(false, "Last name cannot be empty");
            }


            return new ValidationResult(true, null);
        }
    }
}
