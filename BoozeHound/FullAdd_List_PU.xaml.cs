using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullAdd_List_PU : ContentView
    {
        private EventHandler onCancel;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }

        public FullAdd_List_PU()
        {
            InitializeComponent();
        }

        private void BtnCancelFullAdd_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void Full_Add_Beer_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }

        private void Full_Add_Wine_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }

        private void Full_Add_Spirits_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }
    }
}



