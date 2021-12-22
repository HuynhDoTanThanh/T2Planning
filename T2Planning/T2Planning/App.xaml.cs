using System;
using T2Planning.Services;
using T2Planning.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Database db = new Database();
            try
            {
                db.CreateDatabase();
            }
            catch
            {

            }
            MainPage = new NavigationPage(new LoginPage());
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
