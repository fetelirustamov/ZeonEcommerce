using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.Models
{
    public class ProductParameters
    {
        public int ProductParametersId { get; set; }

        public int ProductsID { get; set; }
        public virtual Products Products { get; set; }

        public int ParameterTypeID { get; set; }
        public virtual ParameterType ParametrType { get; set; }

        public int ParameterValueID { get; set; }
        public virtual ParameterValue ParametrValue { get; set; }
    }
}