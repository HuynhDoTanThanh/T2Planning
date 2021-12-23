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
    public partial class EditTablePage : ContentPage
    {
        Database db = new Database();
        List<User> listUser = new List<User>();
        Table table = new Table();
        public EditTablePage()
        {
            InitializeComponent();
        }

        public EditTablePage(Table tableTake)
        {
            InitializeComponent();
            addMember.IsEnabled = false;
            table = tableTake;
            User user_cur = db.GetUser()[0];
            listUser.Add(user_cur);
            init();
        }

        void init()
        {
            tableName.Text = table.tableName;
            listAvatar.ItemsSource = listUser;
        }
        private void save_Clicked(object sender, EventArgs e)
        {
            if (tableName.Text != table.tableName && !(string.IsNullOrWhiteSpace(tableName.Text)))
            {
                table.tableName = tableName.Text;
                DisplayAlert("Thông báo", table.tableName, "Ok");
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Thông báo", "Thay đổi không thành công", "Ok");
            }
            //Push table lên database nữa là oke
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            tableName.IsReadOnly = false;
            addMember.IsEnabled = true;
        }

        private async void addMember_Clicked(object sender, EventArgs e)
        {
            string uid_add = await DisplayPromptAsync("Thêm thành viên", "Nhập UID");
            //Làm gì đó để thêm thành viên khi có uid_add rồi đi bạn yêu 
            User user_cur = db.GetUser()[0];
            listUser.Add(user_cur);
        }
    }
}