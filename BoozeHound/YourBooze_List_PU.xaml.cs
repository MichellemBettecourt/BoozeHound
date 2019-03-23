using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourBooze_List_PU : ContentView
    {
        private EventHandler onCancel;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }

        public YourBooze_List_PU()
        {
            InitializeComponent();
        }

        private void BtnCancelYourBooze_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void Your_Booze_Beer_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }

        private void Your_Booze_Wine_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }

        private void Your_Booze_Spirits_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }
    }
}

