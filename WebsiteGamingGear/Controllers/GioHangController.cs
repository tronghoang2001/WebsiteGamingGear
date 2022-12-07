using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteGamingGear.Others;
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
            foreach (var item in lstGioHang)
            {
                var sp = data.SanPhams.SingleOrDefault(p => p.idSanPham == item.iIdSanPham);
                if (item.iSoLuong > sp.soLuong)
                {
                    ViewBag.Thongbao ="Sản phẩm bạn mua không còn đủ số lượng bạn yêu cầu. Sản phẩm " + sp.tenSanPham + " chỉ còn số lượng là " + sp.soLuong + " !";
                }              
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
            ddh.ngayDat = DateTime.Now;
            ddh.tenNguoiNhan = tennguoinhan;
            ddh.diaChiNguoiNhan = diachinguoinhan;
            ddh.sdtNguoiNhan = sđtnguoinhan;
            ddh.ghiChu = ghichudonhang;
            ddh.trangThai = "1";
            ddh.trangThaiThanhToan = "1";
            ddh.tongTien = TongTien();
            data.DonDatHangs.Add(ddh);
            data.SaveChanges();
            //thêm chi tiết đơn hàng
            foreach (var item in gh)
            {             
                var sp = data.SanPhams.SingleOrDefault(p => p.idSanPham == item.iIdSanPham);
                ChiTietDDH ct = new ChiTietDDH();
                if (item.iSoLuong > sp.soLuong)
                {
                    return RedirectToAction("GioHang");
                }
                else
                {
                    ct.idDDH = ddh.idDDH;
                    ct.idSanPham = item.iIdSanPham;
                    ct.soLuong = item.iSoLuong;
                    ct.gia = item.iDonGia;
                    sp.soLuong = sp.soLuong - ct.soLuong;
                    data.ChiTietDDHs.Add(ct);
                    UpdateModel(sp);
                    data.SaveChanges();
                    return RedirectToAction("XacNhanDonHang", "GioHang");
                }             
            }
            return View();
        }
        public ActionResult XacNhanDonHang()
        {
            Session["Giohang"] = null;
            return View();
        }
        public ActionResult ThanhToan()
        {
            List<GioHang> gh = LayGioHang();
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];
            string totals = (TongTien() * 100).ToString(); //total là tổng của session giỏ hàng
            foreach (var item in gh)
            {
                var sp = data.SanPhams.SingleOrDefault(p => p.idSanPham == item.iIdSanPham);
                if (item.iSoLuong > sp.soLuong)
                {
                    return RedirectToAction("GioHang");
                }
            }
            XuLy pay = new XuLy();

            //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Version", "2.1.0");

            //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_Command", "pay");

            //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_TmnCode", tmnCode);

            //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            //totals là value đã ép kiểu sang kiểu chuỗi ở phía trên
            pay.AddRequestData("vnp_Amount", totals);

            //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_BankCode", "");

            //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_CurrCode", "VND");

            //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_IpAddr", ChuyenDoi.GetIpAddress());

            //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_Locale", "vn");

            //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng");

            //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_OrderType", "other");

            //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);

            //mã hóa đơn
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString());

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            return Redirect(paymentUrl);

        }
        public ActionResult XacNhanThanhToan()
        {
            DonDatHang ddh = new DonDatHang();
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                XuLy pay = new XuLy();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //mã hóa đơn
                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef"));

                //mã giao dịch tại hệ thống VNPAY
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo"));

                //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode");
                //hash của dữ liệu trả về
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                //check chữ ký đúng hay không?
                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret);

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                        ddh.trangThaiThanhToan = "2";
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                        ddh.trangThaiThanhToan = "1";
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                    ddh.trangThaiThanhToan = "1";
                }
                ddh.idTaiKhoan = tk.id;
                ddh.tenNguoiNhan = tk.ten;
                ddh.diaChiNguoiNhan = tk.diaChi;
                ddh.sdtNguoiNhan = tk.soDienThoai;
                ddh.ngayDat = DateTime.Now;
                ddh.trangThai = "1";
                ddh.tongTien = TongTien();
                data.DonDatHangs.Add(ddh);
                data.SaveChanges();
                //thêm chi tiết đơn hàng
                foreach (var item in gh)
                {
                    var sp = data.SanPhams.SingleOrDefault(p => p.idSanPham == item.iIdSanPham);
                    ChiTietDDH ct = new ChiTietDDH();
                    ct.idDDH = ddh.idDDH;
                    ct.idSanPham = item.iIdSanPham;
                    ct.soLuong = item.iSoLuong;
                    ct.gia = item.iDonGia;
                    sp.soLuong = sp.soLuong - ct.soLuong;
                    data.ChiTietDDHs.Add(ct);
                    UpdateModel(sp);
                    data.SaveChanges();
                }
            }
            Session["Giohang"] = null;
            return View();
        }
    }
}