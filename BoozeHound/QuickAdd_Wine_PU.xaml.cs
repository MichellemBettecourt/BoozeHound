using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickAdd_Wine_PU : ContentView
    {
        private EventHandler onCancel;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }

        public QuickAdd_Wine_PU()
        {
            InitializeComponent();
        }

        private void ClearForm()
        {
            QAWine_Name.Text = string.Empty;
            QAWine_Winery.Text = string.Empty;
            wineRating.SetRating(0);
        }

        private void BtnCancelQAWine_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
            ClearForm();
        }

        private void BtnSaveQAWine_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QAWine_Name.Text))
            {
                App.Current.MainPage.DisplayAlert("", "Name required.", "Ok");
                return;
            }

            if (wineRating.Rating == 0)
            {
                App.Current.MainPage.DisplayAlert("", "Rating required.", "Ok");
                return;
            }

            Wine add = new Wine() { Name = QAWine_Name.Text, Rating = wineRating.Rating, Winery = QAWine_Winery.Text };
            DataAccess.SaveWine(add);
            IsVisible = false;
            ClearForm();
            App.Current.MainPage.DisplayAlert("", add.Name + " saved to database.", "Ok");
        }
    }
}
