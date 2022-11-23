using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteGamingGear.Models;

namespace WebsiteGamingGear.Controllers
{
    public class AdminController : Controller
    {
        dbGamingGear db = new dbGamingGear();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //San Pham
        public ActionResult SanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            //return View(db.SanPhams.ToList());
            return View(db.SanPhams.ToList().OrderBy(n => n.idSanPham).ToPagedList(pageNumber, pageSize));
        }
        //THÊM MỚI SẢN PHẨM
        [HttpGet]
        public ActionResult ThemSanPham()
        {

            //dua du lieu vao dropdownlist
            //lay ds tu table chu de, sap xep tang dan theo ten chu de, chon lay gia tri ma CD, hien thi ten CD
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSanPham(SanPham sanPham, HttpPostedFileBase fileUpload)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh ";
                return View();
            }
            else
            {

                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan cua file
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                //kiem tra hinh anh ton tai chua
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    // luu hinh anh vao duong dan
                    fileUpload.SaveAs(path);
                    sanPham.hinhAnh = fileName;
                    sanPham.trangThai = "1";
                    //luu vao CSDL
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("SanPham");
                }
            }
        }
        //XÓA SẢN PHẨM
        [HttpGet]
        public ActionResult XoaSanPham(int id)
        {
            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.idSanPham == id);
            ViewBag.idSanPham = sanPham.idSanPham;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);

        }
        [HttpPost, ActionName("Xoasanpham")]
        public ActionResult XacNhanXoa(int id)
        {
            //LẤY RA ĐỐI TƯỢNG SẢN PHẨM THEO MÃ
            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.idSanPham == id);
            ViewBag.idSanPham = sanPham.idSanPham;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("SanPham");
        }

        //CHỈNH SỬA SẢN PHẨM
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            //LẤY RA ĐỐI TƯỢNG SẢN PHẨM THEO MÃ
            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.idSanPham == id);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            return View(sanPham);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            var sanpham = db.SanPhams.FirstOrDefault(n => n.idSanPham == id);
            sanpham.idSanPham = id;
            //Dua du lieu vao dropdownlist
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            //kiem tra duong dan file
            if (fileUpload == null)
            {
                sanpham.hinhAnh = sanpham.hinhAnh;
            }
            //Them vao CSDL
            else
            {
                //luu ten file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan cua file
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                //kiem tra hinh anh ton tai chua
                if (System.IO.File.Exists(path))
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                else
                {
                    // luu hinh anh vao duong dan
                    fileUpload.SaveAs(path);
                }
                sanpham.hinhAnh = fileName;
            }
            sanpham.idSanPham = id;
            //luu vao CSDL
            db.Configuration.ValidateOnSaveEnabled = false;
            UpdateModel(sanpham);
            db.SaveChanges();
            return this.SuaSanPham(id);
        }
        //Thêm Danh mục
        public ActionResult ThemDanhMuc(DanhMucSP danhMuc)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu"); 
            //luu vao CSDL
            db.DanhMucSPs.Add(danhMuc);
            db.SaveChanges();       
            return RedirectToAction("ThemSanPham");
        }
        //THÊM MỚI THỂ LOẠI SẢN PHẨM
        [HttpGet]
        public ActionResult ThemTheLoai()
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            return View();
        }
        [HttpPost]
        public ActionResult ThemTheLoai(TheLoaiSP theLoai)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            //luu vao CSDL
            db.TheLoaiSPs.Add(theLoai);
            db.SaveChanges();
            return RedirectToAction("ThemSanPham");
        }

        //Thêm Thương hiệu
        public ActionResult ThemThuongHieu(ThuongHieu thuongHieu)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            //luu vao CSDL
            db.ThuongHieux.Add(thuongHieu);
            db.SaveChanges();
            return RedirectToAction("ThemSanPham");
        }
    }
}