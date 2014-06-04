using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls.Maps;
using System.Text.RegularExpressions; //case-insensitive

namespace PacificNWForeclosures
{
   public class HouseInfo
    {
       // need Zillow url address
       // which we can manufacture at the time of object creation

       // need gps coordinates somehow

       // icon info??
       Regex rxInsensitive = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",
                                      RegexOptions.IgnoreCase);

        public string Address { get; set; }
        public string houseNum { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string SaleDate { get; set; }
        public string SaleLocation { get; set; }
        public string TotalDebt { get; set; }
        public string SquareFeet { get; set; }
        public string LotSize { get; set; }
        public string Bedrooms { get; set; }
        public string Baths { get; set; }
        public string Year { get; set; }
        public string AssessedValue { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Something { get; set; }
        public string CityState { get; set; }
        public string CityStateZip { get; set; }

       // empty constructor
        public HouseInfo()
        {         
        }
       // string constructor
        public HouseInfo(string line)
        {
            int counter = 1;
                string[] words = line.Split('|');
                foreach (string word in words)
                {
                   // Console.WriteLine(word);

                    switch (counter)
                    {
                        case 1:
                            // lets set the houseNUm here also so we can later
                            // use it for searching
                            Address = word;
                            string[] f = word.Split(' ');
                            houseNum = f[0];
                            break;
                        case 2:
                             City = word;
                            break;
                        case 3:
                            State = word;
                            break;
                        case 4:
                            Zip = word;
                            break;
                        case 5:
                            SaleDate = word;
                            break;
                        case 6:
                            SaleLocation = word;
                            break;
                        case 7:
                            TotalDebt = word;
                            break;
                        case 8:
                            SquareFeet = word;
                            break;
                        case 9:
                            LotSize = word;
                            break;
                        case 10:
                            Bedrooms = word;
                            break;
                        case 11:
                            Baths = word;
                            break;
                        case 12:
                            Year = word;
                            break;
                        case 13:
                            //string[] s = word.Split('\r');
                            AssessedValue = word;
                            break;
                        case 14:
                            Lat = Convert.ToDouble(word);
                            break;
                        case 15:
                            Long = Convert.ToDouble(word);
                            break;
                        case 16:
                            Something = word;
                            break;
                    }
                    counter++;
                }
                CityState = City + " " + State;
                CityStateZip = City + " " + State + " " + Zip;
        }

    }
//*****************************************************************************
// our list of House objects
    public class HouseList
    {
        // Our actual list of houses!!!
        
        public List<HouseInfo> ListOfHouses;

        // empty constructor
        public HouseList()
        { 
        }
        public HouseList(string text)
        {
           ListOfHouses = new List<HouseInfo>();
          //  foreach (string line in stream)
          // while (stream.) 
           string stuff = text.ToString();
           string[] lines = stuff.Split('\n');
            foreach(string line in lines )
            {        
              //  new HouseInfo(line);
              //  ListOfHouses.Add(new HouseInfo(line));
                HouseInfo h = new HouseInfo(line);
                ListOfHouses.Add(h);
            }
            
        }
        // Retrieve List of Houses
        public List<HouseInfo> HouseListRetrieve()
        {
            return ListOfHouses;
        }
    }



}

 
