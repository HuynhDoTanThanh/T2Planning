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
    public partial class CreateTable : ContentPage
    {
        string Uid = "";
        Database db = new Database();
        List<User> listUser = new List<User>();
        public CreateTable(string uid)
        {
            InitializeComponent();
            Uid = uid;
            affordCreate();
            User user_cur = db.GetUser()[0];
            listUser.Add(user_cur);
        }

        string teamChoosed = "";
        string permissChoosed = "";

        private async void create_Clicked(object sender, EventArgs e)
        {
            Table table = new Table();
            table.tableAdmin = Uid;
            table.tableName = tableName.Text;
            if (teamChoosed == "Bảng Cá nhân")
            {
                table.tableTeam = 1;
            }
            else if (teamChoosed == "Không gian làm việc")
            {
                table.tableTeam = 2;
            }

            if (permissChoosed == "Riêng tư")
            {
                table.tablePermission = 1;
            }
            else if (permissChoosed == "Nhóm")
            {
                table.tablePermission = 2;
            }
            Sync sync = new Sync();
            try
            {
                int tableId = await sync.PushTableAsync(table);
                if(tableId != 0)
                {
                    Member member = new Member() { tableId = tableId, Uid = Uid };

                    sync.PushMemberAsync(member);
                    sync.PullMemberAsync(Uid);
                    sync.PullTable(Uid);

                    Application.Current.MainPage = new MainPage(Uid);
                }
                else
                {
                    await DisplayAlert("Tạo bảng", "tạo bảng thất bại", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Tạo bảng", "tạo bảng thất bại", "OK");
            }
        }

        bool checknull()
        {
            if (teamChoosed == "" || permissChoosed == "" || tableName.Text == "")
            {
                return true;
            }
            return false;
        }

        void affordCreate()
        {
            if (checknull())
            {
                create.IsEnabled = false;
            }
            else
            {
                create.IsEnabled = true;
            }
        }

        private void team_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            List<string> permiss;

            if (selectedIndex != -1)
            {
                teamChoosed = (string)picker.ItemsSource[selectedIndex];
            }

            if (teamChoosed == "Bảng Cá nhân")
            {
                permiss = new List<string>();
                permiss.Add("Riêng tư");
                permission.ItemsSource = permiss;
            }
            else if (teamChoosed == "Không gian làm việc")
            {
                permiss = new List<string>();
                permiss.Add("Riêng tư");
                permiss.Add("Nhóm");
                permission.ItemsSource = permiss;
            }
        }

        private void permission_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                permissChoosed = (string)picker.ItemsSource[selectedIndex];
            }
            affordCreate();
        }

        private void addMember_Clicked(object sender, EventArgs e)
        {

        }

    }
}