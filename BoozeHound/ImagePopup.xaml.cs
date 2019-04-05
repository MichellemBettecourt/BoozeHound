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
	public partial class ImagePopup : ContentView
	{
		public ImagePopup ()
		{
			InitializeComponent ();
		}

        public void SetImage(string path)
        {
            LargeImage.Source = ImageSource.FromFile(path);
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }
    }
}