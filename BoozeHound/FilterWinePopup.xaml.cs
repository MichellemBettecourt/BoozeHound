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
	public partial class FilterWinePopup : ContentView
	{
        private EventHandler onCancel;
        private EventHandler<string> onOk;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }
        public event EventHandler<string> OnOk { add { onOk += value; } remove { onOk -= value; } }

        public FilterWinePopup ()
		{
			InitializeComponent ();

            pickWinery.ItemsSource = DataAccess.GetWineries();
            pickType.ItemsSource = DataAccess.GetWineTypes();

            pickWinery.SelectedIndex = 0;
            pickType.SelectedIndex = 0;
            pickOrderField.SelectedIndex = 0;
            pickOrder.SelectedIndex = 0;
        }

        private void SwRatingOrHigher_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                swRatingOrLower.IsToggled = false;
        }

        private void SwRatingOrLower_Toggled(object sender, ToggledEventArgs e)
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

        private void VintageOrLater_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                VintageOrEarlier.IsToggled = false;
        }

        private void VintageOrEarlier_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                VintageOrLater.IsToggled = false;
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

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            this.IsVisible = false;
        }

        private void BtnOk_Clicked(object sender, EventArgs e)
        {
            this.IsVisible = false;

            string query = "SELECT * FROM Wines";
            string where = " WHERE ";
            string order = string.Empty;
            List<string> filterArgs = new List<string>();

            if (switchWinery.IsToggled)
                filterArgs.Add("Winery = '" + pickWinery.SelectedItem.ToString() + "'");

            if (switchType.IsToggled)
                filterArgs.Add("Type = '" + pickType.SelectedItem.ToString() + "'");

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

            if (VintageSwitch.IsToggled && !string.IsNullOrEmpty(VintageEntry.Text))
            {
                if (VintageOrLater.IsToggled)
                    filterArgs.Add($"Vintage >= '{VintageEntry.Text}'");
                else if (VintageOrEarlier.IsToggled)
                    filterArgs.Add($"Vintage <= '{VintageEntry.Text}'");
                else
                    filterArgs.Add($"Vintage = '{VintageEntry.Text}'");
            }

            if (switchDate.IsToggled)
            {
                if (swDateOrLater.IsToggled)
                    filterArgs.Add("Date >= '" + datePicker1.Date + "'");
                else if (swDateOrEarlier.IsToggled)
                    filterArgs.Add("Date <= '" + datePicker1.Date + "'");
                else
                    filterArgs.Add("Date = '" + datePicker1.Date + "'");
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
    }
}