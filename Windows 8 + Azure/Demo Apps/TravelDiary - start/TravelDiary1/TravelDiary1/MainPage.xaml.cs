#region usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelDiary1.DataModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endregion
using Bing.Maps;
using System.Threading.Tasks;



namespace TravelDiary1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Position currentPosition;
        private List<Place> placeList;

        //TODO: add database Table for Windows Azure

        public MainPage()
        {
            this.InitializeComponent();
            placeList = new List<Place>();
            getCurrentPosition();
            TravelMap.GotFocus += TravelMap_Tapped;
        }

        #region Save Button Click
        private void SaveEntryButton_Click(object sender, RoutedEventArgs e)
        {
            Place myPlace = new Place();
            myPlace.PlaceData.Longitude = currentPosition.Longitude;
            myPlace.PlaceData.Latitude = currentPosition.Latitude;
            myPlace.PlaceData.Title = TitleTextBox.Text;
            myPlace.PlaceData.Description = DescriptionTextBox.Text;
            myPlace.Pin = new Pushpin();
            myPlace.Pin.Name = myPlace.PlaceData.Name;
            myPlace.Pin.Tapped += Pin_Tapped;
            myPlace.Pin.Background = new SolidColorBrush(Colors.Green);
            myPlace.Pin.DataContext = myPlace.PlaceData;
            placeList.Add(myPlace);
            
            MapLayer.SetPosition(myPlace.Pin, new Location(currentPosition.Latitude, currentPosition.Longitude));
            TravelMap.Children.Add(myPlace.Pin);
            InsertDataAsync(myPlace);
        }

        //TODO: add insert data method

        #endregion



        #region Positioning
        private async void getCurrentPosition()
        {
            if (currentPosition == null)
            {
                currentPosition = new Position();
            }
            Geolocator geo = new Geolocator();
            Position result = new Position();
            Geoposition pos = await geo.GetGeopositionAsync();
            currentPosition.Longitude = pos.Coordinate.Point.Position.Longitude;
            currentPosition.Latitude = pos.Coordinate.Point.Position.Latitude;
            LatitudeTextBlock.Text = currentPosition.Latitude.ToString();
            LongitudeTextBlock.Text = currentPosition.Longitude.ToString();
        }
        #endregion


        #region download data
        private async void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            List<PlaceItem> tempList = await GetListAsync();
            foreach (PlaceItem pi in tempList)
            {
                Place p = new Place();
                p.Pin.Background = new SolidColorBrush(Colors.Green);
                p.Pin.Name = pi.Name;
                p.Pin.Tapped += Pin_Tapped;
                p.PlaceData = pi;
                placeList.Add(p);
                TravelMap.Children.Add(p.Pin);
                MapLayer.SetPosition(p.Pin, new Location(pi.Latitude, pi.Longitude));
            }
        }

        //TODO: add get list method
        #endregion

        #region DemoData
        private void DemoButton_Click(object sender, RoutedEventArgs e)
        {
            fillWithDemoData();
        }
        private async void fillWithDemoData()
        {
            Place seattle = new Place();
            seattle.PlaceData.Longitude = -122.64038085937501;
            seattle.PlaceData.Latitude = 47.87715503763398;
            seattle.PlaceData.Title = "Seattle";
            seattle.PlaceData.Description = "Seattle is a coastal seaport city and the seat of King County, in the U.S. state of Washington. With an estimated 634,535 residents as of 2012, Seattle is the largest city in the Pacific Northwest region";
            seattle.Pin = new Pushpin();
            seattle.Pin.Background = new SolidColorBrush(Colors.Green);
            seattle.Pin.Name = seattle.PlaceData.Name;
            placeList.Add(seattle);
            TravelMap.Children.Add(seattle.Pin);
            MapLayer.SetPosition(seattle.Pin, new Location(seattle.PlaceData.Latitude, seattle.PlaceData.Longitude));

            Place london = new Place();
            london.PlaceData.Longitude = -0.1593017578125082;
            london.PlaceData.Latitude = 51.59850312582267;
            london.PlaceData.Title = "London";
            london.PlaceData.Description = "London is the capital city of England and of the United Kingdom. It is the most populous region, urban zone and metropolitan area in the United Kingdom";
            london.Pin = new Pushpin();
            london.Pin.Background = new SolidColorBrush(Colors.Green);
            london.Pin.Name = london.PlaceData.Name;
            placeList.Add(london);
            TravelMap.Children.Add(london.Pin);
            MapLayer.SetPosition(london.Pin, new Location(london.PlaceData.Latitude, london.PlaceData.Longitude));

            Place singapore = new Place();
            singapore.PlaceData.Longitude = 103.820534;
            singapore.PlaceData.Latitude = 1.321996;
            singapore.PlaceData.Title = "Singapore";
            singapore.PlaceData.Description = "Singapore, officially the Republic of Singapore, is a sovereign city-state and island country in Southeast Asia. It lies off the southern tip of the Malay Peninsula and is 137 kilometres north of the equator.";
            singapore.Pin = new Pushpin();
            singapore.Pin.Background = new SolidColorBrush(Colors.Green);
            singapore.Pin.Name = singapore.PlaceData.Name;
            placeList.Add(singapore);
            TravelMap.Children.Add(singapore.Pin);
            MapLayer.SetPosition(singapore.Pin, new Location(singapore.PlaceData.Latitude, singapore.PlaceData.Longitude));

            Place newyork = new Place();
            newyork.PlaceData.Longitude = -73.832703;
            newyork.PlaceData.Latitude = 40.782001;
            newyork.PlaceData.Title = "New York";
            newyork.PlaceData.Description = "New York is a state in the Northeastern and Mid-Atlantic regions of the United States. New York is the 27th-most extensive, the third-most populous, and the seventh-most densely populated of the 50 United States";
            newyork.Pin = new Pushpin();
            newyork.Pin.Background = new SolidColorBrush(Colors.Green);
            newyork.Pin.Name = newyork.PlaceData.Name;
            placeList.Add(newyork);
            TravelMap.Children.Add(newyork.Pin);
            MapLayer.SetPosition(newyork.Pin, new Location(newyork.PlaceData.Latitude, newyork.PlaceData.Longitude));

            foreach (Place p in placeList)
            {
                p.Pin.Tapped += Pin_Tapped;
                await placeTable.InsertAsync(p.PlaceData);
            }
            DemoButton.IsEnabled = false;
        }
        #endregion

        #region Other Event Handlers
        void Pin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("PIN TAPPED");
            Pushpin pp = (Pushpin)sender;
            foreach (Place p in placeList)
            {
                if (p.PlaceData.Name == pp.Name)
                {
                    MyOverlay.setTitle(p.PlaceData.Title);
                    MyOverlay.setDescription(p.PlaceData.Description);
                    MyOverlay.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        void TravelMap_Tapped(object sender, RoutedEventArgs e)
        {
            if (MyOverlay.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                MyOverlay.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
        #endregion
    }
}
