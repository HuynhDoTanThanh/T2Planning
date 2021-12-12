using System;
using System.Collections.Generic;
using T2Planning.Views;
using Xamarin.Forms;

namespace T2Planning
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MyTable), typeof(MyTable));
            Routing.RegisterRoute(nameof(MyCard), typeof(MyCard));
            Routing.RegisterRoute(nameof(Setting), typeof(Setting));
        }

    }
}
