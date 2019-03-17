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
	}
}