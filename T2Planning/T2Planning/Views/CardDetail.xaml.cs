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
        Card card;
        DateTime cardDeadline;
        Sync sync = new Sync();
        string Uid;
        bool ismycard;
        Table table;

        public CardDetail()
        {
            InitializeComponent();
        }

        public CardDetail(Card cardTake, string uid, bool mycard=false)
        {
            InitializeComponent();
            card = cardTake;
            Uid = uid;
            ismycard = mycard;
            init();
            cardName_entry.Text = card.cardName;
            cardDescription_entry.Text = card.cardDescription;
            deadlineDay.Date = card.cardDeadline.Date;
            deadlineTime.Time = card.cardDeadline.TimeOfDay;
        }
        void init()
        {
            List<Table> tables = database.GetTable();
            foreach (Table t in tables)
            {
                if (card.tableId == t.tableId)
                {
                    table = t;
                    break;
                }
            }

            if (Uid == table.tableAdmin)
            {
                update.IsVisible = true;
                delete.IsVisible = true;
            }
            ListCard listCard = database.GetListCardWithQuery(card.listCardId);
            tableName_label.Text = table.tableName;
            listCardName_label.Text = listCard.listCardName;
        }

        private void editCard()
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
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (Uid == table.tableAdmin)
            {
                if (checknull())
                {
                    await DisplayAlert("Tạo the", "Vui long dien day du thong tin", "OK");
                }
                else if (ismycard)
                {
                    editCard();
                    Application.Current.MainPage = new MainPage(Uid);
                    await Shell.Current.GoToAsync(nameof(MyCard));
                }
                else
                {
                    editCard();
                    var nav = new NavigationPage(new TableDetail(table, Uid))
                    {
                        BarBackgroundColor = Color.FromHex("#EB62B9")

                    };
                    Application.Current.MainPage = new MainPage(Uid);
                    await Shell.Current.Navigation.PushModalAsync(nav);
                }
            }
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            cardName_entry.IsReadOnly = false;
            cardDescription_entry.IsReadOnly = false;
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Xoá thẻ", "Bạn có muốn xoá không?", "Có", "Không");
            if (answer)
            {
                sync.DeleteCard(card.cardId);
                sync.PullCard(Uid);

                if (ismycard)
                {
                    Application.Current.MainPage = new MainPage(Uid);
                    await Shell.Current.GoToAsync(nameof(MyCard));
                }
                else
                {
                    var nav = new NavigationPage(new TableDetail(table, Uid))
                    {
                        BarBackgroundColor = Color.FromHex("#EB62B9")

                    };
                    Application.Current.MainPage = new MainPage(Uid);
                    await Shell.Current.Navigation.PushModalAsync(nav);
                }
            }
        }
    }
}