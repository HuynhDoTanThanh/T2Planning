using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        Sync sync = new Sync();
        Table table;
        User user;
        User useradmin;
        List<Member> members_new = new List<Member>();

        ObservableCollection<ShowMember> listUser = new ObservableCollection<ShowMember>();

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
                User user = sync.GetUserUid(member.Uid);
                ShowMember showMember = new ShowMember() { Uid = user.Uid, userName = user.userName, userAvatar = user.userAvatar, delete = DeleteEintragCommand };
                listUser.Add(showMember);
            }


            if(useradmin != null)
            {
                admin.Text = useradmin.userName;
                listAvatar.ItemsSource = listUser;
                if(user.Uid == useradmin.Uid)
                {
                    update.IsVisible = true;
                    deleteTable.IsVisible = true;
                }
            }

            if (table.tablePermission == 2)
            {
                addMember.IsVisible = true;
            }
        }
        private void save_Clicked(object sender, EventArgs e)
        {
            if (tableName.Text != table.tableName && !(string.IsNullOrWhiteSpace(tableName.Text)) || members_new.Count > 0)
            {
                if (tableName.Text != table.tableName && !(string.IsNullOrWhiteSpace(tableName.Text)))
                {
                    table.tableName = tableName.Text;
                    sync.UpdateTable(table);
                    sync.PullTable(user.Uid);
                }
                if (members_new.Count > 0)
                {
                    foreach (Member member in members_new)
                    {
                        sync.PushMember(member);
                    }
                }
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
                ShowMember showMember = new ShowMember() { Uid = user.Uid, userName = user.userName, userAvatar = user.userAvatar, delete = DeleteEintragCommand };
                listUser.Add(showMember);
                await DisplayAlert("Thong bao", user.Uid + user.userName, "Ok");
            }
        }
        private Command deleteEintragCommand;
        public ICommand DeleteEintragCommand
        {
            get
            {
                if (user.Uid == useradmin.Uid)
                {
                    if (deleteEintragCommand == null)
                    {
                        deleteEintragCommand = new Command((e) =>
                        {
                            string Uid = (e as string);
                            if (Uid != useradmin.Uid)
                            {
                                delete(Uid);
                            }
                        }
                        );
                    }
                }
                return deleteEintragCommand;
            }
        }

        async void delete(string Uid)
        {
            bool answer = await DisplayAlert("Xoá thành viên", "Bạn có muốn xoá không?", "Có", "Không");
            if(answer)
            {
                sync.DeleteMember(table.tableId, Uid);
                await Navigation.PushAsync(new EditTablePage(table));
            }
        }

        private async void deleteTable_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Xoá thành viên", "Bạn có muốn xoá không?", "Có", "Không");
            if (answer)
            {
                sync.DeleteTable(table.tableId);
                Application.Current.MainPage = new MainPage(user.Uid);
            }
        }
    }
}