﻿using System;
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
    public partial class SignUpPage : ContentPage
    {
        IAuth auth;
        public SignUpPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            var torrent = auth.SignUpWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
            if (torrent != null)
            {
                User user = new User();
                string Uid = await torrent;

                user.Uid = Uid;
                user.userEmail = EmailInput.Text;
                user.userName = NameInput.Text;
                user.userAvatar = "a0.png";

                Sync sync = new Sync();
                sync.PushUserAsync(user);


                await DisplayAlert("Success", "New User Created", "Ok");
                var signout = auth.SignOut();

                if (signout)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("ERROR", "Something went wrong, plese try again", "Ok");
                }
            }
        }
    }
}