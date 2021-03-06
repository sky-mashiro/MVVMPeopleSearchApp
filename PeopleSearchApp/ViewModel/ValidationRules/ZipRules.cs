﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PeopleSearchApp.ViewModel.ValidationRules
{
    class ZipRules: ValidationRule
    {
        //Used to check if is a valid US zip
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string zip = "";

            if (value is BindingExpression)
            {
                var be = value as BindingExpression;
                if (be != null)
                {
                    var item = be.DataItem as MainWindowViewModel;
                    zip = item.NewPerson.address.zip;
                }
            }
            else
                zip = value as string;

            if (string.IsNullOrEmpty(zip))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
                if(Regex.Match(zip, _usZipRegEx).Success)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Pleas enter a valid US zip" + Environment.NewLine + "or left it empty");
                }
            }
        }
    }
}
