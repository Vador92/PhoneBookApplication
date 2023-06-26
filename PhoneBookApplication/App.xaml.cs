using System;
using System.IO;
using PhoneBookApplication.Models;
using PhoneBookApplication.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBookApplication
{
    public partial class App : Application
    {
        //field for database, we can always access
        private static ContactDatabase database;

        //if connection is already there then we 
        public static ContactDatabase Database
        {
            //if null, creates new database, if not then it returns current database
            get
            {
                if(database == null)
                {
                    database = new ContactDatabase(Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "contact.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactOverviewView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
