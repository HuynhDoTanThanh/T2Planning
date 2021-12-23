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
        int? tableId;
        int listCardId;
        DateTime cardDeadline;
        List<ListCard> listCardschoosed;
        public CardDetail()
        {
            InitializeComponent();
        }
        public CardDetail(Card cardTake)
        {
            InitializeComponent();
            card = cardTake;
            init();
            cardName_entry.Text = card.cardName;
            cardDescription_entry.Text = card.cardDescription;
            deadlineDay.Date = card.cardDeadline.Date;
            deadlineTime.Time = card.cardDeadline.TimeOfDay;

        }
        void init()
        {
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
            

            if (database.AddNewCard(card))
            {
                DisplayAlert("Tạo the", "Tạo the thành công\n"
                        + "name: " + card.cardName 
                        + "\ndes: " + card.cardDescription.ToString()
                        + "\ntable: " + card.tableId.ToString()
                        + "\nlistcard: " + card.listCardId.ToString()
                        + "\nDate: " + card.cardDeadline.ToString(), "OK");
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Tạo the", "tạo the thất bại", "OK");
            }
        }
        bool checknull()
        {
            if (string.IsNullOrWhiteSpace(cardName_entry.Text) || tableId == null || listCardId == null || cardDeadline == null)
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
            }
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            cardName_entry.IsReadOnly = false;
            cardDescription_entry.IsReadOnly = false;
        }
    }
}