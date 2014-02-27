using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TravelDiary1
{
    public sealed partial class PlaceOverlay : UserControl
    {
        public PlaceOverlay()
        {
            this.InitializeComponent();
        }

        public void setTitle(string t)
        {
            TitleTextBlock.Text = t;
        }

        public void setDescription(string d)
        {
            DescriptionTextBlock.Text = d;
        }

        private void Overlay_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
