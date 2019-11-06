using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.App_Classes;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.ViewModel
{
    public class BasketSession
    {
        public IEnumerable<Basket> BasketList { get; set; }
        public Cart Cart { get; set; }
    }
}