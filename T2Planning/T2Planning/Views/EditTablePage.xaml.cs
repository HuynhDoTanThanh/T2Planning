using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        Table table = new Table();
        Sync sync = new Sync();
        User user;
        User useradmin;
        List<Member> members_new = new List<Member>();

        ObservableCollection<User> listUser = new ObservableCollection<User>();

        public EditTablePage()
        {
            InitializeComponent();
        }

        public EditTablePage(Table tableTake)
        {
            InitializeComponent();
            table = tableTake;

            init();
        }

        void init()
        {
            List<Member> members;

            tableName.Text = table.tableName;

            user = db.GetUser()[0];
            useradmin = sync.GetUserUid(table.tableAdmin); 
            members = sync.PullTableMember(table.tableId);

            foreach(Member member in members)
            {
                listUser.Add(sync.GetUserUid(member.Uid));
            }


            if(useradmin != null)
            {
                admin.Text = useradmin.userName;
                listAvatar.ItemsSource = listUser;
                if(user.Uid == useradmin.Uid)
                {
                    update.IsVisible = true;
                }
            }

            if (table.tablePermission == 2)
            {
                addMember.IsVisible = true;
            }
        }
        private void save_Clicked(object sender, EventArgs e)
        {
            if (tableName.Text != table.tableName && !(string.IsNullOrWhiteSpace(tableName.Text)))
            {   
                table.tableName = tableName.Text;
                sync.UpdateTable(table);
                sync.PullTable(user.Uid);
                foreach(Member member in members_new)
                {
                    sync.PushMember(member);
                }   
                DisplayAlert("Thông báo", table.tableName, "Ok");
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Thông báo", "Thay đổi không thành công", "Ok");
            }
        }

        private void update_Clicked(object sender, EventArgs e)
        {
            tableName.IsReadOnly = false;
            addMember.IsEnabled = true;
        }

        private async void addMember_Clicked(object sender, EventArgs e)
        {
            string uid_add = await DisplayPromptAsync("Thêm thành viên", "Nhập UID");

            members_new.Add(new Member { tableId = table.tableId, Uid = uid_add });

            User user = sync.GetUserUid(uid_add);

            if (user != null)
            {
                listUser.Add(user);
                await DisplayAlert("Thong bao", user.Uid + user.userName, "Ok");
            }
        }
    }
}