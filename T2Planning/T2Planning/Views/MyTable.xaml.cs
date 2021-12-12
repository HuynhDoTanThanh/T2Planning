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

        private void logout_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }

        private void lstTable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (lstTable.SelectedItem != null)
            {
                Table table = (Table)lstTable.SelectedItem;

                Navigation.PushAsync(new TableDetail(table));
            }
        }

        void ListViewInit()
        {
            Database db = new Database();
            List<Table> tableworks = new List<Table>();

            tableworks = db.GetTableWork();
            if (tableworks.Count != 0)
            {
                note.Text = "Bảng của Thành";
            }
            lstTable.ItemsSource = tableworks;
        }
    }
}