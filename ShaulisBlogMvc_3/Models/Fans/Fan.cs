using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlogMvc_3.Models.Fans
{
    public class Fan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int YearsInClub { get; set; }
        public virtual Location FanLocation { get; set; }

    }
}