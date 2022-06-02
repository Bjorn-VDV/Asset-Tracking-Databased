using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking_Databased
{
    internal class Office
    {
        public int ID { get; set; }
        public string? OfficePlace { get; set; }
        public string? Currency { get; set; }
        public List<Products>? Products { get; set; }
    }
}
