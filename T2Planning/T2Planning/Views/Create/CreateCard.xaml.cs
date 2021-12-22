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

        string Uid;
        bool tableDetail;
        Table table;

        public CreateCard(string uid, bool tabledetail = false, Table tb = null)
        {
            InitializeComponent();
            Uid = uid;
            tableDetail = tabledetail;
            table = tb;
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
            if (string.IsNullOrWhiteSpace(cardName.Text) || tableId == null || listCardId == null || cardDeadline == null)
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

            Sync sync = new Sync();

            try
            {
                sync.PushCard(card);
                sync.PullCard(Uid);
                if(tableDetail)
                {
                    var nav = new NavigationPage(new TableDetail(table, Uid))
                    {
                        BarBackgroundColor = Color.FromHex("#EB62B9")

                    };
                    Application.Current.MainPage = nav;
                }
                else
                {
                    Navigation.PopAsync();
                }
            }
            catch
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