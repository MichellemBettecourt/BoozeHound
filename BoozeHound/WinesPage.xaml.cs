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

            if (action == "View")
            {

            }
            else if (action == "Delete")
            {

            }
        }

        private void BtnFilter_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            WineSearch.IsVisible = true;
        }

        private void WineSearch_OnOk(object sender, string name)
        {
            WineList.ItemsSource = DataAccess.SearchWines(name);
        }
    }
}