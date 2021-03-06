using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingService.Core.Models
{
    [Table("bulk", Schema ="public")]
    public class Bulk
    {
        [Key]
        public int id { get; set; }

        public string type { get; set; }

        public string trackable { get; set; }

        public string service_level { get; set; }

        public string country { get; set; }

        public string country_code { get; set; }

        public double item_weight_kg { get; set; }

        public double total_weight_kg { get; set; }

        public double ascendia_item_rate { get; set; }

        public double ascendia_rate_per_kg { get; set; }

        public double singpost_item_rate { get; set; }

        public double singpost_rate_per_kg { get; set; }

        public double dai_item_rate { get; set; }

        public double dai_rate_per_kg { get; set; }

        //public shipping_version shipping_version { get; set; }
    }
}
