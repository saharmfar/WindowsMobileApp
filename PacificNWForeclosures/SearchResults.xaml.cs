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
using System.Windows.Media.Imaging;
using System.IO;

namespace PacificNWForeclosures
{
    public partial class SearchResults : PhoneApplicationPage
    {
        // the list for the result of the search that will be used on this page
        public List<HouseInfo> localHouseList = new List<HouseInfo>();

        public SearchResults()
        {
            InitializeComponent();
            findOurList();
            SetPage();

        }
        private void findOurList()
        {
            foreach (HouseInfo h in (App.Current as App).houseList)
            {
                // we have 3 conditions we are going to search for
                // 1. if the house number is the one the user wants
                // 2. if the city is the city the user wants
                // 3. if the zip == the zip the user wants
                if (h.houseNum == (App.Current as App).houseNum ||
                    h.City == (App.Current as App).city ||
                    h.Zip == (App.Current as App).zip)
                {
                    // add our house to our templist if it meets any of 
                    // the three above conditions
                    if (h.houseNum != null && h.Zip != null && h.City != null)
                        localHouseList.Add(h);

                }
            }
        }
        // setting the list on the page here
        public void SetPage()
        {
            int counter = 0;
            foreach (HouseInfo h in localHouseList)
                counter++;

            if (counter > 0)
            {
                mFileNameList.ItemsSource = localHouseList;
            }
        }
        // click event to show the results on the map
        private void mMapResults_Click(object sender, RoutedEventArgs e)
        {
            // Implement this for final 
           // NavigationService.Navigate(new Uri("/ViewMap.xaml", UriKind.Relative));
        }

        // click event handler for when a user selects a property.
        // Should take us to the details page for that property
        private void Selection_Click_Event(object sender, SelectionChangedEventArgs e)
        {
            // lets grab the house information for the house that the user selected
            HouseInfo house = new HouseInfo();
            house = localHouseList.ElementAt(mFileNameList.SelectedIndex);
            // set that data to a global variable
            (App.Current as App).house = house;

            NavigationService.Navigate(new Uri("/PropertyDetail.xaml", UriKind.Relative));
        }
    }
}