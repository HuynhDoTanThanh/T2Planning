using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T2Planning.Models;
using T2Planning.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderContentView : ContentView
    {
        public HeaderContentView()
        {
            InitializeComponent();
            init();
        }

        async void init()
        {
            Database database = new Database();
            await Task.Delay(1000);
            User user = database.GetUser()[0];
             
            avatar.Source = user.userAvatar;
            userName.Text = user.userName;
            userEmail.Text = user.userEmail;
        }

        private async void avatar_Clicked(object sender, EventArgs e)
        {

            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new ChooseAvatar());
        }
    }
}