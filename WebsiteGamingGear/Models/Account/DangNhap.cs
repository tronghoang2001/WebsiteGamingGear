using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingGear.Models.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class DangNhap
    {
        public int idTaiKhoan { get; set; }
        //Tài Khoản
        [Required(ErrorMessage = "Nhập tài khoản")]
        public string taiKhoan { get; set; }
        //Mật khẩu
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string matKhau { get; set; }
        //Roles
        [StringLength(1)] public string quyen { get; set; }
        //Name
        public string ten { get; set; }
        //Avatar
        public string anhDaiDien { get; set; }
    }
}