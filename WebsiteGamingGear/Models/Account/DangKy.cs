using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace GamingGear.Models.Account
{
    public class DangKy
    {
        [Required(ErrorMessage = "Chọn Tỉnh/Thành Phố")]
        public int idTinhThanh { get; set; }
        [Required(ErrorMessage = "Quận/Huyện")]
        public int idQuanHuyen { get; set; }
        [Required(ErrorMessage = "Chọn Phường xã")]
        public int idPhuongXa { get; set; }

        public int idTaiKhoan { get; set; }
        //Tai khoan
        [Required(ErrorMessage = "Nhập tài khoản")]
        [StringLength(50, ErrorMessage = "Tài khoản không quá 50 ký tự", MinimumLength = 1)]
        public string taiKhoan { get; set; }
        //Mat khau
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [StringLength(50)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]))(?=.*[#$^+=!*()@%&]).{8,}$",
        ErrorMessage = "Mật khẩu tổi thiếu 8 ký tự bao gồm: chữ thường, chữ hoa, chữ số và 1 ký tự đặc biệt")]
        public string matKhau { get; set; }
        public string xacNhanMatKhau { get; set; }
        //Email
        [Required(ErrorMessage = "Nhập Email")]
        [StringLength(50, ErrorMessage = "Email tối thiểu 6 ký tự", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; }
        //Request Code
        [StringLength(100)]
        public string Requestcode { get; set; }
        //Ho ten
        [Required(ErrorMessage = "Nhập họ tên")]
        [StringLength(50, ErrorMessage = "Họ tên tối đa 50 ký tự", MinimumLength = 1)]
        public string ten { get; set; }
        //So dien thoai
        [StringLength(10)]
        [Required(ErrorMessage = "Nhập số điện thoại")]
        [MinLength(10, ErrorMessage = "Số diện thoại phải đủ 10 chữ số")]
        [RegularExpression("^(0)([0-9]{9})$",
        ErrorMessage = "Số điện thoại phải bắt đầu bằng 0, chứa ký tự số từ (0 -> 9) và đủ 10 chữ số")]
        public string soDienThoai { get; set; }
        //Anh dai dien
        [StringLength(200, ErrorMessage = "Ảnh đại diện không được quá 200 ký tự")]
        public string anhDaiDien { get; set; }
        //Ngay sinh
        [Required(ErrorMessage = "Nhập Ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ngaySinh { get; set; }
        //Gioi tinh
        [StringLength(1)]

        [Required(ErrorMessage = "Chọn giới tính")]
        public string gioiTinh { get; set; }

        //Quyen
        [Required(ErrorMessage = "Chọn quyền")]
        [StringLength(1)] public string quyen { get; set; }

        //Trang thai
        [StringLength(1)] public string trangThai { get; set; }
    }
}