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

        void init()
        {
            Database database = new Database();
            User user = database.GetUser()[0];

            avatar.Source = user.userAvatar;
            userName.Text = user.userName;
            userEmail.Text = user.userEmail;
        }
    }
}