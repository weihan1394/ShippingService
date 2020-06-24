using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingService.Core.Dtos
{
    public class ExpressDto
    {
        public int id { get; set; }

        public string type { get; set; }

        public string trackable { get; set; }

        public string serviceLevel { get; set; }

        public string country { get; set; }

        public string countryCode { get; set; }

        public int rateFlag { get; set; }

        public double weight { get; set; }

        public double price { get; set; }

        public string vendor { get; set; }

        public int zone { get; set; }
    }
}
