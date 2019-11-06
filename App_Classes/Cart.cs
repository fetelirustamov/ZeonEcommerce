using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.App_Classes
{
    public class Cart
    {
        public static Cart ActiveCart
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx.Session["ActiveCart"] == null)
                    ctx.Session["ActiveCart"] = new Cart();
                return (Cart)ctx.Session["ActiveCart"];
            }
        }

        private List<CartItem> products = new List<CartItem>();
        public List<CartItem> Products
        {
            get { return products; }
            set { products = value; }
        }

        public void AddCart(CartItem ci)
        {

            if (HttpContext.Current.Session["ActiveCart"] != null)
            {
                Cart c = (Cart)HttpContext.Current.Session["ActiveCart"];

                if (c.Products.Any(x => x.Product.ProductsId == ci.Product.ProductsId))
                    c.Products.FirstOrDefault(x => x.Product.ProductsId == ci.Product.ProductsId).Quantity++;
                else
                {
                    c.Products.Add(ci);
                }
            }
            else
            {
                Cart c = new Cart();
                c.Products.Add(ci);
                HttpContext.Current.Session["ActiveCart"]=c;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return Products.Sum(x=>x.UnitPrice);
            }

        }
    }

    public class CartItem
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
       
        public decimal UnitPrice
        {
            get
            {
                return (decimal)Product.Price * (1 - Discount / 100) * Quantity;
                
            }
        }
       
    }
   
}