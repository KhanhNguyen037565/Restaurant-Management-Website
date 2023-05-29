
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Md5;
using WebsiteNhaHang.Models;
namespace WebsiteNhaHang.Controllers
{
    public class TaiKhoanController : Controller
    {
        NhaHangEntities db=new NhaHangEntities();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(TaiKhoan tKhoan,HttpPostedFileBase file)
        {
            var filename="";
            if (file != null)
            {
                filename = Path.GetFileName(file.FileName);  
            }
                      
            if (ModelState.IsValid)
            {
                if (filename != "")
                {
                    var path = Path.Combine(Server.MapPath("~/Content/img/TaiKhoan/"), filename);

                    while(System.IO.File.Exists(path))
                    {
                        filename = "1"+filename;                        
                    }
                    path = Path.Combine(Server.MapPath("~/Content/img/TaiKhoan/"), filename);
                    file.SaveAs(path);
                }
                if (KiemTra(tKhoan.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!!");
                }
                else
                {   
                    tKhoan.LoaiTaiKhoan = 2;
                    tKhoan.NhoMatKhau = false;
                    tKhoan.HinhAnh =filename;
                    tKhoan.MatKhau = Encryptor.MD5Hash(tKhoan.MatKhau);
                    tKhoan.XacNhanMatKhau = Encryptor.MD5Hash(tKhoan.XacNhanMatKhau);
                    if (tKhoan.MatKhau==tKhoan.XacNhanMatKhau)
                    {
                        
                        db.TaiKhoan.Add(tKhoan);
                        db.SaveChanges();
                        ViewBag.ThongBao = "Đăng ký tài khoản thành công!!!";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu xác nhận không đúng");
                        
                    }
                }
            }
            return View();
            //return RedirectToAction("Index", "TrangChu");
        }

        public bool KiemTra(string email)
        {
            return db.TaiKhoan.Count(n => n.Email == email) > 0;
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string email,string matkhau)
        {
            if (ModelState.IsValid)
            {
                matkhau = Encryptor.MD5Hash(matkhau);
                var v = db.TaiKhoan.FirstOrDefault(x=>x.Email == email && x.MatKhau ==matkhau );
                if (v != null)
                {
                    Session["MaTaiKhoan"] = v.MaTaiKhoan;
                    Session["EmailDangNhap"] = v.Email;
                    Session["LoaiTK"] = v.LoaiTaiKhoan;
                    return Redirect("/");
                }
                ViewBag.Message = "Nhập sai mật khẩu hoặc email!!";
            }
            
            return View();           
        }

        public ActionResult SauDangNhap()
        {
            if (Session["EmailDangNhap"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "TrangChu");
            }
            
        }

        public RedirectResult DangXuat()
        {
            //Session["EmailDangNhap"] = null;
            //Session["MatKhauDangNhap"] = null;
            Session.Remove("EmailDangNhap");
            Session.Remove("MaTaiKhoan");
            Session.Remove("LoaiTK");
            return Redirect("/");
        }

        public ActionResult TaiKhoanChiTiet()
        {
            int a = Convert.ToInt32(Session["MaTaiKhoan"]);
            var tk = db.TaiKhoan.FirstOrDefault(n => n.MaTaiKhoan == a );
            if (tk == null)
            {
                return RedirectToAction("DangNhap","TaiKhoan");
            }
            return View(tk);
        }

        public ActionResult SuaTaiKhoan([Bind(Include = "MaTaiKhoan,Email,MatKhau,XacNhanMatKhau,NhoMatKhau,LoaiTaiKhoan,TenNguoiDung,HinhAnh,DiaChi,SoDienThoai")] TaiKhoan taiKhoan, HttpPostedFileBase HinhAnh, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/TaiKhoan/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        taiKhoan.HinhAnh = _FileName;
                        // get Path of old image for deleting it
                        _path = Path.Combine(Server.MapPath("~/Content/img/TaiKhoan/"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                        taiKhoan.HinhAnh = form["oldimage"];
                    
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

                return RedirectToAction("TaiKhoanChiTiet");
            }
            return View(taiKhoan);
        }


    }
}