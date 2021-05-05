using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FarmersMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace CFarmersMarket.Controllers
{
    public class HomeController : Controller
    {
        public User LoggedInUser()
        {
            int? LoggedID = HttpContext.Session.GetInt32("UserId");
            User logged = _context.Users.FirstOrDefault(u => u.UserId == LoggedID);
            return logged;
        }
        public int UserID()
        {
            int UserID = LoggedInUser().UserId;
            return UserID;
        }
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                Console.WriteLine(newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                Console.WriteLine(newUser.UserId);
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Homepage");
            }
            return View("Index");
        }
        [HttpGet("Homepage")]
        public IActionResult Homepage()
        {
            // if (LoggedInUser() == null)
            // {
            //     return RedirectToAction("Index");
            // }
            ViewBag.loggedUser = LoggedInUser();
            List<Vendor> allVendors = _context.Vendors
            .ToList();
            return View(allVendors);
        }

        [HttpPost("login")]
        public IActionResult LoginUser(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var dbuser = _context.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);
                if (dbuser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(loginUser, dbuser.Password, loginUser.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", dbuser.UserId);
                return RedirectToAction("Homepage");
            }
            return View("Index");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            if (UserID() == 1)
            {
                ViewBag.loggedUser = LoggedInUser();
                return View();
            }
            ViewBag.loggedUser = LoggedInUser();
            return RedirectToAction("Homepage");
        }
        [HttpGet("AllVendors")]
        public IActionResult AllVendors()
        {
            if (LoggedInUser() == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.loggedUser = LoggedInUser();
            List<Vendor> allVendors = _context.Vendors
            .Include(v => v.VendorCategories)
            .ToList();
            return View(allVendors);
        }
        [HttpGet("vendorform")]
        public IActionResult VendorForm()
        {
            return View();
        }

        [HttpPost("addvendor")]
        public IActionResult AddVendor(Vendor newVendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newVendor);
                _context.SaveChanges();
                return Redirect("Homepage");
            }
            else
            {
                return View("admin");
            }
        }
        [HttpGet("AllCategories")]
        public IActionResult AllCategories()
        {
            if (LoggedInUser() == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.loggedUser = LoggedInUser();
            List<Category> allCategories = _context.Categories
            .Include(v => v.ProductCategories)
            .ThenInclude(p => p.Product)
            .Include(c => c.VendorCategories)
            .OrderBy(a => a.Name)
            .ToList();
            return View(allCategories);
        }

        [HttpGet("categoryform")]
        public IActionResult CateogryForm()
        {
            return View();
        }
        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpPost("addcategory")]
        public IActionResult AddCategory(Category newCateogry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newCateogry);
                _context.SaveChanges();
                return Redirect("Homepage");
            }
            else
            {
                return View("admin");
            }
        }
        [HttpGet("allproducts")]
        public IActionResult AllProducts()
        {
            if (LoggedInUser() == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.loggedUser = LoggedInUser();
            List<Product> allProducts = _context.Products
            .Include(v => v.ProductCategory)
            .ThenInclude(c => c.Category)
            .Include(v => v.Vendor)
            .ToList();
            return View(allProducts);
        }

        [HttpGet("productform")]
        public IActionResult ProductForm()
        {
            ViewBag.AllCategories = _context.Categories
                .ToList();
            return View();
        }

        [HttpPost("addproduct")]
        public IActionResult AddProduct(Product newProduct, string C1, string C2, string C3)
        {
            // Maybe this might work ¯\_(ツ)_/¯ ?
            if (ModelState.IsValid)
            {
                Console.WriteLine($"\n\n\nFirst Category ID:{C1}\n Second Category ID:{C2}\n Third Category ID:{C3}\n\n\n");
                List<string> cats = new List<string>() { C1, C2, C3 };
                _context.Add(newProduct);
                _context.SaveChanges();
                foreach (var cat in cats)
                {
                    if (cat != "null")
                    {
                        ProductCategory n1 = new ProductCategory();
                        n1.ProductId = newProduct.ProductId;
                        n1.CategoryId = Int32.Parse(cat);
                        _context.Add(n1);
                        _context.SaveChanges();
                    }
                }

                return Redirect($"/product/{newProduct.ProductId}");
            }
            else
            {
                return View("admin");
            }
        }

        [HttpGet("vendor/{VendorId}")]
        public IActionResult ShowVendor(int VendorId)
        {
            ViewBag.loggedUser = LoggedInUser();
            ViewBag.Show = _context.Vendors
                                .Include(p => p.Products)
                                .Include(vr => vr.VendorReviews)
                                    .ThenInclude(r => r.Review)
                                        .ThenInclude(r => r.Reviewer)
                                .Include(vc => vc.VendorCategories)
                                    .ThenInclude(c => c.Category)
                                .Include(v => v.Products)
                                .FirstOrDefault(v => v.VendorId == VendorId);

            return View();
        }

        [HttpGet("product/{ProductId}")]
        public IActionResult ShowProduct(int ProductId)
        {
            ViewBag.loggedUser = LoggedInUser();
            ViewBag.Show = _context.Products
            .Include(v => v.Vendor)
            .Include(pr => pr.ProductReview)
            .ThenInclude(r => r.Review)
            .ThenInclude(u => u.Reviewer)
            .Include(pr => pr.ProductCategory)
            .ThenInclude(c => c.Category)
            .FirstOrDefault(v => v.ProductId == ProductId);
            return View();
        }

        [HttpGet("cart")]
        public IActionResult Cart()
        {
            ViewBag.loggedUser = LoggedInUser();

            User AllItems = _context.Users
                .Include(c => c.Cart)
                    .ThenInclude(p => p.Products)
                .FirstOrDefault(c => c.UserId == UserID());
            ViewBag.Total = AllItems.Cart.Sum(p => p.Products.Price);
            return View(AllItems);
        }
        [HttpGet("addtocart/{UserId}/{ProductId}")]
        public IActionResult addToCart(int UserId, int ProductId)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (p.Quantity > 0)
            {
                p.Quantity = p.Quantity - 1;
                Cart Cart = new Cart();
                Cart.UserId = UserId;
                Cart.ProductId = ProductId;
                _context.Add(Cart);
            }
            _context.SaveChanges();
            return Redirect("/allproducts");
        }

        [HttpGet("removefromcart/{Userid}/{Productid}")]
        public IActionResult RemoveFromCart(int UserId, int ProductId)
        {
            Cart Cart = _context.Carts
                .FirstOrDefault(u => u.UserId == UserId && u.ProductId == ProductId);
            _context.Remove(Cart);
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            p.Quantity = p.Quantity + 1;
            _context.SaveChanges();
            return Redirect("/cart");
        }

        [HttpPost("review/{type}/{id}")]
        public IActionResult Review(Review rev, string type, int id)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rev);
                _context.SaveChanges();
                if (type == "Product")
                {
                    ProductReview newPR = new ProductReview();
                    newPR.ProductId = id;
                    newPR.ReviewId = rev.ReviewId;
                    _context.Add(newPR);
                    _context.SaveChanges();
                    return RedirectToAction("Homepage");
                }
                if (type == "Vendor")
                {
                    VendorReview newVR = new VendorReview();
                    newVR.VendorId = id;
                    newVR.ReviewId = rev.ReviewId;
                    _context.Add(newVR);
                    _context.SaveChanges();
                    return RedirectToAction("Homepage");
                }
                return RedirectToAction("Homepage");
            }
            else
            {
                return View();
            }
        }

        [HttpGet("checkout")]
        public IActionResult checkout()
        //I can't rename the cshtml to capital c so change later
        {
            ViewBag.loggedUser = LoggedInUser();
            User AllItems = _context.Users
                .Include(c => c.Cart)
                    .ThenInclude(p => p.Products)
                .FirstOrDefault(c => c.UserId == UserID());
            ViewBag.Total = AllItems.Cart.Sum(p => p.Products.Price);
            return View();
        }

    }
}
