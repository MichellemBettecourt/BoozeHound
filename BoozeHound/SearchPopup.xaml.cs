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
	public partial class SearchPopup : ContentView
	{
        private EventHandler onCancel;
        private EventHandler<string> onOk;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }
        public event EventHandler<string> OnOk { add { onOk += value; } remove { onOk -= value; } }

		public SearchPopup ()
		{
			InitializeComponent ();
		}

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void BtnOk_Clicked(object sender, EventArgs e)
        {
            if (onOk != null && !string.IsNullOrEmpty(txtSearch.Text))
            {
                IsVisible = false;
                onOk(sender, txtSearch.Text);
            }
        }

        private void TxtSearch_Completed(object sender, EventArgs e)
        {
            if (onOk != null && !string.IsNullOrEmpty(txtSearch.Text))
            {
                IsVisible = false;
                onOk(sender, txtSearch.Text);
            }
        }
    }
}