using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickAdd_List_PU : ContentView
    {
        private EventHandler onCancel;
        private EventHandler onBeer;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }
        public event EventHandler OnBeer { add { onBeer += value; } remove { onBeer -= value; } }

        public QuickAdd_List_PU()
        {
            InitializeComponent ();
        }

        private void BtnCancelQuickAdd_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }


        private void Quick_Add_Beer_Clicked(object sender, EventArgs e)
        {
            if (onBeer != null)
            {
                IsVisible = false;
                onBeer(sender, e);
            }

        }

        private void Quick_Add_Wine_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
           
        }

        private void Quick_Add_Spirits_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }

        private void Quick_Add_Beer_PU_Cancel(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            pu_Quick_Add_Beer.IsVisible = false;
        }
    }
}
