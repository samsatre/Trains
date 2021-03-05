using System;
using System.Collections.Generic;
using System.Text;

namespace Trains
{
    class Route
    {
        public int length;

        public List<char> cities;

        // Creates route object without a known length.
        public Route(List<char> c)
        {
            cities = c;
        }

        // Creates route object with a known length.
        public Route(List<char> c, int l)
        {
            cities = c;
            length = l;
        }

        // Copies object and cities array.
        public Route DeepCopy()
        {
            Route copy = (Route)this.MemberwiseClone();
            List<char> cities_copy = new List<char>(this.cities);
            copy.cities = cities_copy;
            return copy;
        }
    }
}
