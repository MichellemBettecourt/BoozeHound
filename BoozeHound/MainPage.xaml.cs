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

            //DataAccess.AddTestBeers();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new BeersPage();
        }

        /********************************************************************/

        private async void Full_Add_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Add", "Cancel", null, "Beer", "Wine", "Spirit");

            if (action == "Beer")
            {
                App.Current.MainPage = new FullAdd_Beer();
            }
            else if (action == "Wine")
            {

            }
            else if (action == "Spirit")
            {

            }
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
            string action = await DisplayActionSheet("Quick Add", "Cancel", null, "Beer", "Wine", "Spirit");

            if (action == "Beer")
            {
                pu_Quick_Add_Beer.IsVisible = true;
            } else if (action == "Wine")
            {
                pu_Quick_Add_Wine.IsVisible = true;
            }
            else if (action == "Spirit")
            {
                pu_Quick_Add_Spirit.IsVisible = true;
            }
        }

        private void Quick_Add_List_PU_Cancel(object sender, EventArgs e)
        {
            pu_Quick_Add_List.IsVisible = true;
        }

        /********************************************************************/

        private void Quick_Add_Beer(object sender, EventArgs e)
        {
            pu_Quick_Add_Beer.IsVisible = true;
        }

        private void Quick_Add_Beer_PU_Cancel(object sender, EventArgs e)
        {
            pu_Quick_Add_Beer.IsVisible = false;
        }

        /********************************************************************/

        private void Quick_Add_Wine(object sender, EventArgs e)
        {
            pu_Quick_Add_Wine.IsVisible = true;
        }

        private void Quick_Add_Wine_PU_Cancel(object sender, EventArgs e)
        {
            pu_Quick_Add_Wine.IsVisible = false;
        }

        /********************************************************************/

        private void Quick_Add_Spirit(object sender, EventArgs e)
        {
            pu_Quick_Add_Spirit.IsVisible = true;
        }

        private void Quick_Add_Spirit_PU_Cancel(object sender, EventArgs e)
        {
            pu_Quick_Add_Spirit.IsVisible = false;
        }



        /********************************************************************/
        private async void Booze_List_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Your Booze", "Cancel", null, "Beer", "Wine", "Spirit");

            if (action == "Beer")
            {
                App.Current.MainPage = new BeersPage();
            }
            else if (action == "Wine")
            {
                App.Current.MainPage = new WinesPage();
            }
            else if (action == "Spirit")
            {
                App.Current.MainPage = new SpiritsPage();
            }
        }

        /********************************************************************/



        private void Your_Booze_List_PU_Cancel(object sender, EventArgs e)
        {
            pu_Your_Booze_List.IsVisible = true;
        }
    }
}
