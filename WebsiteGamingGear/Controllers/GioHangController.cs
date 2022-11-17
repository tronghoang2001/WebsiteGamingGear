﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteGamingGear.Models;

namespace WebsiteGamingGear.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        //Tạo đối tượng data chứa dữ liệu từ model dbGamingGear
        dbGamingGear data = new dbGamingGear();
        //Lấy giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGioHang
                lstGioHang = new List<GioHang>();
                Session["Giohang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int iIDSanPham, string strURL)
        {
            //Lấy ra Session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //tang 1 luot mua
            var countbuy = data.SanPhams.FirstOrDefault(m => m.idSanPham == iIDSanPham);
            countbuy.luotMua++;
            data.SaveChanges();
            //Kiểm tra sản phẩm này có tồn tại trong Session["Giohang"] chưa?
            GioHang sanpham = lstGioHang.Find(n => n.iIdSanPham == iIDSanPham);
            if (sanpham == null)
            {
                sanpham = new GioHang(iIDSanPham);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
            
        }
        //Tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tổng tiền
        private int TongTien()
        {
            int iTongTien = 0;
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.iThanhTien);
            }
            return iTongTien;
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "NguoiDung");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGioHang);
        }
        //Tạo PartialView để hiển thị thông tin giỏ hàng
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //Lấy giỏ hàng từ Session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sản phẩm đã có trong giỏ hàng Session["Giohang"]?
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iIdSanPham == iMaSP);
            //Nếu tồn tại thì sửa soLuong
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iIdSanPham == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "NguoiDung");
            }
            return RedirectToAction("GioHang");
        }
        //Cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Lấy giỏ hàng từ Session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sản phẩm đã có trong Session["Giohang"]?
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iIdSanPham == iMaSP);
            //Nếu tồn tại thì cho sửa soLuong
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //Xóa tất cả giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            //Lấy giỏ hàng từ Session
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("TrangChu", "NguoiDung");
        }

        //Dat hang
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiểm tra đã đăng nhập chưa?
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "NguoiDung");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            var tennguoinhan = collection["tenNguoiNhan"];
            var diachinguoinhan = collection["diaChiNguoiNhan"];
            var sđtnguoinhan = collection["sđtNguoiNhan"];
            var ghichudonhang = collection["ghiChuDH"];

            ddh.idTaiKhoan = tk.id;
            ddh.tenNguoiDat = tk.ten;
            ddh.ngayDat = DateTime.Now;
            ddh.tenNguoiNhan = tennguoinhan;
            ddh.diaChiNguoiNhan = diachinguoinhan;
            ddh.sdtNguoiNhan = sđtnguoinhan;
            ddh.ghiChu = ghichudonhang;
            ddh.tongTien = TongTien();
            data.DonDatHangs.Add(ddh);
            data.SaveChanges();
            //thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                ChiTietDDH ct = new ChiTietDDH();
                ct.idDDH = ddh.idDDH;
                ct.idSanPham = item.iIdSanPham;
                ct.soLuong = item.iSoLuong;
                ct.gia = item.iDonGia;
                data.ChiTietDDHs.Add(ct);
            }
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}