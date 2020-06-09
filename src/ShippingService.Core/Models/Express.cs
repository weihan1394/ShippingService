using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Core.Models
{
    public class express
    {
        public string Type { get; set; }

        public string Trackable { get; set; }

        public string ServiceLevel { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public int RateFlag { get; set; }

        public double Weight { get; set; }

        public double DHLExpress { get; set; }

        public double SFEconomy { get; set; }

        public int Zone { get; set; }
    }
}
