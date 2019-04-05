using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickAdd_Spirit_PU : ContentView
    {
        private EventHandler onCancel;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }

        public QuickAdd_Spirit_PU()
        {
            InitializeComponent();
        }

        private void ClearForm()
        {
            QASpirit_Name.Text = string.Empty;
            QASpirit_Distiller.Text = string.Empty;
            spiritRating.SetRating(0);
        }

        private void BtnCancelQASpirit_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
            ClearForm();
        }

        private void BtnSaveQASpirit_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QASpirit_Name.Text))
            {
                App.Current.MainPage.DisplayAlert("", "Name required.", "Ok");
                return;
            }

            if (spiritRating.Rating == 0)
            {
                App.Current.MainPage.DisplayAlert("", "Rating required.", "Ok");
                return;
            }

            Spirit add = new Spirit() { Name = QASpirit_Name.Text, Rating = spiritRating.Rating, Distiller = QASpirit_Distiller.Text };
            DataAccess.SaveSpirit(add);
            IsVisible = false;
            ClearForm();
            App.Current.MainPage.DisplayAlert("", add.Name + " saved to database.", "Ok");
        }
    }
}
