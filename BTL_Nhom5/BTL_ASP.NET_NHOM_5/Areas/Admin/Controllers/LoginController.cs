using BTL_ASP.NET_NHOM_5.Areas.Admin.Models;
using BTL_ASP.NET_NHOM_5.Common;
using BTL_ASP.NET_NHOM_5.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_ASP.NET_NHOM_5.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModelAdmin model)
        {
            if (ModelState.IsValid)
            {
                var dao = new QuanTriDao();
                var result = dao.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.HinhAnh = user.Avatar;
                    userSession.Name = user.HoTen;
                    Session.Add(CommonConstants.USER_SESSION_ADMIN, userSession);
                    return RedirectToAction("Index", "ThongKes");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
               
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
               
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
    }
}