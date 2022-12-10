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
        public string diaChi { get; set; }

        public int idTaiKhoan { get; set; }
        //Tai khoan
        public string taiKhoan { get; set; }
        //Mat khau
        public string matKhau { get; set; }
        public string xacNhanMatKhau { get; set; }
        //Email
        public string Email { get; set; }
        //Request Code
        public string Requestcode { get; set; }
        //Ho ten
        public string ten { get; set; }
        //So dien thoai
        public string soDienThoai { get; set; }
        //Anh dai dien
        public string anhDaiDien { get; set; }
        //Ngay sinh
        public DateTime ngaySinh { get; set; }
        //Gioi tinh
        public string gioiTinh { get; set; }

        //Quyen
        [StringLength(1)] public string quyen { get; set; }

        //Trang thai
        [StringLength(1)] public string trangThai { get; set; }
    }
}