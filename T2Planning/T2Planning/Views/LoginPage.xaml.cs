using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        IAuth auth;

        public LoginPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }
        async void LoginClicked(object sender, EventArgs e)
        {
            string token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or Password are incorrect", "Ok");
            }
        }
        void SignUpClicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                Navigation.PushAsync(new SignUpPage());
            }
        }
    }
}