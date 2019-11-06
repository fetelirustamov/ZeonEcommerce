using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.Models
{
    public class ECommerceContext:DbContext
    {
        public ECommerceContext():base("ECommerceConnection")
        {

        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<ProductsComments> ProductsComments { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<BlogComments> BlogComments { get; set; }
        public DbSet<Rols> Rols { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Advertising> Advertising { get; set; }
        public DbSet<OurTeam> OurTeam { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<ParameterType> ParameterType { get; set; }
        public DbSet<ParameterValue> ParameterValue { get; set; }

        public DbSet<ProductParameters> ProductParameters { get; set; }

    }
}