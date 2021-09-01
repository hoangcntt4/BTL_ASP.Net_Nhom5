using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDT { get; set; }
        public string HinhAnh { get; set; }
    }
}