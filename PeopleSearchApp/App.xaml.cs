using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using PeopleSearchApp.Model;

namespace PeopleSearchApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //Override OnStratup to change the database initializer to seed data
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Database.Delete("PeopleSearchApp.Model.PeopleContext");
            //Database.SetInitializer<PeopleContext>(new PeopleInitializer());
        }
    }
}
