using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    public partial class FullAdd_Beer : ContentPage
    {
        public FullAdd_Beer()
        {
            InitializeComponent();
        }
        private void BtnCancelQABeer_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void BtnSaveQABeer_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Beer_Name.Text))
            {
                App.Current.MainPage.DisplayAlert("", "Name required.", "Ok");
                return;
            }

            if (beerRating.Rating == 0)
            {
                App.Current.MainPage.DisplayAlert("", "Rating required.", "Ok");
                return;
            }
            double abv = Convert.ToDouble(Beer_ABV.Text);
            Beer add = new Beer()
            {
                Name = Beer_Name.Text,
                Rating = beerRating.Rating,
                Brewery = Beer_Brewery.Text,
                Style = Beer_Style.Text,
                ABV = abv,
                Notes = Beer_Name.Text
            };
            DataAccess.SaveBeer(add);
            App.Current.MainPage.DisplayAlert("", add.Name + " saved to database.", "Ok");
            App.Current.MainPage = new MainPage();
        }

    }
}
