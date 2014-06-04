using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace PacificNWForeclosures
{
    public partial class ZillowDisplay : PhoneApplicationPage
    {
        public ZillowDisplay()
        {
            InitializeComponent();
        }

        private void mZillowDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            // to be implemented by next week
            HouseInfo home = (App.Current as App).house;
            string navigationString;
            navigationString = "http://www.zillow.com/homes/" + " " + home.Address + " " + home.City + " " + home.State + " " + home.Zip + "_rb/";
            // replaces the spaces with - characters
    //        navigationString.Replace(' ', '-');


            mZillowDisplay.Loaded += new RoutedEventHandler(mZillowDisplay_Loaded);
           // mZillowDisplay.Navigate(new Uri("http://www.zillow.com/homes/12561-roosevelt-way-ne-seattle_rb/", UriKind.Absolute));
            mZillowDisplay.Navigate(new Uri(navigationString, UriKind.Absolute));
        }
    }
}