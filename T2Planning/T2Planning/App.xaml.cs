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
            loggedin();
        }
        
        void loggedin()
        {
            IAuth auth;
            Sync sync = new Sync();
            string Uid;
            auth = DependencyService.Get<IAuth>();
            if (auth.IsSignIn())
            {
                Uid = auth.GetUid();
                sync.PullDB(Uid);
                MainPage = new MainPage();
                //await Shell.Current.GoToAsync(nameof(MyTable));
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
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
