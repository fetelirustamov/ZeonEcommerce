using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.ViewModel
{
    public class ParameterValueType
    {
        public ICollection<ParameterType> ParameterType { get; set; }
        public ParameterValue ParameterValue { get; set; }

    }
}