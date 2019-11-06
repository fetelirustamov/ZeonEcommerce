using ZeonEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.ViewModel
{
    public class AdminCategories
    {
        public IEnumerable<Categories> Categories { get; set; }
        public Categories CategoriesOne { get; set; }
    }
}