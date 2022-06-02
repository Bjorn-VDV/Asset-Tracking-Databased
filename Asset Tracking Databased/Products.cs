using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking_Databased
{
    internal class Products
    {
        [Key]
        public int ID { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Date { get; set; }
        public float UKPrice { get; set; }

        public int OfficeID { get; set; }
        public Office? Office { get; set; }

        public int ProductTypeID { get; set; }
        public ProductType? ProductType { get; set; }
    }
}
