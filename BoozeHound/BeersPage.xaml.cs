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
            DataAccess.AddTestBeers();
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
    }
}