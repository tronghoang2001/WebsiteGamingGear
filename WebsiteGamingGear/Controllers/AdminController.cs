using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult TrangChu(int? page)
        {
            ViewBag.TongDonHang = db.DonDatHangs.Select(a => a.idDDH).Count();
            ViewBag.TongDoanhThu = db.DonDatHangs.Where(a => a.trangThaiThanhToan == "2").Select(a => a.tongTien).Sum();
            ViewBag.LuotMua = db.SanPhams.Select(a => a.luotMua).Sum();
            ViewBag.LuotXem = db.SanPhams.Select(a => a.luotXem).Sum();
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.DonDatHangs.ToList().OrderBy(n => n.idDDH).ToPagedList(pageNumber, pageSize));
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
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            //LẤY RA ĐỐI TƯỢNG SẢN PHẨM THEO MÃ
            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.idSanPham == id);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }          
            return View(sanPham);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(int id, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownlist
            ViewBag.idDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucSP", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiSPs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiSP", "tenTheLoai");
            ViewBag.idThuongHieu = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.tenThuongHieu), "idThuongHieu", "tenThuongHieu");
            var sanpham = db.SanPhams.FirstOrDefault(n => n.idSanPham == id);
            sanpham.idSanPham = id;           
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

        //Tin tuc
        public ActionResult TinTuc(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.TinTucs.ToList().OrderBy(n => n.idTinTuc).ToPagedList(pageNumber, pageSize));
        }
        //THÊM MOI TIN TUC
        [HttpGet]
        public ActionResult ThemTinTuc()
        {

            //dua du lieu vao dropdownlist
            //lay ds tu table danh muc, sap xep tang dan theo ten danh muc, chon lay gia tri ma DM, hien thi ten DM
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTinTuc(TinTuc tinTuc, HttpPostedFileBase fileUpload)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");

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
                    tinTuc.hinhAnh1 = fileName;
                    //luu vao CSDL
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TinTucs.Add(tinTuc);
                    db.SaveChanges();
                    return RedirectToAction("TinTuc");
                }
            }
        }
        //XÓA SẢN PHẨM
        [HttpGet]
        public ActionResult XoaTinTuc(int id)
        {
            TinTuc tinTuc = db.TinTucs.SingleOrDefault(n => n.idTinTuc == id);
            ViewBag.idTinTuc = tinTuc.idTinTuc;
            if (tinTuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tinTuc);

        }
        [HttpPost, ActionName("XoaTinTuc")]
        public ActionResult XacNhanXoaTT(int id)
        {
            //LẤY RA ĐỐI TƯỢNG TIN TUC THEO MÃ
            TinTuc tinTuc = db.TinTucs.SingleOrDefault(n => n.idTinTuc == id);
            ViewBag.idTinTuc = tinTuc.idTinTuc;
            if (tinTuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.TinTucs.Remove(tinTuc);
            db.SaveChanges();
            return RedirectToAction("TinTuc");
        }

        //CHỈNH SỬA TIN TUC
        [HttpGet]
        public ActionResult SuaTinTuc(int id)
        {
            //LẤY RA ĐỐI TƯỢNG TIN TUC THEO MÃ
            var tinTuc = db.TinTucs.FirstOrDefault(n => n.idTinTuc == id);
            if (tinTuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");
            return View(tinTuc);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTinTuc(int id, HttpPostedFileBase fileUpload)
        {           
            //Dua du lieu vao dropdownlist
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");
            var tintuc = db.TinTucs.FirstOrDefault(n => n.idTinTuc == id);
            tintuc.idTinTuc = id;
            //Nếu người dùng không nhập đủ thông tin            
            //kiem tra duong dan file
            if (fileUpload == null)
            {
                tintuc.hinhAnh1 = tintuc.hinhAnh1;
            }
            else
            {
                //luu ten file
                var fileName = Path.GetFileName(fileUpload.FileName);
                tintuc.hinhAnh1 = fileName;
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
            }
            //Update trong CSDL
            //check nhiều validation thì phải cho nó false nếu không sẽ bị lỗi khi chạy đến đây
            db.Configuration.ValidateOnSaveEnabled = false;
            TryUpdateModel(tintuc);
            db.SaveChanges();
            return this.SuaTinTuc(id);
        }
        //Thêm Danh mục
        public ActionResult ThemDanhMucTT(DanhMucTinTuc danhMuc)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");
            //luu vao CSDL
            db.DanhMucTinTucs.Add(danhMuc);
            db.SaveChanges();
            return RedirectToAction("ThemTinTuc");
        }
        //THÊM MỚI THỂ LOẠI SẢN PHẨM
        [HttpGet]
        public ActionResult ThemTheLoaiTT()
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");
            return View();
        }
        [HttpPost]
        public ActionResult ThemTheLoaiTT(TheLoaiTinTuc theLoai)
        {
            ViewBag.idDanhMuc = new SelectList(db.DanhMucTinTucs.ToList().OrderBy(n => n.tenDanhMuc), "idDanhMucTT", "tenDanhMuc");
            ViewBag.idTheLoai = new SelectList(db.TheLoaiTinTucs.ToList().OrderBy(n => n.tenTheLoai), "idTheLoaiTT", "tenTheLoai");
            //luu vao CSDL
            db.TheLoaiTinTucs.Add(theLoai);
            db.SaveChanges();
            return RedirectToAction("ThemTinTuc");
        }

        //Chuyển trạng thái chờ 
        public ActionResult TrangThaiCho(int id)
        {       
                    var donhang = db.DonDatHangs.SingleOrDefault(n => n.idDDH == id);
                    if (donhang != null && donhang.trangThai != "3")
                    {
                        donhang.trangThai = "1";
                        db.Entry(donhang).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("TrangChu");          
        }
        //Chuyển trạng thái sang đang xử lý (đang được giao)
        public ActionResult TrangThaiDangGiao(int id)
        {
                    var donhang = db.DonDatHangs.SingleOrDefault(n => n.idDDH == id);
                    if (donhang != null)
                    {
                        donhang.trangThai = "2";
                        db.Entry(donhang).State = EntityState.Modified;
                    }                 
                    db.SaveChanges();
                    return RedirectToAction("TrangChu");

        }
        //Chuyển trạng thái đơn hàng sang hoàn thành
        public ActionResult TrangThaiHoanThanh(int id)
        {           
                    var donhang = db.DonDatHangs.SingleOrDefault(n => n.idDDH == id);
                    if (donhang != null)
                    {
                        donhang.trangThai = "3";
                        donhang.trangThaiThanhToan = "2";
                        db.Entry(donhang).State = EntityState.Modified;
                    }                  
                    db.SaveChanges();
                    return RedirectToAction("TrangChu");
        }
        public ActionResult LienHe(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.LienHes.ToList().OrderBy(n => n.idLienHe).ToPagedList(pageNumber, pageSize));
        }
        //Phản hồi liên hệ
        [HttpGet]
        public ActionResult PhanHoiLienHe(int id)
        {
            //LẤY RA LIEN HE THEO MÃ
            var lienhe = db.LienHes.FirstOrDefault(n => n.idLienHe == id);
            if (lienhe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lienhe);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PhanHoiLienHe(int id, HttpPostedFileBase fileUpload)
        {
            var lienhe = db.LienHes.FirstOrDefault(n => n.idLienHe == id);
            lienhe.idLienHe = id;
            lienhe.trangThai = "2";
            //Update trong CSDL
            UpdateModel(lienhe);
            db.SaveChanges();
            return this.PhanHoiLienHe(id);
        }
        public ActionResult BinhLuanTinTuc(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.BinhLuanTinTucs.ToList().OrderBy(n => n.idBinhLuan).ToPagedList(pageNumber, pageSize));
        }
        //Chuyển trạng thái chưa duyệt
        public ActionResult BinhLuanThuHoi(int id)
        {
            var binhluan = db.BinhLuanTinTucs.SingleOrDefault(n => n.idBinhLuan == id);
            if (binhluan != null)
            {
                binhluan.trangThai = "1";
                db.Entry(binhluan).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("BinhLuanTinTuc");
        }
        //Chuyển trạng thái đã duyệt
        public ActionResult BinhLuanDaDuyet(int id)
        {
            var binhluan = db.BinhLuanTinTucs.SingleOrDefault(n => n.idBinhLuan == id);
            if (binhluan != null)
            {
                binhluan.trangThai = "2";
                db.Entry(binhluan).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("BinhLuanTinTuc");

        }
        //Bình luận SẢN PHẨM
        public ActionResult BinhLuanSanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.DanhGias.ToList().OrderBy(n => n.idDanhGia).ToPagedList(pageNumber, pageSize));
        }
        //Chuyển trạng thái chưa duyệt
        public ActionResult BinhLuanSPThuHoi(int id)
        {
            var binhluan = db.DanhGias.SingleOrDefault(n => n.idDanhGia == id);
            if (binhluan != null)
            {
                binhluan.trangThai = "1";
                db.Entry(binhluan).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("BinhLuanSanPham");
        }
        //Chuyển trạng thái đã duyệt
        public ActionResult BinhLuanSPDaDuyet(int id)
        {
            var binhluan = db.DanhGias.SingleOrDefault(n => n.idDanhGia == id);
            if (binhluan != null)
            {
                binhluan.trangThai = "2";
                db.Entry(binhluan).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("BinhLuanSanPham");
        }
        //TÀI KHOẢN
        public ActionResult TaiKhoan(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.TaiKhoans.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        //Quyền khách
        public ActionResult QuyenKhach(int id)
        {
            var taikhoan = db.TaiKhoans.SingleOrDefault(n => n.id == id);
            if (taikhoan != null)
            {
                taikhoan.quyen = 1;
                db.Entry(taikhoan).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            return RedirectToAction("TaiKhoan");
        }
        //Quyền nhân viên
        public ActionResult QuyenNhanVien(int id)
        {
            var taikhoan = db.TaiKhoans.SingleOrDefault(n => n.id == id);
            if (taikhoan != null)
            {
                taikhoan.quyen = 2;
                db.Entry(taikhoan).State = EntityState.Modified;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return RedirectToAction("TaiKhoan");

        }
        //Trạng thái hoạt động
        public ActionResult HoatDong(int id)
        {
            var taikhoan = db.TaiKhoans.SingleOrDefault(n => n.id == id);
            if (taikhoan != null)
            {
                taikhoan.trangThai = "1";
                db.Entry(taikhoan).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            return RedirectToAction("TaiKhoan");
        }
        //Trạng thái vô hiệu hóa
        public ActionResult VoHieuHoa(int id)
        {
            var taikhoan = db.TaiKhoans.SingleOrDefault(n => n.id == id);
            if (taikhoan != null)
            {
                taikhoan.trangThai = "2";
                db.Entry(taikhoan).State = EntityState.Modified;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return RedirectToAction("TaiKhoan");

        }
    }
}