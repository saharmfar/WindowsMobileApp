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
using System.IO;
using System.Text;
using System.Data;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Resources;
using System.Device.Location;

namespace PacificNWForeclosures
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            string text;
            Uri uri = new Uri("FinalProjectDatabase.csv", UriKind.Relative);
            StreamResourceInfo sri = App.GetResourceStream(uri);
            StreamReader sr = new StreamReader(sri.Stream);
            text = sr.ReadToEnd();
            sr.Close();
           // List<HouseInfo> list = new List<HouseInfo>(text);
            HouseList list = new HouseList(text);
            List<HouseInfo> l = list.HouseListRetrieve();
            // set the list to the global variable
            (App.Current as App).houseList = l;
            InitializeComponent();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ViewMap.xaml", UriKind.Relative));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Search.xaml", UriKind.Relative));
        }
    }
}