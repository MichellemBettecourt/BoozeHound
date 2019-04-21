using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoozeHound
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BeersPage : ContentPage
	{
		public BeersPage ()
		{
			InitializeComponent ();
            BeerList.ItemsSource = DataAccess.GetBeers();
        }

        private void popSearch_Ok(object sender, string name)
        {
            BeerList.ItemsSource = DataAccess.SearchBeers(name);
        }

        private void popSearch_Cancel(object sender, EventArgs e)
        {
            popSearch.IsVisible = false;
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            popSearch.IsVisible = true;
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private void BtnFilter_Clicked(object sender, EventArgs e)
        {
            popFilter.IsVisible = true;
        }

        private void PopFilter_OnOk(object sender, string e)
        {
            BeerList.ItemsSource = DataAccess.GetBeers(e);
        }

        private async void BeerList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string action = await DisplayActionSheet("", "Cancel", "Delete", "View");

            Beer beer = (Beer)e.Item;

            if (action == "Delete")
            {
                bool delete = await DisplayAlert("Delete Beer", $"Are you sure you want to permanently delete {beer.Name}", "Yes", "No");
                if (delete)
                {
                    DataAccess.DeleteBeer(beer);
                    BeerList.ItemsSource = DataAccess.GetBeers();
                    await DisplayAlert("", $"{beer.Name} has been deleted.", "Ok");
                }
            }
            else if (action == "View")
            {
                App.Current.MainPage = new FullAdd_Beer(beer);
            }
        }
    }
}