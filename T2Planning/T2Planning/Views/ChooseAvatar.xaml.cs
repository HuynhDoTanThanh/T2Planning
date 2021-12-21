using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseAvatar : ContentPage
    {
        public ChooseAvatar()
        {
            InitializeComponent();
        }

        private void pickImage(object sender, EventArgs e)
        {

           if (ava1.IsPressed)
           {
                DisplayAlert("tb", "abc", "ok");
           }
            
        }
    }
}