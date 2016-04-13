using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class NameRules: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = value as string;
            if (string.IsNullOrEmpty(name))
            {
                return new ValidationResult(false, "Name cannot be empty");
            }

            return new ValidationResult(true, null);
        }
    }
}
