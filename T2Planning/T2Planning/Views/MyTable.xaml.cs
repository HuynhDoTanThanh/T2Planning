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
    public partial class MyTable : ContentPage
    {
        IAuth auth;
        public MyTable()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            ListViewInit();
        }

        private async void logout_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        private async void lstTable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (lstTable.SelectedItem != null)
            {
                Table table = (Table)lstTable.SelectedItem;
                var nav = new NavigationPage(new TableDetail(table))
                {
                    BarBackgroundColor = Color.FromHex("#EB62B9")

                };
                await Shell.Current.Navigation.PushModalAsync(nav);
            }
        }

        void ListViewInit()
        {
            Database db = new Database();
            List<Table> tableworks = new List<Table>();

            tableworks = db.GetTable();
            if (tableworks.Count != 0)
            {
                note.Text = "Bảng của Thành";
            }
            lstTable.ItemsSource = tableworks;
        }
    }
}