using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Media;
using System.IO;

namespace BoozeHound
{
    public partial class FullAdd_Beer : ContentPage
    {
        private bool isNew;
        private Beer beer;

        public FullAdd_Beer()
        {
            InitializeComponent();
            isNew = true;
            beer = new Beer();
            BeerImage.Source = ImageSource.FromResource("BoozeHound.beer_bottle.png");
        }

        public FullAdd_Beer(Beer b)
        {
            InitializeComponent();
            isNew = false;
            beer = b;

            Beer_Name.Text = beer.Name;
            beerRating.SetRating(beer.Rating);
            Beer_ABV.Text = beer.ABV?.ToString();
            Beer_Brewery.Text = beer.Brewery;
            Beer_Style.Text = beer.Style;
            Beer_Notes.Text = beer.Notes;

            DateLabel.IsVisible = true;
            DateLabel.Text = $"{beer.Name} added on {beer.Date.ToShortDateString()}";

            if (!string.IsNullOrEmpty(beer.ImagePath))
            {
                BeerImage.Source = ImageSource.FromFile(beer.ImagePath);
            }
            else
            {
                BeerImage.Source = ImageSource.FromResource("BoozeHound.beer_bottle.png");
            }
        }

        private void BtnCancelQABeer_Clicked(object sender, EventArgs e)
        {
            if (isNew)
                App.Current.MainPage = new MainPage();
            else
                App.Current.MainPage = new BeersPage();
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

            double? abv = Convert.ToDouble(Beer_ABV?.Text);

            beer.Name = Beer_Name.Text;
            beer.Brewery = Beer_Brewery.Text;
            beer.Rating = beerRating.Rating;
            beer.Style = Beer_Style.Text;
            beer.ABV = string.IsNullOrEmpty(Beer_ABV.Text) ? beer.ABV : Convert.ToDouble(Beer_ABV.Text);
            beer.Notes = Beer_Notes.Text;

            if (isNew)
            {
                DataAccess.SaveBeer(beer);
                App.Current.MainPage.DisplayAlert("", beer.Name + " saved to database.", "Ok");
            }
            else
            {
                DataAccess.UpdateBeer(beer);
                App.Current.MainPage.DisplayAlert("", beer.Name + " has been updated.", "Ok");
            }

            
            App.Current.MainPage = new MainPage();
        }

        private void Beer_Notes_SizeChanged(object sender, EventArgs e)
        {
            double height = 0;

            // Add up height of controls
            height += Beer_Name.Height;
            height += Beer_Brewery.Height;
            height += beerRating.Height;
            height += Beer_Style.Height;
            height += Beer_ABV.Height;
            height += Beer_Notes.Height;

            // Add padding between controls
            height += (5 * 10);

            // Set control height
            beerForm.HeightRequest = height;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    Name = DateTime.Now.ToString() + ".jpg",
                    Directory = "Beer"
                });

                if (photo != null)
                {
                    // Delete old image if retaking photo
                    if (!string.IsNullOrEmpty(beer.ImagePath))
                        File.Delete(beer.ImagePath);

                    BeerImage.Source = ImageSource.FromFile(photo.Path);
                    beer.ImagePath = photo.Path;
                }
            }
        }
    }
}
