using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class AgeRangeRule: ValidationRule
    {
        private int _min;
        private int _max;

        public AgeRangeRule()
        {
            _min = 0;
            _max = 130;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if ((value.ToString()).Length > 0)
                    age = Int32.Parse((string)value);
            }
            catch
            {
                return new ValidationResult(false, "Pleas enter an Integer" + Environment.NewLine + "or left it empty");
            }

            if ((age < _min) || (age > _max))
            {
                return new ValidationResult(false, "Please enter an age in range: 0 - 130" + Environment.NewLine + "or left it empty");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }

        public bool Validate(int age)
        {
            if ((age < _min) || (age > _max))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
