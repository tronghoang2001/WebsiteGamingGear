using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GamingGear.Others
{
    public static class Authentication
    {
		public static string Ten(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().ten;
		}
		public static string AnhDaiDien(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().anhDaiDien;
		}
		public static int ID(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().idTaiKhoan;
		}
		public static string Email(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().email;
		}
		public static string SoDienThoai(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().soDienThoai;
		}
		public static int IDQuyen(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().idQuyen;
		}
		public static string TenQuyen(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().tenQuyen;
		}
		public static bool Them(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().them;
		}
		public static bool Xoa(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().xoa;
		}
		public static bool Sua(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().sua;
		}
		public static bool Doc(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().doc;
		}
		public static bool CapNhat(this IIdentity identity)
		{
			return identity.LayThongTinNguoiDung().capNhat;
		}

		public static LuuThongTinDangNhap LayThongTinNguoiDung(this IIdentity identity)
		{
			var jsonUserData = HttpContext.Current.User.Identity.Name;
			var userData = JsonConvert.DeserializeObject<LuuThongTinDangNhap>(jsonUserData);
			return userData;
		}
	}
}