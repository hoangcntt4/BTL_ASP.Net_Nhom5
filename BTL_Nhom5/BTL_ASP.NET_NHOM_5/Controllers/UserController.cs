using BTL_ASP.NET_NHOM_5.Common;
using BTL_ASP.NET_NHOM_5.Dao;
using BTL_ASP.NET_NHOM_5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BTL_ASP.NET_NHOM_5.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        TrueMartDb db = new TrueMartDb();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }
        public ActionResult QuanLyTaiKhoan()
        {

            return View();
        }
        public ActionResult changepassword()
        {

            return View();
        }
        public ActionResult Edit()
        {

            return View();
        }
        public PartialViewResult userManager()
        {

            return PartialView();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserName = Request["username"];
                model.Password = Request["password"];
                var dao = new KhachHangDao();
                var result = dao.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.TenDangNhap;
                    userSession.UserID = user.ID;
                    userSession.Name = user.HoTen;
                    userSession.Email = user.Email;
                    userSession.SoDT = user.SoDT;
                    userSession.DiaChi = user.DiaChi;
                    userSession.HinhAnh = user.Avatar;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
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
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(KhachHang model)
        {

            var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
            model = db.KhachHangs.Where(x => x.ID == userSession.UserID).SingleOrDefault();
            model.HoTen = Request["fullName"];
            model.SoDT = Request["mobile"];
            model.DiaChi = Request["address"];
            if (model.HoTen == "" || model.SoDT == "" || model.DiaChi == "")
            {
                ModelState.AddModelError("", "Bạn phải điền đủ thông tin.");
            }
            else
            {
                db.SaveChanges();
                userSession.Name = model.HoTen;
                userSession.SoDT = model.SoDT;
                userSession.DiaChi = model.DiaChi;
                Session[CommonConstants.USER_SESSION] = null;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return View("QuanLyTaiKhoan");
            }


            return View(model);

        }
        [HttpPost]
        public ActionResult changepassword(KhachHang model)
        {

            var oldpass = Request["oldpassword"];
            var newpass = Request["newpassword"];
            var repass = Request["repassword"];
            var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
            model = db.KhachHangs.Where(x => x.ID == userSession.UserID).SingleOrDefault();
            if (oldpass == "" || newpass == "" || repass == "")
            {
                ModelState.AddModelError("", "Bạn phải điền đủ thông tin.");
            }
            else
            if (!model.MatKhau.Equals(oldpass))
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng");
            }
            else
            if (!newpass.Equals(repass))
            {
                ModelState.AddModelError("", "Mật khẩu nhập lại không đúng");
            }
            if (newpass.Length < 6)
            {
                ModelState.AddModelError("", "Mật khẩu phải lớn hơn 6 ký tự.");
            }
            else
            {
                model.MatKhau = newpass;
                db.SaveChanges();
                return View("QuanLyTaiKhoan");
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                model.Name = Request["fullName"];
                model.UserName = Request["userName"];
                model.Email = Request["email"];
                model.Password = Request["password"];
                model.Phone = Request["mobile"];
                model.Address = Request["adress"];

                var dao = new KhachHangDao();
                if (model.Name == "")
                {
                    ModelState.AddModelError("", "Yêu cầu nhập họ tên");

                }
                else
                if (dao.CheckTenDangNhap(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new KhachHang();
                    user.HoTen = model.Name;
                    user.TenDangNhap = model.UserName;
                    user.MatKhau = model.Password;
                    user.SoDT = model.Phone;
                    user.Email = model.Email;
                    user.DiaChi = model.Address;
                    user.CreatedDate = DateTime.Now;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }


        public ActionResult ForgotPassword()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/User/ResetPassword/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            using (var context = new TrueMartDb())
            {
                var getUser = (from s in context.KhachHangs where s.Email == EmailID select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPasswordCode = resetCode;

                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 

                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Reset mật khẩu - TrueMart";
                    var body = "Xin chào " + getUser.HoTen + ", <br/> Bạn đã gửi yêu cầu lấy lại mật khẩu, kích vào link bên dưới để thực hiện. " +

                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "Nếu không phải là bạn, vui lòng bỏ qua email này.<br/><br/> Xin cảm ơn";

                    SendEmail(getUser.Email, body, subject);

                    ViewBag.Message = "Vui lòng kiểm tra email của bạn ("+ EmailID+")";
                }
                else
                {
                    ViewBag.Message = "Email này chưa được đăng ký";
                    return View();
                }
            }

            return View();
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("modter111000@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("modter111000@gmail.com", "hoang111000");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (var context = new TrueMartDb())
            {
                var user = context.KhachHangs.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new TrueMartDb())
                {
                    var user = context.KhachHangs.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        //you can encrypt password here, we are not doing it
                        user.MatKhau = model.NewPassword;
                        //make resetpasswordcode empty string now
                        user.ResetPasswordCode = "";
                        //to avoid validation issues, disable it
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "Cập nhật mật khẩu thành công";
                    }
                }
            }
            else
            {
                message = "Có lỗi xảy ra";
            }
            ViewBag.Message = message;
            return View(model);
        }

    }

    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu mới", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}