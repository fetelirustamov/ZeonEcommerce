using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.Models
{
    public class ParameterValue
    {
        public int ParameterValueId { get; set; }
        public string Name { get; set; }

        public int ParameterTypeID { get; set; }
        public virtual ParameterType ParameterType { get; set; }
        public virtual ICollection<ProductParameters> ProductParametrs { get; set; }
    }
}