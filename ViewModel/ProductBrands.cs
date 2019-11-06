using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.ViewModel
{
    public class ProductBrands
    {
        public ICollection<Products> Products { get; set; }
        public ICollection<Brands> Brands { get; set; }
        public ICollection<Categories> Categories { get; set; }
        public int PageCount { get; set; }
    }
}