using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using PeopleSearchApp.Model.DataAccess;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class IDRules: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int id = 0;

            try
            {
                if (string.IsNullOrEmpty(value as string))
                    return new ValidationResult(false, "ID cannot be empty");

                id = Int32.Parse(value as string);
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
    }
}
