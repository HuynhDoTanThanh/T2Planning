using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Views.Create;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconPlus : AbsoluteLayout
    {
        public IconPlus()
        {
            InitializeComponent();
        }
        private bool isOpen = false;
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                await FloatingActionButton.RotateTo(180, 300);
                FloatingActionButton.Rotation = 0;
                FloatingActionButton.Text = "−";
                isOpen = true;

                FloatMenuItem1.IsVisible = true;
                await FloatButtonItem1.ScaleTo(1, 150);
                FloatMenuItem2.IsVisible = true;
                await FloatButtonItem2.ScaleTo(1, 150);
            }
            else
            {
                await FloatingActionButton.RotateTo(180, 300);
                FloatingActionButton.Rotation = 0;
                FloatingActionButton.Text = "+";
                isOpen = false;


                await FloatButtonItem1.ScaleTo(0.01, 150);
                FloatMenuItem1.IsVisible = false;

                await FloatButtonItem2.ScaleTo(0.01, 150);
                FloatMenuItem2.IsVisible = false;
            }
        }


        private async void FloatMenuItem2Tap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTable());
        }

        private async void FloatMenuItem1Tap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateCard());
        }
    }
}