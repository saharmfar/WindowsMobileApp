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
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
        }

//***************************** Keyboard pop up as number ***************************************
        protected void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //This code opens up the keyboard when you navigate to the page.
            addressSearch.UpdateLayout();
            addressSearch.Focus();
            zipSearch.UpdateLayout();
            zipSearch.Focus();
        }

//***************************** First letter as Uppercase ****************************
        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

//***************************** Addrress Go ***************************************
        private void mAddressGo_Click(object sender, RoutedEventArgs e)
        {
            // keyboard pop up as number
            InputScope scope = new InputScope();
            InputScopeName name = new InputScopeName();
            name.NameValue = InputScopeNameValue.Digits;
            scope.Names.Add(name);
            addressSearch.InputScope = scope;

            // set the house, city and zip info
            if (addressSearch.Text == "")
                (App.Current as App).houseNum = null;
            else
                (App.Current as App).houseNum = addressSearch.Text;
            (App.Current as App).city = null;
            (App.Current as App).zip = null;
            NavigationService.Navigate(new Uri("/SearchResults.xaml", UriKind.Relative));
        }


//***************************** City Go ***************************************
        private void mCityGo_Click(object sender, RoutedEventArgs e)
        {
            string word = citySearch.Text;
            
            // set the house, city and zip info
            (App.Current as App).houseNum = null;
            if (citySearch.Text == "")
                (App.Current as App).city = null;
            else
                word = UppercaseFirst(word);
                (App.Current as App).city = word;
            
            //rxInsensitive.Matches(word);
            (App.Current as App).zip = null;

            NavigationService.Navigate(new Uri("/SearchResults.xaml", UriKind.Relative));
        }

//***************************** Zip Go ***************************************
        private void mZipGo_Click(object sender, RoutedEventArgs e)
        {
            // keyboard pop up as number
            InputScope scope = new InputScope();
            InputScopeName name = new InputScopeName();
            name.NameValue = InputScopeNameValue.Digits;
            scope.Names.Add(name);
            zipSearch.InputScope = scope;

            // set the house, city and zip info
            (App.Current as App).houseNum = null;
            (App.Current as App).city = null;
            if (zipSearch.Text == "")
                (App.Current as App).zip = null;
            else
              (App.Current as App).zip = zipSearch.Text;
 
            NavigationService.Navigate(new Uri("/SearchResults.xaml", UriKind.Relative));
        }
    }
}