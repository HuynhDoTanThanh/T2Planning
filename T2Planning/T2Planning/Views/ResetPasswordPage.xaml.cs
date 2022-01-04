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
    public partial class ResetPasswordPage : ContentPage
    {
        string Uid;
        public ResetPasswordPage()
        {
            InitializeComponent();
        }

        private async void sendLink_Clicked(object sender, EventArgs e)
        {
            string email = email_entry.Text;

            IAuth auth;
            auth = DependencyService.Get<IAuth>();
            Uid = auth.GetUid();

            Database database = new Database();
            User user = database.GetUser()[0];

            string current_email = user.userEmail;



            if (string.IsNullOrEmpty(email) || email != current_email)
            {
                await DisplayAlert("Thông báo", "Bạn chưa nhập hoặc đã nhập sai email.", "Ok");
                return;
            }
            bool isSend = auth.ResetPassword(email);

            if (isSend)
            {
                await DisplayAlert("Thông báo", "Link thay đổi mật khẩu đã được gửi đến email của bạn.", "Ok");

                var signOut = auth.SignOut();
                if (signOut)
                {
                    await Shell.Current.Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
                }
            }
            else
            {
                await DisplayAlert("Thông báo", "Gửi link thất bại.", "Ok");
            }
        }
    }
}