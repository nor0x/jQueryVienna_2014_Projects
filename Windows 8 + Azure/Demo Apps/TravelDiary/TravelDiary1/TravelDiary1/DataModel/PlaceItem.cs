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

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public PlaceItem()
        {
            Name = Guid.NewGuid().ToString();
        }
    }
}
