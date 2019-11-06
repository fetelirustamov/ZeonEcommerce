namespace ZeonEcommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lnitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisings",
                c => new
                    {
                        AdvertisingId = c.Int(nullable: false, identity: true),
                        RowNo = c.Int(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.AdvertisingId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        CustomersID = c.Int(nullable: false),
                        ProductsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BasketId)
                .ForeignKey("dbo.Customers", t => t.CustomersID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsID, cascadeDelete: true)
                .Index(t => t.CustomersID)
                .Index(t => t.ProductsID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomersId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        City = c.String(),
                        Street = c.String(),
                        Phone = c.String(),
                        Token = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CustomersId);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        BlogsCommentsId = c.Int(nullable: false, identity: true),
                        BlogComment = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        BlogsID = c.Int(nullable: false),
                        CustomersID = c.Int(),
                        SuppliersID = c.Int(),
                    })
                .PrimaryKey(t => t.BlogsCommentsId)
                .ForeignKey("dbo.Blogs", t => t.BlogsID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SuppliersID)
                .ForeignKey("dbo.Customers", t => t.CustomersID)
                .Index(t => t.BlogsID)
                .Index(t => t.CustomersID)
                .Index(t => t.SuppliersID);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogsId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        BlogContent = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Picture = c.String(),
                        SuppliersID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogsId)
                .ForeignKey("dbo.Suppliers", t => t.SuppliersID, cascadeDelete: true)
                .Index(t => t.SuppliersID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SuppliersId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SurName = c.String(nullable: false, maxLength: 50),
                        Company = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        Street = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        WebSite = c.String(),
                        Token = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RolsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SuppliersId)
                .ForeignKey("dbo.Rols", t => t.RolsID, cascadeDelete: true)
                .Index(t => t.RolsID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductsId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discont = c.Int(nullable: false),
                        Picture = c.String(),
                        Stock = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        SuppliersID = c.Int(nullable: false),
                        BrandsID = c.Int(nullable: false),
                        CategoriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductsId)
                .ForeignKey("dbo.Brands", t => t.BrandsID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoriesID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SuppliersID, cascadeDelete: true)
                .Index(t => t.SuppliersID)
                .Index(t => t.BrandsID)
                .Index(t => t.CategoriesID);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandsId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.BrandsId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoriesId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        SubCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoriesId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogsId = c.Int(nullable: false, identity: true),
                        ActionName = c.String(),
                        ControllerName = c.String(),
                        Time = c.DateTime(nullable: false),
                        Info = c.String(),
                        UserIp = c.String(),
                        CustomersID = c.Int(),
                        CategoriesID = c.Int(),
                    })
                .PrimaryKey(t => t.LogsId)
                .ForeignKey("dbo.Categories", t => t.CategoriesID)
                .ForeignKey("dbo.Customers", t => t.CustomersID)
                .Index(t => t.CustomersID)
                .Index(t => t.CategoriesID);
            
            CreateTable(
                "dbo.ParameterTypes",
                c => new
                    {
                        ParameterTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterTypeId)
                .ForeignKey("dbo.Categories", t => t.CategoriesID, cascadeDelete: true)
                .Index(t => t.CategoriesID);
            
            CreateTable(
                "dbo.ParameterValues",
                c => new
                    {
                        ParameterValueId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParameterTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterValueId)
                .ForeignKey("dbo.ParameterTypes", t => t.ParameterTypeID, cascadeDelete: true)
                .Index(t => t.ParameterTypeID);
            
            CreateTable(
                "dbo.ProductParameters",
                c => new
                    {
                        ProductParametersId = c.Int(nullable: false, identity: true),
                        ProductsID = c.Int(nullable: false),
                        ParameterTypeID = c.Int(nullable: false),
                        ParameterValueID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductParametersId)
                .ForeignKey("dbo.ParameterTypes", t => t.ParameterTypeID, cascadeDelete: true)
                .ForeignKey("dbo.ParameterValues", t => t.ParameterValueID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductsID, cascadeDelete: false)
                .Index(t => t.ProductsID)
                .Index(t => t.ParameterTypeID)
                .Index(t => t.ParameterValueID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImagesId = c.Int(nullable: false, identity: true),
                        HomeSlider = c.String(),
                        ProductSlider = c.String(),
                        RowNo = c.Int(nullable: false),
                        ProductsID = c.Int(),
                    })
                .PrimaryKey(t => t.ImagesId)
                .ForeignKey("dbo.Products", t => t.ProductsID)
                .Index(t => t.ProductsID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        ProductsID = c.Int(nullable: false),
                        OrdersID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.Orders", t => t.OrdersID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsID, cascadeDelete: true)
                .Index(t => t.ProductsID)
                .Index(t => t.OrdersID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrdersId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderAdress = c.String(),
                        OrderPhone = c.String(),
                        OrderName = c.String(),
                        TotalPrice = c.String(),
                    })
                .PrimaryKey(t => t.OrdersId);
            
            CreateTable(
                "dbo.ProductsComments",
                c => new
                    {
                        ProductCommentsId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CustomersID = c.Int(),
                        SuppliersID = c.Int(),
                        ProductsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCommentsId)
                .ForeignKey("dbo.Customers", t => t.CustomersID)
                .ForeignKey("dbo.Products", t => t.ProductsID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SuppliersID)
                .Index(t => t.CustomersID)
                .Index(t => t.SuppliersID)
                .Index(t => t.ProductsID);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        RolsId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RolsId);
            
            CreateTable(
                "dbo.OurTeams",
                c => new
                    {
                        OurTeamId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Job = c.String(nullable: false, maxLength: 100),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Linkedin = c.String(),
                        Google = c.String(),
                        About = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.OurTeamId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 100),
                        PageContent = c.String(),
                        Picture = c.String(),
                        RowNo = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Logo = c.String(),
                        Fax = c.String(),
                        City = c.String(),
                        Adress = c.String(),
                        Facebook = c.String(),
                        Instagram = c.String(),
                        Youtube = c.String(),
                        Google = c.String(),
                        Twitter = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComments", "CustomersID", "dbo.Customers");
            DropForeignKey("dbo.Suppliers", "RolsID", "dbo.Rols");
            DropForeignKey("dbo.Products", "SuppliersID", "dbo.Suppliers");
            DropForeignKey("dbo.ProductsComments", "SuppliersID", "dbo.Suppliers");
            DropForeignKey("dbo.ProductsComments", "ProductsID", "dbo.Products");
            DropForeignKey("dbo.ProductsComments", "CustomersID", "dbo.Customers");
            DropForeignKey("dbo.OrderDetails", "ProductsID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrdersID", "dbo.Orders");
            DropForeignKey("dbo.Images", "ProductsID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoriesID", "dbo.Categories");
            DropForeignKey("dbo.ProductParameters", "ProductsID", "dbo.Products");
            DropForeignKey("dbo.ProductParameters", "ParameterValueID", "dbo.ParameterValues");
            DropForeignKey("dbo.ProductParameters", "ParameterTypeID", "dbo.ParameterTypes");
            DropForeignKey("dbo.ParameterValues", "ParameterTypeID", "dbo.ParameterTypes");
            DropForeignKey("dbo.ParameterTypes", "CategoriesID", "dbo.Categories");
            DropForeignKey("dbo.Logs", "CustomersID", "dbo.Customers");
            DropForeignKey("dbo.Logs", "CategoriesID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandsID", "dbo.Brands");
            DropForeignKey("dbo.Baskets", "ProductsID", "dbo.Products");
            DropForeignKey("dbo.Blogs", "SuppliersID", "dbo.Suppliers");
            DropForeignKey("dbo.BlogComments", "SuppliersID", "dbo.Suppliers");
            DropForeignKey("dbo.BlogComments", "BlogsID", "dbo.Blogs");
            DropForeignKey("dbo.Baskets", "CustomersID", "dbo.Customers");
            DropIndex("dbo.ProductsComments", new[] { "ProductsID" });
            DropIndex("dbo.ProductsComments", new[] { "SuppliersID" });
            DropIndex("dbo.ProductsComments", new[] { "CustomersID" });
            DropIndex("dbo.OrderDetails", new[] { "OrdersID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductsID" });
            DropIndex("dbo.Images", new[] { "ProductsID" });
            DropIndex("dbo.ProductParameters", new[] { "ParameterValueID" });
            DropIndex("dbo.ProductParameters", new[] { "ParameterTypeID" });
            DropIndex("dbo.ProductParameters", new[] { "ProductsID" });
            DropIndex("dbo.ParameterValues", new[] { "ParameterTypeID" });
            DropIndex("dbo.ParameterTypes", new[] { "CategoriesID" });
            DropIndex("dbo.Logs", new[] { "CategoriesID" });
            DropIndex("dbo.Logs", new[] { "CustomersID" });
            DropIndex("dbo.Products", new[] { "CategoriesID" });
            DropIndex("dbo.Products", new[] { "BrandsID" });
            DropIndex("dbo.Products", new[] { "SuppliersID" });
            DropIndex("dbo.Suppliers", new[] { "RolsID" });
            DropIndex("dbo.Blogs", new[] { "SuppliersID" });
            DropIndex("dbo.BlogComments", new[] { "SuppliersID" });
            DropIndex("dbo.BlogComments", new[] { "CustomersID" });
            DropIndex("dbo.BlogComments", new[] { "BlogsID" });
            DropIndex("dbo.Baskets", new[] { "ProductsID" });
            DropIndex("dbo.Baskets", new[] { "CustomersID" });
            DropTable("dbo.Settings");
            DropTable("dbo.Pages");
            DropTable("dbo.OurTeams");
            DropTable("dbo.Rols");
            DropTable("dbo.ProductsComments");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Images");
            DropTable("dbo.ProductParameters");
            DropTable("dbo.ParameterValues");
            DropTable("dbo.ParameterTypes");
            DropTable("dbo.Logs");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogComments");
            DropTable("dbo.Customers");
            DropTable("dbo.Baskets");
            DropTable("dbo.Advertisings");
        }
    }
}
