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
    public partial class ChooseAvatar : ContentPage
    {
        Database db = new Database();
        User user = new User();
        public ChooseAvatar()
        {
            InitializeComponent();
            user = db.GetUser()[0];
        }

        private void pickImage(object sender, EventArgs e)
        {
            if (ava1.IsPressed)
            {
                user.userAvatar = "a1.png";
            }
            else if (ava2.IsPressed)
            {
                user.userAvatar = "a2.png";
            }
            else if (ava3.IsPressed)
            {
                user.userAvatar = "a3.png";
            }
            else if (ava4.IsPressed)
            {
                user.userAvatar = "a4.png";
            }
            else if (ava5.IsPressed)
            {
                user.userAvatar = "a5.png";
            }
            else if (ava6.IsPressed)
            {
                user.userAvatar = "a6.png";
            }
            else if (ava7.IsPressed)
            {
                user.userAvatar = "a7.png";
            }
            else if (ava8.IsPressed)
            {
                user.userAvatar = "a8.png";
            }
            else if (ava9.IsPressed)
            {
                user.userAvatar = "a9.png";
            }
            else if (ava10.IsPressed)
            {
                user.userAvatar = "a10.png";
            }
            else if (ava11.IsPressed)
            {
                user.userAvatar = "a11.png";
            }
            else if (ava12.IsPressed)
            {
                user.userAvatar = "a12.png";
            }
            else if (ava13.IsPressed)
            {
                user.userAvatar = "a13.png";
            }
            else if (ava14.IsPressed)
            {
                user.userAvatar = "a14.png";
            }
            else if (ava15.IsPressed)
            {
                user.userAvatar = "a15.png";
            }
            else if (ava16.IsPressed)
            {
                user.userAvatar = "a16.png";
            }
            else if (ava17.IsPressed)
            {
                user.userAvatar = "a17.png";
            }
            else if (ava18.IsPressed)
            {
                user.userAvatar = "a18.png";
            }
            else if (ava19.IsPressed)
            {
                user.userAvatar = "a19.png";
            }
            else
            {
                user.userAvatar = "a0.png";
            }
            Sync sync = new Sync();
            sync.UpdateUser(user);
            db.resetUser();
            sync.PullUser(user.Uid);
            DisplayAlert("Thông báo", "Thay đổi ảnh đại diện thành công", "Ok");
            var nav = new MainPage(user);
            Application.Current.MainPage = nav;
        }
    }
}