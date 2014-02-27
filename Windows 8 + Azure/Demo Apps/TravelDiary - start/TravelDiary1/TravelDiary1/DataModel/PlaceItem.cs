using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bing.Maps;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace TravelDiary1.DataModel
{
    class PlaceItem
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public PlaceItem()
        {
            Name = Guid.NewGuid().ToString();
        }
    }
}
