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
        Database db = new Database();

        public MyCard()
        {
            InitializeComponent();
            ListViewInit();
        }


        void ListViewInit()
        {
            List<Card> mycards = new List<Card>();

            mycards = db.GetCard();
            mycards.Sort((x, y) => DateTime.Compare(x.cardDeadline, y.cardDeadline));
            lstCard.ItemsSource = mycards;
        }

        private void lstCard_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Card card = e.Item as Card;
            User user = db.GetUser()[0];
            Navigation.PushAsync(new CardDetail(card, user.Uid, true));
        }
    }
}