using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZeonEcommerce.Filter;
using ZeonEcommerce.Models;
using ZeonEcommerce.App_Classes;
using ZeonEcommerce.ViewModel;

namespace ZeonEcommerce.Controllers
{

    public class HomeController : Controller
    {

        ECommerceContext db = new ECommerceContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Əsas Səhifə";
            return View();
        }


        public PartialViewResult Brands()
        {
            return PartialView(db.Brands.ToList());
        }

        public PartialViewResult Adds()
        {
            return PartialView(db.Advertising.ToList());
        }

        public PartialViewResult Header()
        {

            var setting = db.Settings.FirstOrDefault(x => x.Id == 1);
            return PartialView(setting);
        }

        public PartialViewResult Menu()
        {
            return PartialView(db.Categories.ToList());
        }

        public PartialViewResult Footer()
        {
            var setting = db.Settings.FirstOrDefault(x => x.Id == 1);
            return PartialView(setting);
        }

        public PartialViewResult Services()
        {
            return PartialView(db.Products.ToList());
        }

        public PartialViewResult Collection()
        {
            var ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = Request.ServerVariables["REMOTE_ADDR"];
            }
            List<Products> product;
            var logCategories = db.Logs.Where(x => x.UserIp == ipAdd).OrderByDescending(x => x.LogsId).Take(1).FirstOrDefault();
            if (logCategories != null)
            {
                product = db.Products.Where(x => x.CategoriesID == logCategories.CategoriesID).Take(8).ToList();
            }
            else
            {
                product = db.Products.Where(x => x.CategoriesID == 2).Take(8).ToList();
            }


            if (product.Count<8)
            {
                product = product.Take(4).ToList();
            }else if (product.Count>8)
            {
                product = product.Take(8).ToList();
            }
            return PartialView(product);
        }

        public PartialViewResult NewProducts()
        {
            return PartialView(db.Products.ToList());
        }

        public PartialViewResult Slider()
        {
            return PartialView(db.Images.ToList());
        }

        public ActionResult Cart()
        {
            //BasketSession ViewModel = new BasketSession();
            if (Session["CurrentUser"] != null)
            {
                var id = (int)Session["CurrentUserId"];
                ViewBag.BasketList = db.Basket.Where(x => x.CustomersID ==id).ToList();
                return View();
            }
            if (Session["ActiveCart"] != null)
            {

                var Cart = (Cart)Session["ActiveCart"];
                ViewBag.TotalPrice = Cart.TotalPrice;
                return View(Cart.Products);
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult CompleteShopping()
        {
            if (Session["CurrentUser"] != null)
            {
                var id = (int)Session["CurrentUserId"];
                ViewBag.TotalPrice = db.Basket.Where(x => x.CustomersID == id).Sum(x=>x.Products.Price*x.Quantity);
                return View();
            }
            if (Session["ActiveCart"] != null)
            {

                var Cart = (Cart)Session["ActiveCart"];
                ViewBag.TotalPrice = Cart.TotalPrice;
                return View(Cart);
            }
            else
            {
                return HttpNotFound();
            }
        }

         [HttpPost]
        [reCAPTCHA.MVC.CaptchaValidator]
        public ActionResult CompleteShopping(string OrderName,string OrderPhone,string SurName,string OrderAdress, string TotalPrice, bool captchaValid)
        {
            if (ModelState.IsValid)
            {
                Orders orders = new Orders();
                orders.OrderDate = DateTime.Now;
                orders.OrderName = OrderName + " " + SurName;
                orders.OrderPhone = OrderPhone;
                orders.OrderAdress = OrderAdress;
                orders.TotalPrice = TotalPrice;
                db.Orders.Add(orders);
                db.SaveChanges();

                if (Session["CurrentUser"] != null)
                {
                    var id = (int)Session["CurrentUserId"];
                    foreach (var item in db.Basket.Where(x => x.CustomersID == id))
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.ProductsID = item.ProductsID;
                        orderDetails.OrdersID = db.Orders.Last().OrdersId;
                        db.OrderDetails.Add(orderDetails);
                        db.Basket.Remove(item);
                        db.SaveChanges();
                        
                    }
                    foreach (var item in db.Basket.Where(x => x.CustomersID == id).ToList())
                    {
                        db.Basket.Remove(item);
                    }
                    return RedirectToAction(nameof(Index));
                }
                if (Session["ActiveCart"] != null)
                {

                    var Cart = (Cart)Session["ActiveCart"];

                    foreach (var item in Cart.Products)
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.ProductsID = item.Product.ProductsId;
                        orderDetails.OrdersID = db.Orders.OrderByDescending(x=>x.OrdersId).FirstOrDefault().OrdersId;
                        db.OrderDetails.Add(orderDetails);
                        db.SaveChanges();
                    }

                    Session.Remove("ActiveCart");
                    return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(CompleteShopping));
        }

        public void AddCart(int id)
        {
            if (Session["CurrentUserId"] != null)
            {
                int UserID = (int)Session["CurrentUserId"];
                var basketItem = db.Basket.Where(x => x.CustomersID == UserID && x.ProductsID == id).FirstOrDefault();
                if (basketItem != null)
                {
                    basketItem.Quantity++;
                    db.SaveChanges();
                }
                else
                {
                    Basket basketAdd = new Basket();
                    basketAdd.ProductsID = id;
                    basketAdd.Quantity=1;
                    basketAdd.CustomersID = (int)Session["CurrentUserId"];
                    db.Basket.Add(basketAdd);
                    db.SaveChanges();

                }
            }
            else
            {
                CartItem ci = new CartItem();
                Products product = db.Products.FirstOrDefault(x => x.ProductsId == id);
                ci.Product = product;
                //ci.Quantity = 1;
                ci.Discount = 0;
                Cart cart = new Cart();
                cart.AddCart(ci);
               
            }
        }

        public PartialViewResult MiniCartWidget()
        {

            if (Session["CurrentUser"] != null)
            {
                int id= (int)Session["CurrentUserId"];
                var basketList= db.Basket.Where(x => x.CustomersID == id).ToList();
                ViewBag.Basket = basketList;
                ViewBag.TotalPrice = basketList.Sum(x => x.Products.Price);
                ViewBag.Quantity = basketList.Sum(x => x.Quantity);

                return PartialView();
            }
            else if (Session["ActiveCart"] != null)
            {
                return PartialView((Cart)Session["ActiveCart"]);
            }
            else
            {

                return PartialView();
            }
        }

        public void CartProductRemove(int id)
        {

            if (Session["CurrentUser"] != null)
            {
                int CustomersID = (int)Session["CurrentUserId"];
                var basket = db.Basket.Where(x => x.CustomersID == CustomersID && x.ProductsID == id).FirstOrDefault();
                if (basket.Quantity > 1)
                {

                    basket.Quantity = basket.Quantity - 1; ;
                    db.SaveChanges();
                }
                else if (basket.Quantity == 1)
                {
                    db.Basket.Remove(basket);
                    db.SaveChanges();

                }
            }
            else
            {
                if (HttpContext.Session["ActiveCart"] != null)
                {
                    Cart c = (Cart)HttpContext.Session["ActiveCart"];

                    if (c.Products.FirstOrDefault(x => x.Product.ProductsId == id).Quantity > 1)
                    {
                        c.Products.FirstOrDefault(x => x.Product.ProductsId == id).Quantity--;
                    }
                    else
                    if (c.Products.FirstOrDefault(x => x.Product.ProductsId == id).Quantity == 1)
                    {
                        CartItem ci = c.Products.FirstOrDefault(x => x.Product.ProductsId == id);
                        c.Products.Remove(ci);
                    }
                }
            }
            
        }

        [ActFilter]
        public ActionResult ProductDetails(int id)
        {
            ParameterProduct viewModel = new ParameterProduct();
            var log = db.Logs.OrderByDescending(x => x.LogsId).Take(1).Single();
            var Products = db.Products.FirstOrDefault(x => x.ProductsId == id);
            viewModel.Product = Products;
            viewModel.ProductParameters = db.ProductParameters.Where(x => x.ProductsID == id).ToList();
            if (log.CategoriesID == null)
            {
                log.CategoriesID = Products.CategoriesID;
                db.SaveChanges();
            }


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddProductComments(ProductsComments productComments)
        {
            db.ProductsComments.Add(productComments);
            db.SaveChanges();
            return RedirectToAction("ProductDetails/" + productComments.ProductsID);
        }

        public ActionResult Icons()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View(db.OurTeam.ToList());
        }

        public ActionResult Contact()
        {
            var setting = db.Settings.FirstOrDefault(x => x.Id == 1);
            return View(setting);
        }

        [HttpPost]
        public ActionResult Contact(string receiver, string subject, string message)
        {
            MailMessage Message = new MailMessage();

            Message.To.Add("postmaster@sandbox0f013a90c699448e943d5140a46ca9d7.mailgun.org");
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = false;
            Message.From = new MailAddress(receiver);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("postmaster@sandbox0f013a90c699448e943d5140a46ca9d7.mailgun.org", "c8fa9337b79b98a3dd6198d0d018203a-816b23ef-c0bc0b43");
            smtp.Host = "smtp.mailgun.org";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            smtp.Send(Message);
            ViewBag.Message = "Mailiniz Uğurla göndərildi";

            return RedirectToAction("Contact");
        }

        public PartialViewResult ContactBestSellers()
        {
            
            return PartialView(db.Products.ToList().Take(5));
        }

        public PartialViewResult ContactBrands()
        {
            
            return PartialView(db.Brands.ToList().Take(5));
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customers customers)
        {


            customers.Password = Crypto.Hash(customers.Password);
            var customer = db.Customers.FirstOrDefault(x => x.Email == customers.Email && x.Password == customers.Password);
            if (customer != null)
            {
                Session["CurrentUser"] = customer;
                Session["CurrentUserId"] = customer.CustomersId;
                FormsAuthentication.SetAuthCookie(customer.Name, false);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ResultMessage"] = "Emailiniz və ya Şifrəniz düzgün deyil !! ";
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customers customers, string repassword)
        {
            if (db.Customers.FirstOrDefault(x => x.Email == customers.Email) == null)
            {
                if (customers.Password == repassword)
                {
                    if (customers.Password.Length > 5)
                    {
                        customers.Token = Guid.NewGuid();
                        customers.Password = Crypto.Hash(repassword);
                        Session["CurrentUser"] = customers;
                        Session["CurrentUserId"] = customers.CustomersId;

                        db.Customers.Add(customers);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ResultMessage"] = "Şifrəniz 6 elementden az olmamalıdır!! ";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["ResultMessage"] = "Şifrələriniz eyni deyil,yenidən yoxlayın.";
                    return RedirectToAction("Login");
                }
            }
            else
            {

                TempData["ResultMessage"] = "Bu email artıq istifadə olunur,başqa emaili yoxlayın";
                return RedirectToAction("Login");
            }

        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
        }

        public ActionResult Account()
        {
            if (Session["CurrentUserId"] != null)
            {
                var id = (int)Session["CurrentUserId"];
                var customers = db.Customers.FirstOrDefault(x => x.CustomersId == id);

                return View(customers);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult EditAccount(Customers customers)
        {
            var oldCustomer = db.Customers.FirstOrDefault(x => x.CustomersId == customers.CustomersId);
            oldCustomer.City = customers.City;
            oldCustomer.Name = customers.Name;
            oldCustomer.Street = customers.Street;
            oldCustomer.Phone = customers.Phone;
            oldCustomer.Email = customers.Email;
            oldCustomer.Token = Guid.NewGuid();
            db.SaveChanges();

            return RedirectToAction("Account", "Home");
        }
        

        public ActionResult Products(string search, string category, string brand,int? page)
        {
            if (search == null && category == null && brand == null)
            {
                return HttpNotFound();
            }
            ProductBrands productBrands = new ProductBrands();
            int PageSkip=0;
            if (page != null)
            {
                 PageSkip = (int)((page-1) * 2);
            }
            var products = db.Products.ToList();
            productBrands.Categories = db.Categories.ToList();
            productBrands.Brands = db.Brands.ToList();

            if (search != null)
            {
                ViewBag.Search = search;
                var Products = products.Where(x => x.Name.Contains(search) || x.Categories.Name.Contains(search) || x.Brands.Name.Contains(search) || x.Name.Contains(search.ToLower()) || x.Categories.Name.Contains(search.ToLower()) || x.Brands.Name.Contains(search.ToLower()) || x.Name.Contains(search.ToUpper()) || x.Categories.Name.Contains(search.ToUpper()) || x.Brands.Name.Contains(search.ToUpper()) || x.Name.Contains(search.ToTitleCase()) || x.Categories.Name.Contains(search.ToTitleCase()) || x.Brands.Name.Contains(search.ToTitleCase())).ToList();

                productBrands.PageCount = Products.Count / 2+1;
                
                productBrands.Products = Products.Skip(PageSkip).Take(2).ToList();
                return View(productBrands);
            }else
            if (category != null)
            {
                ViewBag.Category = category;
                var cateoryId = db.Categories.Where(x=>x.Name.Contains(category)).FirstOrDefault().CategoriesId;
                var Products = products.Where(x => x.Categories.Name == category||x.Categories.SubCategoryID==cateoryId).ToList();
                productBrands.PageCount = Products.Count / 2+1;
                productBrands.Products = Products.Skip(PageSkip).Take(2).ToList();
                return View(productBrands);
            }
            else
            if (brand != null)
            {
                ViewBag.Brands = brand;
                var Products = products.Where(x => x.Brands.Name == brand).ToList();

                productBrands.PageCount = Products.Count / 12 + 1;
                productBrands.Products = Products.Skip(PageSkip).Take(12).ToList();
                return View(productBrands);
            }
            else
            {

                return HttpNotFound();
            }
        }

        [Route("yandex_df8681b359789bad")]
        public ActionResult yandex_df8681b359789bad()
        {
            return View();
        }
    }
}
