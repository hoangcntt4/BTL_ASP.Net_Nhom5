using BTL_ASP.NET_NHOM_5.Dao;
using BTL_ASP.NET_NHOM_5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BTL_ASP.NET_NHOM_5.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        public int mahoadon { get; set; }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            decimal total = 0;
            foreach (var item in list)
            {
                var donGia = Convert.ToDecimal(item.Product.DonGia) - Convert.ToDecimal(item.Product.DonGia) * Convert.ToDecimal(item.Product.KhuyenMai.Discount);

                total += (donGia * item.Quantity);
            }
            ViewBag.tongtien = total;
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.MaSP == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.Where(x => x.Product.MaSP == item.Product.MaSP).SingleOrDefault();
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new SanPhamDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.MaSP == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.MaSP == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Add(long productId, int quantity)
        {
            var product = new SanPhamDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.MaSP == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.MaSP == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return Redirect(Request.UrlReferrer.ToString()); ;
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var session = (UserLogin)Session[BTL_ASP.NET_NHOM_5.Common.CommonConstants.USER_SESSION];
            var order = new HoaDon();
            order.CreatedDate = DateTime.Now;
            shipName = Request["shipName"];
            mobile = Request["mobile"];
            address = Request["address"];
            email = Request["email"];
            order.TrangThai = "Chờ xác nhận";
            order.ID = Convert.ToInt32(session.UserID);
            order.CreatedBy = session.UserName;


            try
            {

                var id = new OrderDao().Insert(order);
                Session["madh"] = Convert.ToInt32(id);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var donGia = Convert.ToDecimal(item.Product.DonGia) - Convert.ToDecimal(item.Product.DonGia) * Convert.ToDecimal(item.Product.KhuyenMai.Discount);
                    var orderDetail = new ChiTietHoaDon();
                    orderDetail.MaSP = item.Product.MaSP;
                    orderDetail.MaHD = Convert.ToInt32(id);
                    orderDetail.DonGia = donGia;
                    orderDetail.SoLuong = item.Quantity;
                    detailDao.Insert(orderDetail);


                    total += (donGia * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception ex)
            {
                //ghi log

                return Redirect("/loi");
            }
            Session[CartSession] = null;
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            ViewBag.ma = Session["madh"];
            return View();
        }
    }
}