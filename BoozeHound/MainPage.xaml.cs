﻿using System;
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
    }
}
