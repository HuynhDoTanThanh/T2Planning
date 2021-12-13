using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using T2Planning.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCard : ContentPage
    {
        Database database = new Database();
        List<Table> tables;
        List<ListCard> listCards;

        string tableChoosed = "";
        string listChoosed = "";
        int? tableId;
        int? listCardId;
        DateTime cardDeadline;

        public CreateCard()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            tables = database.GetTable();
            listCards = database.GetListCard();

            List<string> tablename = new List<string>();
            foreach(Table table in tables)
            {
                tablename.Add(table.tableName);
            }

            tablechoose.ItemsSource = tablename;
        }

        bool checknull()
        {
            if (string.IsNullOrWhiteSpace(cardName.Text) || string.IsNullOrWhiteSpace(cardDescription.Text) || tableId == null || listCardId == null || cardDeadline == null)
            {
                return true;
            }
            return false;
        }



        private void tablechoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                tableChoosed = (string)picker.ItemsSource[selectedIndex];
            }

            foreach (Table table in tables)
            {
                if (table.tableName == tableChoosed)
                {
                    tableId = table.tableId;
                    break;
                }
            }

            List<ListCard> listCardschoosed = new List<ListCard>();
            foreach (ListCard listCard in listCards)
            {
                if (listCard.tableId == tableId)
                {
                    listCardschoosed.Add(listCard);
                }
            }

            List<string> listname = new List<string>();
            if(listCardschoosed != null)
            {
                foreach (ListCard listCard in listCardschoosed)
                {
                    listname.Add(listCard.listCardName);
                }
                listchoose.ItemsSource = listname;
            }
        }

        private void listchoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                listChoosed = (string)picker.ItemsSource[selectedIndex];
            }
            foreach (ListCard listCard in listCards)
            {
                if ((listCard.listCardName == listChoosed) && (listCard.tableId == tableId))
                {
                    listCardId = listCard.listCardId;
                    break;
                }
            }
        }

        private void addCard()
        {
            cardDeadline = deadlineDay.Date.Add(deadlineTime.Time);

            Card card = new Card()
            {
                cardName = cardName.Text,
                cardDescription = cardDescription.Text,
                tableId = (int)tableId,
                listCardId = (int)listCardId,
                cardDeadline = cardDeadline
            };

            if (database.AddNewCard(card))
            {
                DisplayAlert("Tạo the", "Tạo the thành công\n" 
                        + "name: " + card.cardName + "\ndes: " + card.cardDescription.ToString() 
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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if(checknull())
            {
                DisplayAlert("Tạo the", "Vui long dien day du thong tin", "OK");
            }
            else
            {
                addCard();
            }
        }
    }
}