using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using GamingGear.Models.Account;
using GamingGear.Others;
using WebsiteGamingGear.Models;
using System.Web;
using System.IO;

namespace GamingGear.Controllers
{
    public class AccountController : Controller
    {
        //Tao doi tuong chua toan bo CSDL tu dbGamingGear
        dbGamingGear db = new dbGamingGear();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //View Đăng nhập
        public ActionResult DangNhap(string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null && Request.UrlReferrer.ToString().Length > 0)
            {
                return RedirectToAction("DangNhap", new { returnUrl = Request.UrlReferrer.ToString() });
            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("TrangChu", "NguoiDung");
            }
            return View();
        }
        // POST: /Account/ExternalLogin

        //Code xử lý đăng nhập
        [HttpPost]
        //script xử lý SignIn | path: Scripts/my_js/checkuseraccount.js"
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(DangNhap model, string returnUrl)
        {
            //Mã hóa password dùng sha 256
            model.matKhau = Crypto.Hash(model.matKhau);
            //nếu trùng username,password và status ="1 tức là đang hoạt dộng và 2 là vô hiệu hóa và 0 là chưa kích hoạt" thì đăng nhập thành công
            var kiemTraHoatDong = db.TaiKhoans.Where(m => m.trangThai == "1" && m.taiKhoan1.ToLower() == model.taiKhoan && m.matKhau == model.matKhau).SingleOrDefault();
            var kiemTraVoHieuHoa = db.TaiKhoans.Where(m => m.trangThai == "2" && m.taiKhoan1.ToLower() == model.taiKhoan && m.matKhau == model.matKhau).SingleOrDefault();
            var kiemTraKichHoat = db.TaiKhoans.Where(m => m.trangThai == "0" && m.taiKhoan1.ToLower() == model.taiKhoan && m.matKhau == model.matKhau).SingleOrDefault();

            if (kiemTraVoHieuHoa != null)
            {
                ViewBag.Thongbao1 = "Tài khoản đã bị vô hiệu hóa!";
            }
            else
            if (kiemTraKichHoat != null)//kiểm tra tài khoản đã kích hoạt hay chưa
            {
                TempData["AccountID"] = kiemTraKichHoat.id;
                TempData["EmailID"] = kiemTraKichHoat.email;
                String maKichHoat = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(kiemTraKichHoat.email, maKichHoat, "XacThucTaiKhoan");
                return RedirectToAction("XacThucEmail", "Account");
            }
            else if (kiemTraHoatDong != null)//check đúng tài khoản và mật khẩu chưa
            {          
                ViewBag.Thongbao2 = "Đăng nhập thành công!";
                Session["TaiKhoan"] = kiemTraHoatDong;

                return RedirectToAction("TrangChu", "NguoiDung");
            }
            else//trường hợp cuối thì cho nó fase sai tài khoản hoặc mật khẩu
            {
                ViewBag.Thongbao3 = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }
            return View(model);
        }
        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            Session.Clear();
            return RedirectToAction("TrangChu", "NguoiDung");
        }

        //View Đăng ký
        public ActionResult DangKy()
        {
            //Nếu đã đăng nhập thì không vô được trang đăng ký
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("TrangChu", "NguoiDung");
            }
            return View();
        }
        //Code Xử lý đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Exclude = "trangThai,requestcode")]  TaiKhoan tk,  DangKy dangKy)
        {
            string fail = "";
            string success = "";
            //check tk đã có trong hệ database chưa
            var checkTaiKhoan = db.TaiKhoans.Any(m => m.taiKhoan1 == dangKy.taiKhoan);
            //check email đã có trong hệ database chưa
            var checkEmail = db.TaiKhoans.Any(m => m.email == dangKy.Email);
            //check số điện thoại đã có trong hệ database chưa
            var checkSdt = db.TaiKhoans.Any(m => m.soDienThoai == dangKy.soDienThoai);
            if (checkTaiKhoan)
            {
                fail = "Tên tài khoản đã được sử dụng";
            }
            if (checkEmail)
            {
                fail = "Email đã được sử dụng";
            }
            if (checkSdt)
            {
                fail = "SĐT đã được sử dụng";
            }
            if (dangKy.xacNhanMatKhau != dangKy.matKhau)
            {
                fail = "Xác nhận mật khẩu không trùng với mật khẩu";
            }
            else
            {
                tk.taiKhoan1 = dangKy.taiKhoan;
                tk.quyen = 1;         
                tk.email = dangKy.Email;
                tk.ten = dangKy.ten;
                tk.gioiTinh = dangKy.gioiTinh;
                tk.anhDaiDien = "/Images/svg/avatars/001-boy.svg";
                tk.ngaySinh = dangKy.ngaySinh;
                tk.soDienThoai = dangKy.soDienThoai;
                tk.diaChi = dangKy.diaChi;
                //tạo chuỗi code kích hoạt tài khoản
                tk.trangThai = "0";
                dangKy.Requestcode = Guid.NewGuid().ToString();
                tk.requestCode = dangKy.Requestcode;
                //Gửi request code đến email bạn đăng ký tài khoản, nếu không muốn gửi request code thì chuyển status ="1", comment  model.Requestcode = Guid.NewGuid().ToString(); và SendVerificationLinkEmail(model.Email, model.Requestcode, "VerifyAccount");
                SendVerificationLinkEmail(dangKy.Email, tk.requestCode, "XacThucTaiKhoan");
                //do password có nhiều ràng buộc "validdation nên phải thêm" không thêm sẽ báo lõi "Validation failed for one or more entities" 
                db.Configuration.ValidateOnSaveEnabled = false;
                //hash password và không cho khoảng trắng
                tk.matKhau = Crypto.Hash(dangKy.matKhau.Trim());
                db.TaiKhoans.Add(tk);
                //add dữ liệu vào database
                db.SaveChanges();
                TempData["AccountID"] = tk.id;//truyền sang EmailverificationRequired
                TempData["EmailID"] = tk.email;//truyền sang EmailverificationRequired
                return RedirectToAction("XacThucEmail", "Account");
            }
            ViewBag.Success = success;
            ViewBag.Fail = fail;
            return View(dangKy);
        }
        //Gửi email xác thựctrang này chỉ tồn tại nếu bạn vừa dk xong, nếu bạn refesh lại trang thì trang này sẽ không hiện nữa
        //nếu bạn muuốn xác thực lại tk thì có thể dùng cách quên mật khẩu lúc đó tài khoản của bạn sẽ được kích hoạt khi bạn thay đổi mk thành công

        [HttpGet]
        public ActionResult XacThucTaiKhoan(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                bool activate = false;
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    var verify = db.TaiKhoans.Where(a => a.requestCode == new Guid(id).ToString()).FirstOrDefault();
                    if (verify != null)
                    {
                        
                        verify.trangThai = "1";
                        verify.requestCode = "";
                        db.SaveChanges();
                        activate = true;
                    }
                    else
                    {
                        ViewBag.Message = "Yêu cầu không hợp lệ";
                    }
                }
                ViewBag.Status = activate;
            }
            return View();
        }
        public ActionResult XacThucEmail()
        {
            ViewBag.AccountID = TempData["AccountID"];
            ViewBag.Email = TempData["EmailID"];
            if (ViewBag.AccountID == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                return View();
            }
        }
        //Gửi lại email xác thực
        public ActionResult ReSendVerification(TaiKhoan taiKhoan)
        {
            var account = db.TaiKhoans.FirstOrDefault(m => m.id == taiKhoan.id);
            if (account != null)
            {
                string maXacThuc = Guid.NewGuid().ToString();
                string EmailID = account.email;
                SendVerificationLinkEmail(EmailID, maXacThuc, "XacThucTaiKhoan");
                account.email = account.email;
                account.requestCode = maXacThuc;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        //View quên mật khẩu
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        //Code xử lý quên mật khẩu
        [HttpPost]
        public ActionResult QuenMatKhau(string EmailID)
        {
            string fail = "";
            string success = "";
            var account = db.TaiKhoans.Where(m => m.email == EmailID && m.trangThai != "2").FirstOrDefault(); // kiểm tra email đã trùng với email đăng ký tài khoản chưa, nếu chưa đăng ký sẽ trả về fail
            if (account != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                //Gửi code reset đến mail đã nhập ở form quên mật khẩu 
                SendVerificationLinkEmail(account.email, resetCode, "KhoiPhucMatKhau");
                string sendmail = account.email;
                account.requestCode = resetCode; //request code phải giống reset code
                //account.expired_at = DateTime.Now.AddMinutes(10);//email quên mật khẩu hết hạn sau 10p
                //db.Configuration.ValidateOnSaveEnabled = false;//khi chạy action "ForgotPassword" và bị báo lỗi "Validation failed for one or more entities. See 'EntityValidationErrors'" thì thêm dòng này vô. Vì có quá nhiều validation trong một funtion nên báo lỗi.
                db.SaveChanges();
                success = "Đường dẫn reset Mật khẩu đã được gửi đến " + EmailID + " vui lòng kiểm tra Email !";
            }
            else
            {
                fail = "Email chưa tồn tại hoặc tài khoản đã bị vô hiệu hóa !"; // tài khoản không có trong hệ thống sẽ báo fail
            }

            ViewBag.ThongBao1 = success;//truyền viewbag qua view của "ForgotPassword"
            ViewBag.ThongBao2 = fail;//truyền viewbag qua view của "ForgotPassword"
            return View();
        }
        //View ResetPassword
        public ActionResult KhoiPhucMatKhau(string id)
        {
            var user = db.TaiKhoans.Where(a => a.requestCode == id).FirstOrDefault();
            if (user != null)
            {
                KhoiPhucMatKhau model = new KhoiPhucMatKhau();
                model.resetCode = id;
                return View(model);
            }
            else
            {
                return RedirectToAction("TrangChu", "NguoiDung");
            }
        }
        //Code xử lý resetpassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KhoiPhucMatKhau(KhoiPhucMatKhau model)
        {
            
                var user = db.TaiKhoans.Where(m => m.requestCode == model.resetCode).FirstOrDefault();
                if (user != null) //&& user.expired_at > DateTime.Now)
                {
                    if(model.matKhauMoi != model.xacNhanMatKhau)
                    {
                        ViewBag.ThongBao = "Xác nhận mật khẩu không trùng với mật khẩu !";
                    }
                    else
                    {
                        user.matKhau = Crypto.Hash(model.matKhauMoi);
                        //sau khi đổi mật khẩu thành công khi quay lại link cũ thì sẽ không đôi được mật khẩu nữa 
                        user.requestCode = "";
                        user.trangThai = "1";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        ViewBag.ThongBao = "Cập nhật mật khẩu thành công !";
                        return RedirectToAction("DangNhap");
                    }                  
                }
            
           
            return View(model);
        }
        //Gửi Email xác nhận đăng ký, quên mật khẩu
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string maKichHoat, string emailLink)
        {
            // đường dẫn mail gồm có controller "Account"  +"emailfor" +  "code reset đã được mã hóa(mội lần gửi email quên mật khẩu sẽ random 1 code reset mới"
            var verifyUrl = "/Account/" + emailLink + "/" + maKichHoat;
            ///để dùng google email gửi email reset cho người khác bạn cần phải vô đây "https://www.google.com/settings/security/lesssecureapps" Cho phép ứng dụng kém an toàn: Bật
            var fromEmail = new MailAddress(SendEmail.UserEmail, SendEmail.Name); // "username email-vd: vn123@gmail.com" ,"tên hiển thị mail khi gửi"
            var toEmail = new MailAddress(emailID);
            //nhập password của bạn
            var fromEmailPassword = SendEmail.Password;
            string subject = "";
            //dùng body mail html , file template nằm trong thư mục "EmailTemplate/Text.cshtml"
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "MailConfirm" + ".cshtml");
            if (emailLink == "XacThucTaiKhoan")
            {
                subject = "Xác thực tài khoản " + emailID;
                body = body.Replace("{{ViewBag.Sendmail}}", "Xác thực tài khoản");
                body = body.Replace("{{ViewBag.confirmtext}}", "Kích hoạt tài khoản");
                body = body.Replace("{{ViewBag.bodytext}}", "Email này có hiệu lực trong vòng <span style='font-weight:600;'>10 phút</span>, Vui lòng nhấn vào nút bên dưới để xác thực hoàn tất đăng ký cho tài khoản của bạn");
                body = body.Replace("{{viewBag.Confirmlink}}", Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl));//hiển thị nội dung lên form html
            }
            else if (emailLink == "KhoiPhucMatKhau")
            {
                subject = "Khôi phục mật khẩu cho " + emailID;
                body = body.Replace("{{ViewBag.Sendmail}}", "Khôi phục mật khẩu");
                body = body.Replace("{{ViewBag.confirmtext}}", "Thiết lập mật khẩu mới");
                body = body.Replace("{{ViewBag.bodytext}}", "Email này có hiệu lực trong vòng <span style='font-weight:600;'>10 phút</span>, Vui lòng nhấn vào nút bên dưới để đặt lại mật khẩu tài khoản của bạn. Nếu bạn không yêu cầu đặt lại mật khẩu mới, vui lòng bỏ qua email này");
                body = body.Replace("{{viewBag.Confirmlink}}", Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl));//hiển thị nội dung lên form html
            }
            var smtp = new SmtpClient
            {
                Host = SendEmail.Host, //tên mấy chủ nếu bạn dùng gmail thì đổi  "Host = "smtp.gmail.com"
                Port = 587,
                EnableSsl = true, //bật ssl
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        //View đổi mật khẩu
        public ActionResult DoiMatKhau()
        {   
            if (Session["TaiKhoan"] != null || Session["TaiKhoan"].ToString() != "")
            {
                TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
                var idtaikhoan = tk.id;
                var account = db.TaiKhoans.Where(m => m.id == idtaikhoan).FirstOrDefault();
                ViewBag.Avatar = account.anhDaiDien;
                ViewBag.AccountName = account.ten;
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "Account");
            }
        }
        //Thay đổi mật khẩu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(DoiMatKhau model)
        {
            TaiKhoan tk = (TaiKhoan)Session["TaiKhoan"];
            var idtaikhoan = tk.id;
            string matkhaucu = Crypto.Hash(model.matKhauCu);
            string matkhaumoi = Crypto.Hash(model.matKhauMoi);
            var account = db.TaiKhoans.Where(m => m.id == idtaikhoan).FirstOrDefault();
            var checkpassword = db.TaiKhoans.Any(m => m.matKhau == matkhaucu && m.id == idtaikhoan);
            if (checkpassword)
            {
                if (matkhaumoi == account.matKhau)
                {
                    ViewBag.ThongBao1 = "Mật khẩu mới không được trùng với mật khẩu cũ !";
                }
                else
                {
                    if(model.xacNhanMatKhau != model.matKhauMoi)
                    {
                        ViewBag.ThongBao5 = "Xác nhận mật khẩu không trùng khớp !";
                    }
                    else
                    {
                        if (account != null)
                        {
                            db.Configuration.ValidateOnSaveEnabled = false;
                            account.matKhau = matkhaumoi;
                            db.SaveChanges();
                            ViewBag.ThongBao2 = "Cập nhật mật khẩu thành công !";
                            DangXuat();
                            return RedirectToAction("DangNhap", "Account");
                        }
                        else
                        {
                            ViewBag.ThongBao3 = "Lỗi !";
                        }
                    }                   
                }

            }
            else
            {
                ViewBag.ThongBao4 = "Sai mật khẩu cũ !";
            }
            return View();
        }

        //Sửa thông tin tài khoản
        //GET
        public ActionResult SuaThongTin(int id)
        {
            var khachhang = db.TaiKhoans.FirstOrDefault(n => n.id == id);
            return View(khachhang);
        }
        //POST
        [HttpPost]
        public ActionResult SuaThongTin(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            //Tạo 1 biến khachhang với đối tương id = id truyền vào
            var taikhoan = db.TaiKhoans.First(n => n.id == id);
            var hoten = collection["hoTenKH"];
            var sdt = collection["sdtKH"];
            var email = collection["Email"];
            var ngaySinh = String.Format("{0:MM/dd/yyyy}",collection["ngaySinh"]);
            taikhoan.id = id;
            //Nếu người dùng không nhập đủ thông tin            
            if (string.IsNullOrEmpty(ngaySinh))
            {
                ViewData["Loi4"] = "Chưa nhập ngày sinh!";
            }
            //kiem tra duong dan file
            if (fileUpload == null)
            {
                taikhoan.anhDaiDien = taikhoan.anhDaiDien;
            }
            else
            {
                //luu ten file
                var fileName = Path.GetFileName(fileUpload.FileName);
                taikhoan.anhDaiDien = fileName;
                //luu duong dan cua file
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                //kiem tra hinh anh ton tai chua
                if (System.IO.File.Exists(path))
                    ViewBag["Loi5"] = "Hình ảnh đã tồn tại";
                else
                {
                    // luu hinh anh vao duong dan
                    fileUpload.SaveAs(path);
                }
                taikhoan.ngaySinh = DateTime.Parse(ngaySinh);
            }
            taikhoan.ten = hoten;
            taikhoan.soDienThoai = sdt;
            taikhoan.email = email;
            //Update trong CSDL
            //check nhiều validation thì phải cho nó false nếu không sẽ bị lỗi khi chạy đến đây
            db.Configuration.ValidateOnSaveEnabled = false;
            TryUpdateModel(taikhoan);
            db.SaveChanges();
            return this.SuaThongTin(id);
        }
    }
}