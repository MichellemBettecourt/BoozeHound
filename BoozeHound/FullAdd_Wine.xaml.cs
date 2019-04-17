using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Media;
using System.IO;

namespace BoozeHound
{
    public partial class FullAdd_Wine : ContentPage
    {
        private bool isNew;
        private Wine wine;
        private TapGestureRecognizer tap = new TapGestureRecognizer();

        public FullAdd_Wine()
        {
            InitializeComponent();
            isNew = true;
            wine = new Wine();
            WineImage.Source = ImageSource.FromResource("BoozeHound.wine_bottle.png");
            tap.Tapped += Image_Tapped;
        }

        public FullAdd_Wine(Wine b)
        {
            InitializeComponent();
            isNew = false;
            wine = b;

            Wine_Name.Text = wine.Name;
            wineRating.SetRating(wine.Rating);
            Wine_ABV.Text = wine.ABV?.ToString();
            Wine_Winery.Text = wine.Winery;
            Wine_Type.Text = wine.Type;
            Wine_Vintage.Text = wine.Vintage?.ToString();
            Wine_Notes.Text = wine.Notes;

            DateLabel.IsVisible = true;
            DateLabel.Text = $"{wine.Name} added on {wine.Date.ToShortDateString()}";

            tap.Tapped += Image_Tapped;

            if (!string.IsNullOrEmpty(wine.ImagePath))
            {
                WineImage.Source = ImageSource.FromFile(wine.ImagePath);
                WineImage.GestureRecognizers.Add(tap);
                imagePopup.SetImage(wine.ImagePath);
            }
            else
            {
                WineImage.Source = ImageSource.FromResource("BoozeHound.wine_bottle.png");
            }
        }

        ~FullAdd_Wine()
        {
            WineImage.GestureRecognizers.Clear();
            tap.Tapped -= Image_Tapped;
        }

        private void Image_Tapped(object sender, EventArgs e)
        {
            imagePopup.IsVisible = true;
        }

        private void BtnCancelQAWine_Clicked(object sender, EventArgs e)
        {
            if (isNew)
                App.Current.MainPage = new MainPage();
            else
                App.Current.MainPage = new WinesPage();
        }

        private void BtnSaveQAWine_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Wine_Name.Text))
            {
                App.Current.MainPage.DisplayAlert("", "Name required.", "Ok");
                return;
            }

            if (wineRating.Rating == 0)
            {
                App.Current.MainPage.DisplayAlert("", "Rating required.", "Ok");
                return;
            }

            double? abv = Convert.ToDouble(Wine_ABV?.Text);

            wine.Name = Wine_Name.Text;
            wine.Winery = Wine_Winery.Text;
            wine.Rating = wineRating.Rating;
            wine.Type = Wine_Type.Text;
            wine.Vintage = string.IsNullOrEmpty(Wine_Vintage.Text) ? wine.Vintage : Convert.ToInt16(Wine_Vintage.Text);
            wine.ABV = string.IsNullOrEmpty(Wine_ABV.Text) ? wine.ABV : Convert.ToDouble(Wine_ABV.Text);
            wine.Notes = Wine_Notes.Text;

            if (isNew)
            {
                DataAccess.SaveWine(wine);
                App.Current.MainPage.DisplayAlert("", wine.Name + " saved to database.", "Ok");
            }
            else
            {
                DataAccess.UpdateWine(wine);
                App.Current.MainPage.DisplayAlert("", wine.Name + " has been updated.", "Ok");
            }


            App.Current.MainPage = new MainPage();
        }

        private void Wine_Notes_SizeChanged(object sender, EventArgs e)
        {
            double height = 0;

            // Add up height of controls
            height += Wine_Name.Height;
            height += Wine_Winery.Height;
            height += wineRating.Height;
            height += Wine_Type.Height;
            height += Wine_Vintage.Height;
            height += Wine_ABV.Height;
            height += Wine_Notes.Height;

            // Add padding between controls
            height += (5 * 10);

            // Set control height
            wineForm.HeightRequest = height;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    Name = DateTime.Now.ToString() + ".jpg",
                    Directory = "Wine"
                });

                if (photo != null)
                {
                    // Delete old image if retaking photo
                    if (!string.IsNullOrEmpty(wine.ImagePath))
                        File.Delete(wine.ImagePath);

                    WineImage.Source = ImageSource.FromFile(photo.Path);
                    wine.ImagePath = photo.Path;
                    imagePopup.SetImage(photo.Path);

                    if (WineImage.GestureRecognizers.Count == 0)
                        WineImage.GestureRecognizers.Add(tap);
                }
            }
        }
    }
}

