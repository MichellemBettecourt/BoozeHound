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
	public partial class StarRating : ContentView
	{
        private List<Image> stars;
        private double rating;

        public double Rating { get { return rating; } }

        TapGestureRecognizer oneHalfTap = new TapGestureRecognizer();
        TapGestureRecognizer oneTap = new TapGestureRecognizer();
        TapGestureRecognizer twoHalfTap = new TapGestureRecognizer();
        TapGestureRecognizer twoTap = new TapGestureRecognizer();
        TapGestureRecognizer threeHalfTap = new TapGestureRecognizer();
        TapGestureRecognizer threeTap = new TapGestureRecognizer();
        TapGestureRecognizer fourHalfTap = new TapGestureRecognizer();
        TapGestureRecognizer fourTap = new TapGestureRecognizer();
        TapGestureRecognizer fiveHalfTap = new TapGestureRecognizer();
        TapGestureRecognizer fiveTap = new TapGestureRecognizer();

        public StarRating ()
		{
			InitializeComponent ();
            stars = new List<Image>() { OneHalf, One, TwoHalf, Two, ThreeHalf, Three, FourHalf, Four, FiveHalf, Five };
            InitTapGestures();
        }

        ~StarRating()
        {
            RemoveTapGestures();
        }

        private void InitTapGestures()
        {
            oneHalfTap.Tapped += OneHalf_Clicked;
            oneTap.Tapped += One_Clicked;
            twoHalfTap.Tapped += TwoHalf_Clicked;
            twoTap.Tapped += Two_Clicked;
            threeHalfTap.Tapped += ThreeHalf_Clicked;
            threeTap.Tapped += Three_Clicked;
            fourHalfTap.Tapped += FourHalf_Clicked;
            fourTap.Tapped += Four_Clicked;
            fiveHalfTap.Tapped += FiveHalf_Clicked;
            fiveTap.Tapped += Five_Clicked;

            OneHalf.GestureRecognizers.Add(oneHalfTap);
            One.GestureRecognizers.Add(oneTap);
            TwoHalf.GestureRecognizers.Add(twoHalfTap);
            Two.GestureRecognizers.Add(twoTap);
            ThreeHalf.GestureRecognizers.Add(threeHalfTap);
            Three.GestureRecognizers.Add(threeTap);
            FourHalf.GestureRecognizers.Add(fourHalfTap);
            Four.GestureRecognizers.Add(fourTap);
            FiveHalf.GestureRecognizers.Add(fiveHalfTap);
            Five.GestureRecognizers.Add(fiveTap);
        }

        private void RemoveTapGestures()
        {
            OneHalf.GestureRecognizers.Clear();
            One.GestureRecognizers.Clear();
            TwoHalf.GestureRecognizers.Clear();
            Two.GestureRecognizers.Clear();
            ThreeHalf.GestureRecognizers.Clear();
            Three.GestureRecognizers.Clear();
            FourHalf.GestureRecognizers.Clear();
            Four.GestureRecognizers.Clear();
            FiveHalf.GestureRecognizers.Clear();
            Five.GestureRecognizers.Clear();

            oneHalfTap.Tapped -= OneHalf_Clicked;
            oneTap.Tapped -= One_Clicked;
            twoHalfTap.Tapped -= TwoHalf_Clicked;
            twoTap.Tapped -= Two_Clicked;
            threeHalfTap.Tapped -= ThreeHalf_Clicked;
            threeTap.Tapped -= Three_Clicked;
            fourHalfTap.Tapped -= FourHalf_Clicked;
            fourTap.Tapped -= Four_Clicked;
            fiveHalfTap.Tapped -= FiveHalf_Clicked;
            fiveTap.Tapped -= Five_Clicked;
        }

        private void UpdateUI(int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (i % 2 == 0)
                {
                    stars[i].Source = "star_left_filled_sm.png";
                }
                else
                {
                    stars[i].Source = "star_right_filled_sm.png";
                }
            }
            for (int i = index; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    stars[i].Source = "star_left_empty_sm.png";
                }
                else
                {
                    stars[i].Source = "star_right_empty_sm.png";
                }
            }
            rating = index / 2.0;
        }

        public void SetRating(double r)
        {
            UpdateUI((int)(r * 2));
        }

        private void OneHalf_Clicked(object sender, EventArgs e)
        {
            UpdateUI(1);
        }

        private void One_Clicked(object sender, EventArgs e)
        {
            UpdateUI(2);
        }

        private void TwoHalf_Clicked(object sender, EventArgs e)
        {
            UpdateUI(3);
        }

        private void Two_Clicked(object sender, EventArgs e)
        {
            UpdateUI(4);
        }

        private void ThreeHalf_Clicked(object sender, EventArgs e)
        {
            UpdateUI(5);
        }

        private void Three_Clicked(object sender, EventArgs e)
        {
            UpdateUI(6);
        }

        private void FourHalf_Clicked(object sender, EventArgs e)
        {
            UpdateUI(7);
        }

        private void Four_Clicked(object sender, EventArgs e)
        {
            UpdateUI(8);
        }

        private void FiveHalf_Clicked(object sender, EventArgs e)
        {
            UpdateUI(9);
        }

        private void Five_Clicked(object sender, EventArgs e)
        {
            UpdateUI(10);
        }
    }
}