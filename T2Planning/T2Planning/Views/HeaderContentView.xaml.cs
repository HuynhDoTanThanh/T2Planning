﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderContentView : ContentView
    {
        public HeaderContentView()
        {
           
            InitializeComponent();
        }

        private async void avatar_Clicked(object sender, EventArgs e)
        {
            
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new ChooseAvatar());
        }
    }
}