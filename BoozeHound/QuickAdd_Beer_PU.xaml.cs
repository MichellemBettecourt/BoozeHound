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
            Double rating = Convert.ToDouble(QABeer_Rating.Text);
            Beer add = new Beer() { Name = QABeer_Name.Text, Rating = rating, Brewery = QABeer_Brewery.Text};
            DataAccess.SaveBeer(add);
            IsVisible = false;
        }
    }
}
