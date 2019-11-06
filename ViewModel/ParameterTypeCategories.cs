using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.ViewModel
{
    public class ParameterTypeCategories
    {
        public ICollection<Categories> Categories { get; set; }
        public ParameterType ParameterType { get; set; }
    }
}