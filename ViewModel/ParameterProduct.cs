using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.ViewModel
{
    public class ParameterProduct
    {
        public Products Product { get; set; }
        public ICollection<ProductParameters> ProductParameters { get; set; }
    }
}