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
		}

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}