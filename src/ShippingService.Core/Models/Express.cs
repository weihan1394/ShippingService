using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShippingService.Core.Models
{
    public class express
    {
        [Key]
        public int id { get; set; }

        public string type { get; set; }

        public string trackable { get; set; }

        public string service_level { get; set; }

        public string country { get; set; }

        public string country_code { get; set; }

        public int rate_flag { get; set; }

        public double weight { get; set; }

        public double dhl_express { get; set; }

        public double sf_economy { get; set; }

        public double ninja_van { get; set; }

        public int zone { get; set; }
    }
}
