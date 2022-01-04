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
    public partial class MainPage : Shell
    {
        public MainPage(string Uid)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MyTable), typeof(MyTable));
            Routing.RegisterRoute(nameof(MyCard), typeof(MyCard));
            Routing.RegisterRoute(nameof(Setting), typeof(Setting));
            init(Uid);
        }

        void init(string Uid)
        {
            Sync sync = new Sync();
            sync.PullDB(Uid);
        }

        public MainPage(User user)
        {
            InitializeComponent();
        }
    }
}