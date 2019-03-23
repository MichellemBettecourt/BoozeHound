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

        private void Quick_Add_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            pu_Quick_Add_List.IsVisible = true;
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
        private void Booze_List_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new BeersPage());
            //pu_Quick_Add_List.IsVisable = true;
            pu_Your_Booze_List.IsVisible = true;
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
