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
	public partial class FilterSpiritPopup : ContentView
	{
        private EventHandler onCancel;
        private EventHandler<string> onOk;

        public event EventHandler OnCancel { add { onCancel += value; } remove { onCancel -= value; } }
        public event EventHandler<string> OnOk { add { onOk += value; } remove { onOk -= value; } }

        public FilterSpiritPopup()
		{
			InitializeComponent ();

            pickDistiller.ItemsSource = DataAccess.GetDistillers();
            pickType.ItemsSource = DataAccess.GetSpiritTypes();

            pickDistiller.SelectedIndex = 0;
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

        private void AgeOrOlder_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                AgeOrYounger.IsToggled = false;
        }

        private void AgeOrYounger_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                AgeOrOlder.IsToggled = false;
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            this.IsVisible = false;
        }

        private void BtnOk_Clicked(object sender, EventArgs e)
        {
            this.IsVisible = false;

            string query = "SELECT * FROM Spirits";
            string where = " WHERE ";
            string order = string.Empty;
            List<string> filterArgs = new List<string>();

            if (switchDistiller.IsToggled)
                filterArgs.Add("Distiller = '" + pickDistiller.SelectedItem.ToString() + "'");

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

            if (AgeSwitch.IsToggled && !string.IsNullOrEmpty(AgeEntry.Text))
            {
                if (AgeOrOlder.IsToggled)
                    filterArgs.Add($"Age >= '{AgeEntry.Text}'");
                else if (AgeOrYounger.IsToggled)
                    filterArgs.Add($"Age <= '{AgeEntry.Text}'");
                else
                    filterArgs.Add($"Age = '{AgeEntry.Text}'");
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
    }
}