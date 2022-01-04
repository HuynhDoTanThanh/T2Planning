using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using T2Planning.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting : ContentPage
    {
        IAuth auth;
        string Uid;
        public Setting()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            Uid = auth.GetUid();
            init();
        }

        void init()
        {
            Database database = new Database();
            User user = database.GetUser()[0];

            avatar.Source = user.userAvatar;
            userName.Text = user.userName;
            userEmail.Text = user.userEmail;
            uid_entry.Text = Uid;
            uid_entry.IsReadOnly = true;
        }

        private async void avatar_Clicked(object sender, EventArgs e)
        {

            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new ChooseAvatar());
        }

        private async void changePass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPasswordPage());
        }

        private async void copyUID_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(Uid);
            var text = await Clipboard.GetTextAsync();
            await DisplayAlert("Thông báp", "Đã sao chép UID vào bộ nhớ tạm", "ok");
        }

        private async void logout_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                await Shell.Current.Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            }
        }
    }
}