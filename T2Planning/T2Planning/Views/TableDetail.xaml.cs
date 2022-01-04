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
        Sync sync = new Sync();
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

                try
                {
                    sync.PushListCard(listCard);
                    sync.PullListCard(Uid);
                    var nav = new NavigationPage(new TableDetail(mytable, Uid))
                    {
                        BarBackgroundColor = Color.FromHex("#EB62B9")

                    };
                    Application.Current.MainPage = nav;
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
            if (Uid == mytable.tableAdmin)
            {
                fradd.IsVisible = true;
            }
            List<ListCard> listCards = new List<ListCard>();
            listCards = db.GetListCard();
            List<ListViewCard> listViewCards = new List<ListViewCard>();

            foreach (ListCard listCard in listCards)
            {
                if (listCard.tableId == mytable.tableId)
                {
                    List<Card> cards = db.GetCardWithQuery(listCard.listCardId);
                    listViewCards.Add(new ListViewCard { cards = cards, listCard = listCard });
                }
            }
            lstcard.ItemsSource = listViewCards;
        }


        private void addCard_Clicked(object sender, EventArgs e)
        {
            if (Uid == mytable.tableAdmin)
            {
                Navigation.PushAsync(new CreateCard(Uid, true, mytable));
            }
            else
            {
                DisplayAlert("Thông báo:", "Bạn không phải chủ phòng", "Ok");
            }
        }

        private async void lstCardDetail_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {

            if (((ListView)sender).SelectedItem != null)
            {
                Card card = (Card)((ListView)sender).SelectedItem;
                await Navigation.PushAsync(new CardDetail(card, Uid, mytable));
            }
            ((ListView)sender).SelectedItem = null;
        }

        private async void editListName_Clicked(object sender, EventArgs e)
        {
            if (Uid == mytable.tableAdmin)
            {
                string listCardName_new = await DisplayPromptAsync("Thay đổi tên danh sách", "Tên danh sách mới");
                var listCard = (ListViewCard)lstcard.CurrentItem;
                //ListCardId current 
                int listCardID = listCard.listCard.listCardId;

                ListCard listCard_new = new ListCard() { listCardId = listCardID, listCardName = listCardName_new };

                try
                {
                    if (!string.IsNullOrWhiteSpace(listCardName_new))
                    {
                        sync.UpdateListCard(listCard_new);
                        db.resetListCard();
                        sync.PullListCard(Uid);

                        var nav = new NavigationPage(new TableDetail(mytable, Uid))
                        {
                            BarBackgroundColor = Color.FromHex("#EB62B9")

                        };
                        Application.Current.MainPage = nav;
                    }
                    else
                    {
                        await DisplayAlert("Thông báo:", "Vui lòng nhập lại tên danh sách", "Ok");
                    }
                }
                catch
                {
                    await DisplayAlert("Thông báo:", "Thay đổi thất bại", "Ok");
                }
            }
        }

        private async void deleteListName_Clicked(object sender, EventArgs e)
        {
            if (Uid == mytable.tableAdmin)
            {
                bool answer = await DisplayAlert("Xoá thành viên", "Bạn có muốn xoá không?", "Có", "Không");
                if (answer)
                {
                    var listCard = (ListViewCard)lstcard.CurrentItem;
                    int listCardID = listCard.listCard.listCardId;

                    try
                    {
                        sync.DeleteListCard(listCardID);
                        db.resetListCard();
                        sync.PullListCard(Uid);

                        var nav = new NavigationPage(new TableDetail(mytable, Uid))
                        {
                            BarBackgroundColor = Color.FromHex("#EB62B9")

                        };
                        Application.Current.MainPage = nav;
                    }
                    catch
                    {
                        await DisplayAlert("Thông báo:", "Xoá thất bại", "Ok");
                    }
                }
            }
        }
        private void setting_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditTablePage(mytable));
        }
    }
}