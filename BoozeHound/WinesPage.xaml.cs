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
	public partial class WinesPage : ContentPage
	{
		public WinesPage ()
		{
			InitializeComponent ();
            WineList.ItemsSource = DataAccess.GetWines();
		}

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private async void WineList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string action = await DisplayActionSheet("", "Cancel", "Delete", "View");

            Wine wine = (Wine)e.Item;

            if (action == "View")
            {
                App.Current.MainPage = new FullAdd_Wine(wine);
            }
            else if (action == "Delete")
            {
                DataAccess.DeleteWine(wine);
                WineList.ItemsSource = DataAccess.GetWines();
                await DisplayAlert("", $"{wine.Name} has been deleted.", "Ok");
            }
        }

        private void BtnFilter_Clicked(object sender, EventArgs e)
        {
            WineFilter.IsVisible = true;
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            WineSearch.IsVisible = true;
        }

        private void WineSearch_OnOk(object sender, string name)
        {
            WineList.ItemsSource = DataAccess.SearchWines(name);
        }

        private void WineFilter_OnOk(object sender, string query)
        {
            WineList.ItemsSource = DataAccess.GetWines(query);
        }
    }
}