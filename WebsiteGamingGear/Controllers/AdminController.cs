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
        //THÊM MỚI LOẠI SẢN PHẨM
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
    }
}