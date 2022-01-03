using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MyTable), typeof(MyTable));
            Routing.RegisterRoute(nameof(MyCard), typeof(MyCard));
            Routing.RegisterRoute(nameof(Setting), typeof(Setting));
        }

        public MainPage(User user)
        {
            InitializeComponent();
        }
    }
}