using Bing.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDiary1.DataModel
{
    class Place
    {
       public PlaceItem PlaceData { get; set; }
       public Pushpin Pin { get; set; }

       public Place()
       {
           PlaceData = new PlaceItem();
           Pin = new Pushpin();
       }
    }
}
