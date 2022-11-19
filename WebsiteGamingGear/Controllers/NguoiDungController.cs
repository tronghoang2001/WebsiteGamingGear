using GamingGear.Others;
using WebsiteGamingGear.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace GamingGear.Controllers
{
    public class NguoiDungController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu dbGamingGear
        dbGamingGear db = new dbGamingGear();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        private List<SanPham> LaySanPham(int count)
        {
            //Sap xep giam dan theo ngay them 
            return db.SanPhams.OrderByDescending(sp => sp.ngayThem).Take(count).ToList();
        }
        //Sản phẩm hiển thị ở trang chủ
        public ActionResult TrangChu()
        {
            //Lay san pham
            var sanpham = LaySanPham(6);           
            return View(sanpham);
        }
        public ActionResult LaySPTheoLuotXem()
        {
            //Lay san pham
            var sanpham = from sp in db.SanPhams orderby sp.luotXem descending select sp;
            return View(sanpham);
        }
        public ActionResult LaySPTheoLuotMua()
        {
            //Lay san pham
            var sanpham = from sp in db.SanPhams orderby sp.luotMua descending select sp;
            return View(sanpham);
        }
        public ActionResult LaySPTheoManHinh()
        {
            //Lay san pham
            var sanpham = from sp in db.SanPhams where sp.idDanhMuc == 1 select sp;
            return View(sanpham);
        }
        public ActionResult LaySPTheoLaptop()
        {
            //Lay san pham
            var sanpham = from sp in db.SanPhams where sp.idDanhMuc == 9 select sp;
            return View(sanpham);
        }
        public ActionResult LaySPTheoCard()
        {
            //Lay san pham
            var sanpham = from sp in db.SanPhams where sp.idDanhMuc == 10 select sp;
            return View(sanpham);
        }
        public ActionResult LaySPTheoBanPhim()
        {
            //Lay san pham
            var sanpham = from sp in db.SanPhams where sp.idDanhMuc == 4 select sp;
            return View(sanpham);
        }
        //Sản phẩm hiển thị ở trang sản phẩm
        public ActionResult SanPham(int ? page)
        {
            //Tao bien quy dinh so san pham tren moi trang
            int pageSize = 12;
            //Tao bien so trang
            int pageNum = (page ?? 1);
            //Lay 36 san pham moi nhat 
            var sanpham = LaySanPham(36);            
            return View(sanpham.ToPagedList(pageNum,pageSize));
        }
        
        //Chi tiết sản phẩm 
        public ActionResult ChiTietSanPham(int id)
        {
            var sanpham = from sp in db.SanPhams where sp.idSanPham == id select sp;
            //tang 1 luot xem
            var countview = db.SanPhams.FirstOrDefault(m => m.idSanPham == id);
            countview.luotXem++;
            db.SaveChanges();
            return View(sanpham.Single());
        }
        //San pham lien qua
        public ActionResult SanPhamLienQuan(int iddanhmuc, int id)
        {
            var splq = from sp in db.SanPhams where sp.idDanhMuc == iddanhmuc where id != sp.idSanPham select sp;
            return View(splq);
        }
        //Lay Danh muc
        public ActionResult DanhMuc()
        {
            var danhmuc = from dm in db.DanhMucSPs select dm;
            return PartialView(danhmuc);
        }
        //Lay the loai
        public ActionResult TheLoai(int id)
        {
            var theloai = from tl in db.TheLoaiSPs where tl.idDanhMucSP == id select tl;
            return PartialView(theloai);
        }
        //Lay Thuong hieu
        public ActionResult ThuongHieu()
        {
            var thuonghieu = from th in db.ThuongHieux select th;
            return PartialView(thuonghieu);
        }
        //Lay san pham theo danh muc
        public ActionResult SPTheoDanhMuc(int id, string searchString)
        {
            var sanpham = from sp in db.SanPhams where sp.idDanhMuc == id select sp;
            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(s => s.tenSanPham.Contains(searchString));
            }
            return View(sanpham);
        }
        //Lấy sản phẩm theo thể loại
        public ActionResult SPTheoTheLoai(int idtheloai)
        {
            var sanpham = from sp in db.SanPhams where sp.idTheLoaiSP == idtheloai select sp;
            return View(sanpham);
        }
        //Lay san pham theo thuong hieu
        public ActionResult SPTheoThuongHieu(int id, string searchString)
        {
            var sanpham = from sp in db.SanPhams where sp.idThuongHieu == id select sp;
            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(s => s.tenSanPham.Contains(searchString));
            }
            return View(sanpham);
        }

        //Gui lien he
        public ActionResult LienHe()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LienHe(FormCollection collection)
        {
            LienHe lienhe = new LienHe();
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            var noidunglh = collection["noiDungLH"];

            lienhe.idTaiKhoan = tk.id;
            lienhe.tenNguoiLH = tk.ten;
            lienhe.soDienThoai = tk.soDienThoai;
            lienhe.email = tk.email;
            lienhe.noiDung = noidunglh;
            if (ModelState.IsValid)
            {
                db.LienHes.Add(lienhe);
                db.SaveChanges();
            }
            return RedirectToAction("XacNhanLienHe");
        }
        public ActionResult XacNhanLienHe()
        {
            return View();
        }

        //Tin tuc
        public List<TinTuc> DSTinTuc(int count)
        {
            return db.TinTucs.OrderByDescending(a => a.idTinTuc).Take(count).ToList();
        }

        public ActionResult TinTuc(int? page)
        {
            //Tao bien quy dinh so san pham tren moi trang
            int pageSize = 6;
            //Tao bien so trang
            int pageNum = (page ?? 1);
            //Lay 15 tin tuc
            var tintucmoi = DSTinTuc(15);
            return View(tintucmoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult ChiTietTinTuc(int id)
        {
            var tintuc = from tt in db.TinTucs where tt.idTinTuc == id select tt;
            return View(tintuc.Single());
        }
        //Lay Danh muc tin tuc
        public ActionResult DanhMucTT()
        {
            var danhmuc = from dm in db.DanhMucTinTucs select dm;
            return PartialView(danhmuc);
        }
        //Lay the loai tin tuc
        public ActionResult TheLoaiTT(int id)
        {
            var theloai = from tl in db.TheLoaiTinTucs where tl.idDanhMucTT == id select tl;
            return PartialView(theloai);
        }
        //Lay tin tuc theo danh muc
        public ActionResult TTTheoDanhMuc(int id, string searchString)
        {
            var sanpham = from tt in db.TinTucs where tt.idDanhMucTT == id select tt;
            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(s => s.tieuDeTin.Contains(searchString));
            }
            return View(sanpham);
        }
        //Lấy tin tuc theo thể loại
        public ActionResult TTTheoTheLoai(int idtheloai)
        {
            var sanpham = from tt in db.TinTucs where tt.theLoaiTin == idtheloai select tt;
            return View(sanpham);
        }
        //Tin tuc lien quan
        public ActionResult TinTucLienQuan(int iddanhmuc, int id)
        {
            var ttlq = from tt in db.TinTucs where tt.idDanhMucTT == iddanhmuc where id != tt.idTinTuc select tt;
            return View(ttlq);
        }

        //Bình luận tin tức
        public ActionResult ThemBinhLuanTT(FormCollection collection, int id)
        {
            BinhLuanTinTuc binhluan = new BinhLuanTinTuc();
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            var tintuc = from tt in db.TinTucs where tt.idTinTuc == id select tt;
            var noidungbl = collection["noiDungBL"];
            if(tk == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                binhluan.idTaiKhoan = tk.id;
                binhluan.idTinTuc = id;
                binhluan.noiDungBL = noidungbl;
                binhluan.trangThai = "1";
                binhluan.tenNguoiBL = tk.ten;
                binhluan.ngayThem = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.BinhLuanTinTucs.Add(binhluan);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ChiTietTinTuc", new { id = id});
        }
        public ActionResult BinhLuanTT(int id)
        {
            //Lay binh luan
            var binhluan = (from tt in db.BinhLuanTinTucs orderby tt.ngayThem descending where tt.idTinTuc == id select tt).Take(5);
            return View(binhluan);
        }

        //Bình luận sản phẩm
        public ActionResult ThemBinhLuanSP(FormCollection collection, int idsanpham)
        {
            DanhGia binhluan = new DanhGia();
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            var tintuc = from sp in db.SanPhams where sp.idSanPham == idsanpham select sp;
            var noidungbl = collection["noiDungBL"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                binhluan.idNguoiDanhGia = tk.id;
                binhluan.idSanPham = idsanpham;
                binhluan.noiDung = noidungbl;
                binhluan.trangThai = "1";
                binhluan.tenNguoiDanhGia = tk.ten;
                binhluan.ngayThem = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.DanhGias.Add(binhluan);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ChiTietSanPham", new { id = idsanpham});
        }
        //Hiển thị bình luận sản phẩm
        public ActionResult BinhLuanSP(int id)
        {
            //Lay binh luan
            var binhluan = (from dg in db.DanhGias orderby dg.ngayThem descending where dg.idSanPham == id select dg).Take(5);
            return View(binhluan);
        }
    }
}