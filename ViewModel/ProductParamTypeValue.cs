using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.ViewModel
{
    public class ProductParamTypeValue
    {
        public int? ProductsID { get; set; }
        public ICollection<Products> Products { get; set; }
        public ICollection<ParameterType> ParameterTypes { get; set; }
        public ICollection<ParameterValue> ParameterValues { get; set; }
        public ProductParameters ProductParameters { get; set; }

    }
}