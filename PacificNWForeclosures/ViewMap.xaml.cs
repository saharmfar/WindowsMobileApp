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
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;

namespace PacificNWForeclosures
{
    public partial class ViewMap : PhoneApplicationPage
    {
        GeoCoordinateWatcher mGeoWatcher = new GeoCoordinateWatcher();

        private MapLayer posLayer;
        private Pushpin gpsPushpin;
        private Pushpin gpsLoc;

        Image pinImage = new Image();
        public double latitude;
        public double longitude;
        List<HouseInfo> localList;

//*****************************************************************************************
        public ViewMap()
        {
            InitializeComponent();

            mMap.CredentialsProvider = new ApplicationIdCredentialsProvider("AnGsTBs1fixL1LjX5vp-0rKpRnbND9nm-tl_AjQBP7AXILch1wX3clicMCL77S7w");

            //Initialize posLayer
            posLayer = new MapLayer();
            posLayer.Name = "Layer1";
            mMap.Children.Add(posLayer);
            posLayer.Visibility = Visibility.Visible;

            //Initialize Pushpin for FindThis button
            gpsLoc = new Pushpin();
            gpsLoc.Name = "Pushpin2";
            posLayer.Children.Add(gpsLoc);
            gpsLoc.Visibility = Visibility.Collapsed;
            
            mGeoWatcher.Start();
            mGeoWatcher.PositionChanged += PositionChange;
            mGeoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
            mGeoWatcher.PositionChanged += gps;

            // lets set up a for loop here and step through our list
            // local house list which will be a copy of our appdata list
            // this list is a copy of the FULL HOUSE LIST!!!! not just search results
            localList = new List<HouseInfo>();
            localList = (App.Current as App).houseList;
         
            // not really sure yet if we should add the pushpin object the the HouseInfo class objects.
            // Lets think about it for a while. 
            // We could just create the pushpins during the list creation while we are constructing everything
            foreach (HouseInfo h in localList)
            {
                if (h.Lat != 0 && h.Long != 0)
                {
                    //Initialize Pushpin
                    gpsPushpin = new Pushpin();
                    latitude = h.Lat;
                    longitude = h.Long;
                    gpsPushpin.DataContext = h;
                    gpsPushpin.Location = new GeoCoordinate(latitude, longitude);
                    gpsPushpin.Name = "Pushpin1";
                    gpsPushpin.Template = (ControlTemplate)(this.Resources["PushpinControlTemplate1"]);
                    gpsPushpin.Content = h;
                    gpsPushpin.MouseLeftButtonUp += new MouseButtonEventHandler(homeIconClick);
                    posLayer.Children.Add(gpsPushpin);
                    gpsPushpin.Visibility = Visibility.Visible;
                    mMap.Center = gpsPushpin.Location;
                    mMap.ZoomLevel = 12;
                }
            }
        }

        CivicAddressResolver resolver = new CivicAddressResolver();
//************************************* GPS Service function ***********************************
        void PositionChange(object sender, GeoPositionChangedEventArgs<GeoCoordinate> args)
        {
            Dispatcher.BeginInvoke(() =>
            {
                (App.Current as App).mCurrentCoordinate = args.Position.Location;
                gpsPushpin.Location = args.Position.Location;
                gpsLoc.Location = args.Position.Location;
                CivicAddress address = resolver.ResolveAddress(new GeoCoordinate(latitude, longitude));
                (App.Current as App).mLocation = address.ToString();
                string s = (App.Current as App).mLocation;
            });
        }

//******************************** GPS Position GEO Coordinate*****************************************
        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            gpsPushpin.Location = e.Position.Location;
            gpsLoc.Location = e.Position.Location;
            gpsPushpin.Visibility = Visibility.Visible;
            latitude = e.Position.Location.Latitude;
            longitude = e.Position.Location.Longitude;
        }
//******************************** GPS Position GEO Helper *****************************************
        void gps(object sender, GeoPositionChangedEventArgs<GeoCoordinate> args)
        {
            mMap.Center = gpsLoc.Location;
            mGeoWatcher.Stop();
        }
//*****************************************************************************************
        void homeIconClick(object sender, MouseButtonEventArgs e)
        {
            // set the global varible for the info on the house here 
            // before we navigate so that the PropertyDetail page will display correctly
            // lets grab the house information for the house that the user selected
            // set that data to a global variable
            Pushpin house = new Pushpin();
            house = (Pushpin)sender;
            (App.Current as App).house = (HouseInfo)house.Content;

            NavigationService.Navigate(new Uri("/PropertyDetail.xaml", UriKind.Relative));
        }

//*****************************************************************************************
        private void click_ZoomIn(object sender, RoutedEventArgs e)
        {
            ++mMap.ZoomLevel;
            if (mMap.ZoomLevel >= 10)
            {
                mWarning.Visibility = Visibility.Collapsed;
                gpsPushpin.Visibility = Visibility.Visible;
            }
        }

//*****************************************************************************************
        private void click_ZoomOut(object sender, RoutedEventArgs e)
        {
            if (mMap.ZoomLevel <= 10)
            {
                gpsPushpin.Template = (ControlTemplate)(this.Resources["PushpinControlTemplate1"]);
                gpsPushpin.Visibility = Visibility.Collapsed;
                mWarning.Visibility = Visibility.Visible;
                mWarning.Text = "Zoom in to view homes";
                --mMap.ZoomLevel;
            }
            else
            {
                --mMap.ZoomLevel;
            }
        }

//*****************************************************************************************
        private void PushpinButton_Click(object sender, RoutedEventArgs e)
        {
            HouseInfo house = new HouseInfo();
            house = (HouseInfo)gpsPushpin.Content;
            (App.Current as App).house = house;

            NavigationService.Navigate(new Uri("/PropertyDetail.xaml", UriKind.Relative));
        }

    }
}