using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickAdd_Beer_PU : ContentView
    {
        private EventHandler onCancel;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }

        public QuickAdd_Beer_PU()
        {
            InitializeComponent();
        }

        private void BtnCancelQABeer_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void BtnSaveQABeer_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QABeer_Name.Text))
            {
                App.Current.MainPage.DisplayAlert("", "Name required.", "Ok");
                return;
            }

            if (beerRating.Rating == 0)
            {
                App.Current.MainPage.DisplayAlert("", "Rating required.", "Ok");
                return;
            }

            Beer add = new Beer() { Name = QABeer_Name.Text, Rating = beerRating.Rating, Brewery = QABeer_Brewery.Text};
            DataAccess.SaveBeer(add);
            IsVisible = false;
            App.Current.MainPage.DisplayAlert("", add.Name + " saved to database.", "Ok");
        }
    }
}
