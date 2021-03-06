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
    public partial class MyTable : ContentPage
    {
        IAuth auth;
        string Uid;
        public MyTable()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            Uid = auth.GetUid();
            ListViewInit();
        }


        private async void lstTable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (lstTable.SelectedItem != null)
            {
                Table table = (Table)lstTable.SelectedItem;
                var nav = new NavigationPage(new TableDetail(table, Uid))
                {
                    BarBackgroundColor = Color.FromHex("#EB62B9")

                };
                await Shell.Current.Navigation.PushModalAsync(nav);
            }
        }

        void ListViewInit()
        {
            Database db = new Database();
            List<Table> tables = new List<Table>();
            User user = db.GetUser()[0];
            tables = db.GetTable();
            if (tables.Count != 0)
            {
                note.Text = "Không gian làm việc của " + user.userName.ToString();
            }
            lstTable.ItemsSource = tables;
        }

        private bool isOpen = false;

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                await FloatingActionButton.RotateTo(180, 300);
                FloatingActionButton.Rotation = 0;
                FloatingActionButton.Text = "−";
                isOpen = true;

                FloatMenuItem1.IsVisible = true;
                await FloatButtonItem1.ScaleTo(1, 150);
                FloatMenuItem2.IsVisible = true;
                await FloatButtonItem2.ScaleTo(1, 150);
            }
            else
            {
                await FloatingActionButton.RotateTo(180, 300);
                FloatingActionButton.Rotation = 0;
                FloatingActionButton.Text = "+";
                isOpen = false;


                await FloatButtonItem1.ScaleTo(0.01, 150);
                FloatMenuItem1.IsVisible = false;

                await FloatButtonItem2.ScaleTo(0.01, 150);
                FloatMenuItem2.IsVisible = false;
            }
        }

        private async void sync_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage(Uid);
        }
        private async void FloatMenuItem2Tap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTable(Uid));
        }

        private async void FloatMenuItem1Tap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateCard(Uid));
        }
    }
}