using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_ASP.NET_NHOM_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        
        public ActionResult OrderLookup()//Tra cứu đơn hàng
        {
            return View();
        }
        public ActionResult ViewProductsByCategory()//Xem sản phẩm theo danh muc
        {
            return View();
        }
        public ActionResult ViewProductDetails()//Xem chi tiết sản phẩm
        {
              return View();
        }
        public ActionResult ViewTerms()//Xem điều khoản
        {
            return View();
        }
        public ActionResult ViewPosts()//Xem bài viết
        {
            return View();
        }
        public ActionResult ShoppingCartManagement()//Quản lý giỏ hàng
        {
            return View();
        }
    }
}