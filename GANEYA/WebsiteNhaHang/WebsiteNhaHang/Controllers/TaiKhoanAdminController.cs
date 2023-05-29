using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Net;
using WebsiteNhaHang.Md5;
using System.IO;

namespace WebsiteNhaHang.Controllers
{
    public class TaiKhoanAdminController : Controller
    {
        NhaHangEntities db = new NhaHangEntities();
        // GET: TaiKhoanAdmin
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string email, string matkhau)
        {
            if (ModelState.IsValid)
            {
                matkhau = Encryptor.MD5Hash(matkhau);
                var v = db.TaiKhoan.FirstOrDefault(x => x.Email == email && x.MatKhau == matkhau);
                if (v != null&&v.LoaiTaiKhoan!=2)
                {
                    Session["EmailDangNhap"] = v.Email;
                    Session["MaTaiKhoan"] = v.MaTaiKhoan;
                    Session["LoaiTK"] = v.LoaiTaiKhoan;
                    return Redirect("/TaiKhoanAdmin/TaiKhoanChiTiet");
                }
                ModelState.AddModelError("", "Email hoặc mật khẩu sai!!");
            }

            return View();
        }
        public RedirectResult DangXuat()
        {
            Session.Remove("EmailDangNhap");
            Session.Remove("MaTaiKhoan");
            Session.Remove("LoaiTK");

            return Redirect("/TaiKhoanAdmin/DangNhap");
        }
        public ActionResult TaiKhoan()
        {
            //if (KiemTraDangNhap() == false)
            //{
            //    return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            //}
            if (Session["EmailDangNhap"] == null)
            {
                return null;
            }
            string email = Session["EmailDangNhap"].ToString();
            var v = db.TaiKhoan.FirstOrDefault(n => n.Email == email);
            //var v = db.TaiKhoans.FirstOrDefault(n => n.MaTaiKhoan == Int32.Parse(Session["MaTaiKhoan"].ToString()));
            
            return View(v);
        }
        public ActionResult TaiKhoanChiTiet()
        {
            if (KiemTraDangNhap() == false)
            {
                return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            }
            int c = Convert.ToInt32(Session["MaTaiKhoan"]);         
            var a=db.TaiKhoan.FirstOrDefault(n => n.MaTaiKhoan == c);
            return View(a);
        }
        public ActionResult DanhSachTaikhoan(int? page)
        {
            if (KiemTraDangNhap() == false)
            {
                return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            }
            if(Convert.ToInt32(Session["LoaiTK"]) == 3)
            {
                return RedirectToAction("TaiKhoanChiTiet", "TaiKhoanAdmin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var taiKhoan = db.TaiKhoan;
            return View(taiKhoan.ToList().OrderBy(n => n.MaTaiKhoan).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            if (KiemTraDangNhap() == false)
            {
                return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            }

            ViewBag.LoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan");
                return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,Email,MatKhau,XacNhanMatKhau,NhoMatKhau,LoaiTaiKhoan,TenNguoiDung,HinhAnh,DiaChi,SoDienThoai")] TaiKhoan taiKhoan, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/TaiKhoan/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        taiKhoan.HinhAnh = _FileName;
                    }
                    taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
                    db.TaiKhoan.Add(taiKhoan);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachTaikhoan", "TaiKhoanAdmin");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            //if (ModelState.IsValid)
            //{
            //    taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
            //    db.TaiKhoan.Add(taiKhoan);
            //    db.SaveChanges();
            //    return RedirectToAction("DanhSachTaikhoan", "TaiKhoanAdmin");
            //}

            ViewBag.LoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.LoaiTaiKhoan);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (KiemTraDangNhap() == false)
            {
                return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.LoaiTaiKhoan);
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTaiKhoan,Email,MatKhau,XacNhanMatKhau,NhoMatKhau,LoaiTaiKhoan,TenNguoiDung,HinhAnh,DiaChi,SoDienThoai")] TaiKhoan taiKhoan, HttpPostedFileBase HinhAnh, FormCollection form)
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
                    taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DanhSachTaikhoan");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

                return RedirectToAction("DanhSachTaikhoan");
            }
            //if (ModelState.IsValid)
            //{
            //    taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
            //    db.Entry(taiKhoan).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("DanhSachTaikhoan");
            //}
            ViewBag.LoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.LoaiTaiKhoan);
            return View(taiKhoan);
        }
        public ActionResult Edit2(int? id)
        {
            if (KiemTraDangNhap() == false)
            {
                return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }            
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "MaTaiKhoan,Email,MatKhau,XacNhanMatKhau,NhoMatKhau,LoaiTaiKhoan,TenNguoiDung,HinhAnh,DiaChi,SoDienThoai")] TaiKhoan taiKhoan)
        {

            if (ModelState.IsValid)
            {
                taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/TaiKhoanAdmin/TaiKhoanChiTiet");
            }
            //ViewBag.LoaiTaiKhoan = new SelectList(db.LoaiTaiKhoan, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.LoaiTaiKhoan);
            return View(taiKhoan);
        }
        // GET: TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (KiemTraDangNhap() == false)
            {
                return RedirectToAction("DangNhap", "TaiKhoanAdmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            db.TaiKhoan.Remove(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("DanhSachTaikhoan");
        }
        public bool KiemTraDangNhap()
        {
            if (Session["MaTaiKhoan"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return false;
            }
            return true;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}