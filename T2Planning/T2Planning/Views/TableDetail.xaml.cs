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
        public TableDetail(Table table)
        {
            InitializeComponent();
            mytable = table;
            Title = mytable.tableName;
            init();
        }

        private async void addlst_Clicked(object sender, EventArgs e)
        {
            string listCardName = await DisplayPromptAsync("Tạo danh sách mới", "Tên danh sách");

            ListCard listCard = new ListCard() { listCardName = listCardName, tableId = mytable.tableId };

            db.AddNewListCard(listCard);

            init();

            await DisplayAlert("Thông báo", "Tạo mới thành công" + "\nTên danh sách: " + listCardName, "Ok");
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
            Navigation.PushAsync(new CreateCard());
        }
    }
}