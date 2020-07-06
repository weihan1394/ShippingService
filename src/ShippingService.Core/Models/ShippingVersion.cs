using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShippingService.Core.Models
{
    public class shipping_version
    {
        [Key]
        public int id { get; set; }

        public DateTime upload_date { get; set; }

        public string shipping_mode { get; set; }

        public DateTime valid_start { get; set; }

        public DateTime valid_end { get; set; }

        public string uploaded_by { get; set; }
    }
}
