using ZeonEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.ViewModel
{
    public class AdminProduct
    {
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Brands> Brands { get; set; }
        public Products Products { get; set; }
    }
}