using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingGear.Others
{
    public class LuuThongTinDangNhap
    {
		public int idTaiKhoan { get; set; }
		public string ten { get; set; }
		public string email { get; set; }
		public bool them { get; set; }
		public bool xoa { get; set; }
		public bool sua { get; set; }
		public bool doc { get; set; }
		public bool capNhat { get; set; }
		public int idQuyen { get; set; }
		public string tenQuyen { get; set; }
		public string anhDaiDien { get; set; }
		public string soDienThoai { get; set; }
	}
}