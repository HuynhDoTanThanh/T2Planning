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
    public partial class CardDetail : ContentPage
    {
        Database database = new Database();
        List<Table> tables;
        List<ListCard> listCards;
        Card card;
        string tableName = "";
        string listCardName = "";
        DateTime cardDeadline;
        Sync sync = new Sync();
        string Uid;
        Table table;

        public CardDetail()
        {
            InitializeComponent();
        }

        public CardDetail(Card cardTake, string uid, Table tableTake)
        {
            InitializeComponent();
            card = cardTake;
            Uid = uid;
            table = tableTake;
            init();
            cardName_entry.Text = card.cardName;
            cardDescription_entry.Text = card.cardDescription;
            deadlineDay.Date = card.cardDeadline.Date;
            deadlineTime.Time = card.cardDeadline.TimeOfDay;

        }
        void init()
        {
            if (Uid == table.tableAdmin)
            {
                update.IsVisible = true;
            }

            tables = database.GetTable();
            foreach (Table t in tables)
            {
                if (card.tableId == t.tableId)
                {
                    tableName = t.tableName;
                    break;
                }
            }

            listCards = database.GetListCard();
            foreach (ListCard l in listCards)
            {
                if (card.listCardId == l.listCardId)
                {
                    listCardName = l.listCardName;
                    break;
                }
            }
            tableName_label.Text = tableName;
            listCardName_label.Text = listCardName;
        }

        private void addCard()
        {
            cardDeadline = deadlineDay.Date.Add(deadlineTime.Time);

            if (card.cardName != cardName_entry.Text)
            {
                card.cardName = cardName_entry.Text;
            }

            if (card.cardDescription != cardDescription_entry.Text)
            {
                card.cardDescription = cardDescription_entry.Text;
            }
            cardDeadline = deadlineDay.Date.Add(deadlineTime.Time);
            if (card.cardDeadline != cardDeadline)
            {
                card.cardDeadline = cardDeadline;
            }

            sync.UpdateCard(card);
            sync.PullCard(Uid);
        }
        bool checknull()
        {
            if (string.IsNullOrWhiteSpace(cardName_entry.Text) || cardDeadline == null)
            {
                return true;
            }
            return false;
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (checknull())
            {
                DisplayAlert("Tạo the", "Vui long dien day du thong tin", "OK");
            }
            else
            {
                addCard();
                var nav = new NavigationPage(new TableDetail(table, Uid))
                {
                    BarBackgroundColor = Color.FromHex("#EB62B9")

                };
                Application.Current.MainPage = nav;
            }
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            cardName_entry.IsReadOnly = false;
            cardDescription_entry.IsReadOnly = false;
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Xoá thành viên", "Bạn có muốn xoá không?", "Có", "Không");
            if (answer)
            {
                sync.DeleteCard(card.cardId);

                var nav = new NavigationPage(new TableDetail(table, Uid))
                {
                    BarBackgroundColor = Color.FromHex("#EB62B9")

                };
                Application.Current.MainPage = nav;
            }
        }
    }
}