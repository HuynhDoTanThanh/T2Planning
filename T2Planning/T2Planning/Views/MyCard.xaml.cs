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
    public partial class MyCard : ContentPage
    {
        IAuth auth;

        public MyCard()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            ListViewInit();
        }

        private async void logout_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }


        void ListViewInit()
        {
            Database db = new Database();
            List<Card> mycards = new List<Card>();

            mycards = db.GetCard();
            mycards.Sort((x, y) => DateTime.Compare(x.cardDeadline, y.cardDeadline));
            lstCard.ItemsSource = mycards;
        }

        private void lstCard_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void deleteCard_Clicked(object sender, EventArgs e)
        {

        }
    }
}