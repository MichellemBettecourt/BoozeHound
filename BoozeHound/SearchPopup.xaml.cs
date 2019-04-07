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

        public string Title { get; set; }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: "Title",
            returnType: typeof(string),
            declaringType: typeof(SearchPopup),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SearchTitleChanged);

		public SearchPopup ()
		{
			InitializeComponent ();
		}

        private static void SearchTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SearchPopup)bindable;
            control.lblTitle.Text = newValue.ToString();
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
            txtSearch.Text = string.Empty;
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
            string name = txtSearch.Text;

            if (onOk != null && !string.IsNullOrEmpty(name))
            {
                IsVisible = false;
                txtSearch.Text = string.Empty;
                onOk(sender, name);
            }
        }
    }
}