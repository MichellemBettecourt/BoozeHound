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
	public partial class FilterBeerPopup : ContentView
	{
        private EventHandler onCancel;
        private EventHandler<string> onOk;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }
        public event EventHandler<string> OnOk { add { onOk += value; } remove { onOk -= value; } }

        public FilterBeerPopup ()
		{
			InitializeComponent ();

            pickBrewery.ItemsSource = DataAccess.GetBreweries();
            pickStyle.ItemsSource = DataAccess.GetBeerStyles();

            pickBrewery.SelectedIndex = 0;
            pickStyle.SelectedIndex = 0;
            pickOrderField.SelectedIndex = 0;
            pickOrder.SelectedIndex = 0;
		}

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void BtnOk_Clicked(object sender, EventArgs e)
        {
            IsVisible = false;

            string query = "SELECT * FROM Beers";
            string where = " WHERE ";
            string order = string.Empty;
            List<string> filterArgs = new List<string>();

            if (switchBrewery.IsToggled)
                filterArgs.Add("Brewery = '" + pickBrewery.SelectedItem.ToString() + "'");

            if (switchStyle.IsToggled)
                filterArgs.Add("Style = '" + pickStyle.SelectedItem.ToString() + "'");

            if (switchRating.IsToggled)
            {
                if (swRatingOrHigher.IsToggled)
                    filterArgs.Add("Rating >= '" + pickRating.SelectedItem.ToString() + "'");
                else if (swRatingOrLower.IsToggled)
                    filterArgs.Add("Rating <= '" + pickRating.SelectedItem.ToString() + "'");
                else
                    filterArgs.Add("Rating = '" + pickRating.SelectedItem.ToString() + "'");
            }

            if (switchAbv.IsToggled && !string.IsNullOrEmpty(txtAbv.Text))
            {
                if (swAbvOrHigher.IsToggled)
                    filterArgs.Add("ABV >= '" + txtAbv.Text + "'");
                else if (swAbvOrLower.IsToggled)
                    filterArgs.Add("ABV <= '" + txtAbv.Text + "'");
                else
                    filterArgs.Add("ABV = '" + txtAbv.Text + "'");
            }

            if (switchDate.IsToggled)
            {
                if (swDateOrLater.IsToggled)
                    filterArgs.Add("Date >= '" + datePicker1.Date.ToShortDateString() + "'");
                else if (swDateOrEarlier.IsToggled)
                    filterArgs.Add("Date <= '" + datePicker1.Date.ToShortDateString() + "'");
                else
                    filterArgs.Add("Date = '" + datePicker1.Date.ToShortDateString() + "'");
            }

            if (swNotes.IsToggled && !string.IsNullOrEmpty(txtNotes.Text))
            {
                filterArgs.Add("Notes LIKE '%" + txtNotes.Text + "%'");
            }

            if (swOrderBy.IsToggled)
                order = " ORDER BY " + pickOrderField.SelectedItem + " " + (pickOrder.SelectedIndex == 0 ? "ASC" : "DESC");

            if (filterArgs.Count == 0)
                where = string.Empty;

            foreach (string arg in filterArgs)
            {
                where += arg;

                if (arg != filterArgs.Last())
                    where += " AND ";
            }

            onOk?.Invoke(sender, query + where + order + ";");
        }

        private void SwitchBrewery_Toggled(object sender, ToggledEventArgs e)
        {
            lblBrewery.IsEnabled = e.Value;
            pickBrewery.IsEnabled = e.Value;
        }

        private void SwitchStyle_Toggled(object sender, ToggledEventArgs e)
        {
            lblStyle.IsEnabled = e.Value;
            pickStyle.IsEnabled = e.Value;
        }

        private void SwitchRating_Toggled(object sender, ToggledEventArgs e)
        {
            lblRating.IsEnabled = e.Value;
            pickRating.IsEnabled = e.Value;
            swRatingOrHigher.IsEnabled = e.Value;
            lblRatingOrHigher.IsEnabled = e.Value;
            swRatingOrLower.IsEnabled = e.Value;
            lblRatingOrLower.IsEnabled = e.Value;
        }

        private void SwitchAbv_Toggled(object sender, ToggledEventArgs e)
        {
            lblAbv.IsEnabled = e.Value;
            txtAbv.IsEnabled = e.Value;
            swAbvOrHigher.IsEnabled = e.Value;
            lblAbvOrHigher.IsEnabled = e.Value;
            swAbvOrLower.IsEnabled = e.Value;
            lblAbvOrLower.IsEnabled = e.Value;
        }

        private void SwitchDate_Toggled(object sender, ToggledEventArgs e)
        {
            lblDate.IsEnabled = e.Value;
            datePicker1.IsEnabled = e.Value;
            swDateOrLater.IsEnabled = e.Value;
            lblDateOrLater.IsEnabled = e.Value;
            swDateOrEarlier.IsEnabled = e.Value;
            lblDateOrEarlier.IsEnabled = e.Value;
        }

        private void SwitchOrHigher_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swRatingOrLower.IsToggled = false;
        }

        private void SwitchOrLower_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swRatingOrHigher.IsToggled = false;
        }

        private void SwAbvOrHigher_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swAbvOrLower.IsToggled = false;
        }

        private void SwAbvOrLower_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swAbvOrHigher.IsToggled = false;
        }

        private void SwDateOrLater_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swDateOrEarlier.IsToggled = false;
        }

        private void SwDateOrEarlier_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swDateOrLater.IsToggled = false;
        }

        private void SwOrderBy_Toggled(object sender, ToggledEventArgs e)
        {
            lblOrderBy.IsEnabled = e.Value;
            pickOrderField.IsEnabled = e.Value;
            pickOrder.IsEnabled = e.Value;
        }

        private void SwNotes_Toggled(object sender, ToggledEventArgs e)
        {
            lblNotes.IsEnabled = e.Value;
            txtNotes.IsEnabled = e.Value;
        }
    }
}