using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class FansByCountry
    {
        public FansByCountry(string country, int count)
        {
            this.country = country;
            this.count = count;
        }

        public string country { get; set; }
        public int count { get; set; }
    }
}