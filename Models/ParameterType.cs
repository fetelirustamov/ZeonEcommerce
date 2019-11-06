using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.Models
{
    public class ParameterType
    {
        public int ParameterTypeId { get; set; }
        public string Name { get; set; }

        public int CategoriesID { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
        public virtual ICollection<ProductParameters> ProductParameters { get; set; }
    }
}