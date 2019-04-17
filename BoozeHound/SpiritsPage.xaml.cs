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
	public partial class SpiritsPage : ContentPage
	{
		public SpiritsPage ()
		{
			InitializeComponent ();
            SpiritList.ItemsSource = DataAccess.GetSpirits();
		}

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private async void SpiritList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string action = await DisplayActionSheet("", "Cancel", "Delete", "View");

            Spirit spirit = (Spirit)e.Item;

            if (action == "View")
            {
                App.Current.MainPage = new FullAdd_Spirit(spirit);
            }
            else if (action == "Delete")
            {
                DataAccess.DeleteSpirit(spirit);
                await DisplayAlert("", $"{spirit.Name} has been deleted.", "Ok");
                SpiritList.ItemsSource = DataAccess.GetSpirits();
            }
        }

        private void SpiritSearch_OnOk(object sender, string name)
        {
            SpiritList.ItemsSource = DataAccess.SearchBeers(name);
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            SpiritSearch.IsVisible = true;
        }

        private void SpiritFilter_OnOk(object sender, string query)
        {
            SpiritList.ItemsSource = DataAccess.GetSpirits(query);
        }

        private void BtnFilter_Clicked(object sender, EventArgs e)
        {
            SpiritFilter.IsVisible = true;
        }
    }
}