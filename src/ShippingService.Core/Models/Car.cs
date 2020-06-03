using System.ComponentModel.DataAnnotations;

namespace ShippingService.Core.Models
{
    public class car
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string plate { get; set; }

        [StringLength(50)]
        public string model { get; set; }
    }
}
