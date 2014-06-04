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

namespace PacificNWForeclosures
{
    public partial class PropertyDetail : PhoneApplicationPage
    {
        public PropertyDetail()
        {
         
            InitializeComponent();
            try
            {
                HouseInfo tempHouse = new HouseInfo();
                tempHouse = (App.Current as App).house;
                addressData.Text = tempHouse.Address;
                cityData.Text = tempHouse.CityStateZip;
                saleDate.Text = tempHouse.SaleDate;
                saleLocation.Text = tempHouse.SaleLocation;
                totalDebt.Text = tempHouse.TotalDebt;
                beds.Text = tempHouse.Bedrooms;
                baths.Text = tempHouse.Bedrooms;
                sqft.Text = tempHouse.SquareFeet;
                lotSize.Text = tempHouse.LotSize;
                assessed.Text = tempHouse.AssessedValue;
            }
            catch (Exception e) { }

        }

        private void addressLink_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/ZillowDisplay.xaml", UriKind.Relative));
        }
    }
}