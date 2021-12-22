using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using T2Planning.Services;
using T2Planning.Views.Create;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableDetail : ContentPage
    {
        Table mytable;
        Database db = new Database();
        string Uid;
        public TableDetail(Table table, string uid)
        {
            InitializeComponent();
            mytable = table;
            Uid = uid;
            Title = mytable.tableName;
            init();
        }


        private async void addlst_Clicked(object sender, EventArgs e)
        {
            string listCardName = await DisplayPromptAsync("Tạo danh sách mới", "Tên danh sách");

            if(string.IsNullOrWhiteSpace(listCardName))
            {
                await DisplayAlert("Thông báo", "Tạo mới that bai", "Ok");
            }
            else
            {
                ListCard listCard = new ListCard() { listCardName = listCardName, tableId = mytable.tableId };

                Sync sync = new Sync();
                try
                {
                    sync.PushListCard(listCard);
                    sync.PullListCard(Uid);
                }
                catch
                {
                    await DisplayAlert("Thông báo", "Tạo mới that bai", "Ok");
                }

                init();
            }
        }

        void init()
        {
            List<ListCard> listCards = new List<ListCard>();
            listCards = db.GetListCard();
            List<ListViewCard> listViewCards = new List<ListViewCard>();

            foreach (ListCard listCard in listCards)
            {
                List<Card> cards = db.GetCardWithQuery(listCard.listCardId);
                listViewCards.Add(new ListViewCard { cards = cards, listCardName = listCard.listCardName });
            }
            lstcard.ItemsSource = listViewCards;
        }


        private void addCard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateCard(Uid, true, mytable));
        }

        private async void lstCardDetail_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {

            if (((ListView)sender).SelectedItem != null)
            {
                Card card = (Card)((ListView)sender).SelectedItem;
                await Navigation.PushAsync(new CardDetail(card));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}