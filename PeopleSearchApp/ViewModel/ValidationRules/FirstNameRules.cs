using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class FirstNameRules: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = "";

            if (value is BindingExpression)
            {
                var be = value as BindingExpression;
                if (be != null)
                {
                    var item = be.DataItem as MainWindowViewModel;
                    name = item.NewPerson.firstName;
                }
            }
            else
                name = value as string;

            if (string.IsNullOrEmpty(name))
            {
                return new ValidationResult(false, "First name cannot be empty");
            }

            return new ValidationResult(true, null);
        }
    }
}
