using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BoozeHound
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DataAccess.AddTestBeers();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            App.Current.MainPage = new BeersPage();
        }

        /********************************************************************/

        private void Full_Add_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            pu_Full_Add_List.IsVisible = true;
        }

        private void Full_Add_List_PU_Ok(object sender, string name)
        {
            pu_Full_Add_List.IsVisible = true;
        }

        private void Full_Add_List_PU_Cancel(object sender, EventArgs e)
        {
            pu_Full_Add_List.IsVisible = true;
        }

        /********************************************************************/

        private async void Quick_Add_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            //pu_Quick_Add_List.IsVisible = true;

            string action = await DisplayActionSheet("Quick Add", "Cancel", null, "Beer", "Wine", "Spirit");

            if (action == "Beer")
            {
                pu_Quick_Add_Beer.IsVisible = true;
            }
        }

        private void Quick_Add_List_PU_Cancel(object sender, EventArgs e)
        {
            pu_Quick_Add_List.IsVisible = true;
        }

        private void Quick_Add_Beer(object sender, EventArgs e)
        {
            pu_Quick_Add_Beer.IsVisible = true;
        }

        private void Quick_Add_Beer_PU_Cancel(object sender, EventArgs e)
        {
            pu_Quick_Add_Beer.IsVisible = false;
        }


        /********************************************************************/
        private async void Booze_List_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            //pu_Quick_Add_List.IsVisable = true;
            //pu_Your_Booze_List.IsVisible = true;
            string action = await DisplayActionSheet("Your Booze", "Cancel", null, "Beer", "Wine", "Spirit");
            
            if (action == "Beer")
            {
                App.Current.MainPage = new BeersPage();
            }
            else if (action == "Wine")
            {

            }
            else if (action == "Spirit")
            {

            }
        }

        private void Your_Booze_List_PU_Ok(object sender, string name)
        {
            pu_Your_Booze_List.IsVisible = true;
        }

        private void Your_Booze_List_PU_Cancel(object sender, EventArgs e)
        {
            pu_Your_Booze_List.IsVisible = true;
        }

        /********************************************************************/

    }
}
