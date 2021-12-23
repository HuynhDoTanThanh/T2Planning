using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using T2Planning.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        IAuth auth;
        Sync sync = new Sync();
        string Uid;

        public LoginPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        bool checknull()
        {
            if (string.IsNullOrWhiteSpace(EmailInput.Text) || string.IsNullOrWhiteSpace(PasswordInput.Text))
            {
                return true;
            }
            return false;
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            stackLoading.BindingContext = this;
            loading.BindingContext = this;

            IsBusy = true;
            backStack.IsVisible = false;
            if (!checknull())
            {
                string token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
                if (token != string.Empty)
                {
                    Uid = auth.GetUid();
                    try
                    {
                        Database database = new Database();
                        User user = database.GetUser()[0];
                        if (Uid != user.Uid)
                        {
                            database.DeleteDatabase();
                            database.CreateDatabase();
                        }
                    }
                    catch
                    {

                    }
                    sync.PullDB(Uid);
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Authentication Failed", "Email or Password are incorrect", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Thông báo", "Tên đăng nhập hoặc mật khẩu không thể để trống", "Ok");
            }
            IsBusy = false;
            backStack.IsVisible = true;
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                await Navigation.PushAsync(new SignUpPage());
            }
        }

    }
}