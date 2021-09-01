using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Areas.Admin.Models
{
    public class LoginModelAdmin
    {
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}