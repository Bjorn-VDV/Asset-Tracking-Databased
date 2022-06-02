using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking_Databased
{
    internal class ProductType
    {
        public int ID { get; set; }
        public string? TypeName { get; set; }
        public List<Products>? Products { get; set; }
    }
}
