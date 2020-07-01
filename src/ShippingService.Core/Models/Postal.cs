using System.ComponentModel.DataAnnotations;

namespace ShippingService.Core.Models
{
    public class postal
    {
        [Key]
        public int id { get; set; }

        public string type { get; set; }

        public string trackable { get; set; }

        public string service_level_days { get; set; }

        public string country { get; set; }

        public string country_code { get; set; }

        public double item_weight_kg { get; set; }

        public double singpost_item_rate { get; set; }

        public double singpost_rate_per_kg { get; set; }
    }
}
