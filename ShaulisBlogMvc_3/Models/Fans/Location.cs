using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlogMvc_3.Models.Fans
{
    public class Location
    {
        public int LocationId { get; set; }
        public string PlaceId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public int StreetNumber { get; set; }
        public int AptNumber { get; set; }
    }
}