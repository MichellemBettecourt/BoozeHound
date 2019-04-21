using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Media;
using System.IO;

namespace BoozeHound
{
    public partial class FullAdd_Spirit : ContentPage
    {
        private bool isNew;
        private Spirit spirit;
        private TapGestureRecognizer tap = new TapGestureRecognizer();

        public FullAdd_Spirit()
        {
            InitializeComponent();
            BGImage.Source = ImageSource.FromResource("BoozeHound.spirit_background2.png");
            isNew = true;
            spirit = new Spirit();
            SpiritImage.Source = ImageSource.FromResource("BoozeHound.spirit_bottle.png");
            tap.Tapped += Image_Tapped;
        }

        public FullAdd_Spirit(Spirit b)
        {
            InitializeComponent();
            BGImage.Source = ImageSource.FromResource("BoozeHound.spirit_background2.png");
            isNew = false;
            spirit = b;

            Spirit_Name.Text = spirit.Name;
            spiritRating.SetRating(spirit.Rating);
            Spirit_ABV.Text = spirit.ABV?.ToString();
            Spirit_Distiller.Text = spirit.Distiller;
            Spirit_Type.Text = spirit.Type;
            Spirit_Age.Text = spirit.Age?.ToString();
            Spirit_Notes.Text = spirit.Notes;

            DateLabel.IsVisible = true;
            DateLabel.Text = $"{spirit.Name} added on {spirit.Date.ToShortDateString()}";

            tap.Tapped += Image_Tapped;

            if (!string.IsNullOrEmpty(spirit.ImagePath))
            {
                SpiritImage.Source = ImageSource.FromFile(spirit.ImagePath);
                SpiritImage.GestureRecognizers.Add(tap);
                imagePopup.SetImage(spirit.ImagePath);
            }
            else
            {
                SpiritImage.Source = ImageSource.FromResource("BoozeHound.spirit_bottle.png");
            }
        }

        ~FullAdd_Spirit()
        {
            SpiritImage.GestureRecognizers.Clear();
            tap.Tapped -= Image_Tapped;
        }

        private void Image_Tapped(object sender, EventArgs e)
        {
            imagePopup.IsVisible = true;
        }

        private void BtnCancelQASpirit_Clicked(object sender, EventArgs e)
        {
            if (isNew)
                App.Current.MainPage = new MainPage();
            else
                App.Current.MainPage = new SpiritsPage();
        }

        private void BtnSaveQASpirit_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Spirit_Name.Text))
            {
                App.Current.MainPage.DisplayAlert("", "Name required.", "Ok");
                return;
            }

            if (spiritRating.Rating == 0)
            {
                App.Current.MainPage.DisplayAlert("", "Rating required.", "Ok");
                return;
            }

            double? abv = Convert.ToDouble(Spirit_ABV?.Text);

            spirit.Name = Spirit_Name.Text;
            spirit.Distiller = Spirit_Distiller.Text;
            spirit.Rating = spiritRating.Rating;
            spirit.Type = Spirit_Type.Text;
            spirit.Age = string.IsNullOrEmpty(Spirit_Age.Text) ? spirit.Age : Convert.ToInt16(Spirit_Age.Text);
            spirit.ABV = string.IsNullOrEmpty(Spirit_ABV.Text) ? spirit.ABV : Convert.ToDouble(Spirit_ABV.Text);
            spirit.Notes = Spirit_Notes.Text;

            if (isNew)
            {
                DataAccess.SaveSpirit(spirit);
                App.Current.MainPage.DisplayAlert("", spirit.Name + " saved to database.", "Ok");
            }
            else

            {
                DataAccess.UpdateSpirit(spirit);
                App.Current.MainPage.DisplayAlert("", spirit.Name + " has been updated.", "Ok");
            }


            App.Current.MainPage = new MainPage();
        }

        private void Spirit_Notes_SizeChanged(object sender, EventArgs e)
        {
            double height = 0;

            // Add up height of controls
            height += Spirit_Name.Height;
            height += Spirit_Distiller.Height;
            height += spiritRating.Height;
            height += Spirit_Type.Height;
            height += Spirit_Age.Height;
            height += Spirit_ABV.Height;
            height += Spirit_Notes.Height;

            // Add padding between controls
            height += (5 * 10);

            // Set control height
            spiritForm.HeightRequest = height;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    Name = DateTime.Now.ToString() + ".jpg",
                    Directory = "Spirit"
                });

                if (photo != null)
                {
                    // Delete old image if retaking photo
                    if (!string.IsNullOrEmpty(spirit.ImagePath))
                        File.Delete(spirit.ImagePath);

                    SpiritImage.Source = ImageSource.FromFile(photo.Path);
                    spirit.ImagePath = photo.Path;
                    imagePopup.SetImage(photo.Path);

                    if (SpiritImage.GestureRecognizers.Count == 0)
                        SpiritImage.GestureRecognizers.Add(tap);
                }
            }
        }
    }
}
