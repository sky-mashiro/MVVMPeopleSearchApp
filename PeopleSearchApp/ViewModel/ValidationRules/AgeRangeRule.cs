using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    //Used to validate age, range 0 -130 or empty
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

            string _age = "";

            if (value is BindingExpression)
            {
                var be = value as BindingExpression;
                if (be != null)
                {
                    var item = be.DataItem as MainWindowViewModel;
                    _age = item.NewPerson.age;
                }
            }
            else
                _age = value as string;

            try
            {
                if (string.IsNullOrEmpty(_age))
                    return new ValidationResult(true, null);

                age = Int32.Parse(_age);
            }
            catch
            {
                return new ValidationResult(false, "Pleas enter an Integer age" + Environment.NewLine + "or left it empty");
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
