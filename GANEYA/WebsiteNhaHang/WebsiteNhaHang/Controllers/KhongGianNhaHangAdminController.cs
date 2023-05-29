using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebsiteNhaHang.Controllers
{
    public class KhongGianNhaHangAdminController : Controller
    {
        private NhaHangEntities db = new NhaHangEntities();

        // GET: KhongGianNhaHangAdmin
        public ActionResult Index(int? page)
        {
            if (Session["LoaiTK"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return Redirect("/TaiKhoanAdmin/DangNhap");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var khongGianNhaHang = db.KhongGianNhaHang.Include(k => k.LoaiKhongGianNhaHang).Include(k => k.TaiKhoan);
            return View(khongGianNhaHang.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: KhongGianNhaHangAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhongGianNhaHang khongGianNhaHang = db.KhongGianNhaHang.Find(id);
            if (khongGianNhaHang == null)
            {
                return HttpNotFound();
            }
            return View(khongGianNhaHang);
        }

        // GET: KhongGianNhaHangAdmin/Create
        public ActionResult Create()
        {
            ViewBag.LoaiKhongGian = new SelectList(db.LoaiKhongGianNhaHang, "MaLoaiKhongGian", "TenLoai");
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email");
            return View();
        }

        // POST: KhongGianNhaHangAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhongGian,TenKhongGian,HinhAnh,GioiThieu,LoaiKhongGian,NguoiDang,NgayDang")] KhongGianNhaHang khongGianNhaHang, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/KhongGian/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        khongGianNhaHang.HinhAnh = _FileName;
                    }
                    int c = Convert.ToInt32(Session["MaTaiKhoan"]);
                    khongGianNhaHang.NguoiDang = c;
                    khongGianNhaHang.NgayDang = DateTime.Now.Date;
                    db.KhongGianNhaHang.Add(khongGianNhaHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }

            ViewBag.LoaiKhongGian = new SelectList(db.LoaiKhongGianNhaHang, "MaLoaiKhongGian", "TenLoai", khongGianNhaHang.LoaiKhongGian);
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", khongGianNhaHang.NguoiDang);
            return View(khongGianNhaHang);
        }

        // GET: KhongGianNhaHangAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhongGianNhaHang khongGianNhaHang = db.KhongGianNhaHang.Find(id);
            if (khongGianNhaHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiKhongGian = new SelectList(db.LoaiKhongGianNhaHang, "MaLoaiKhongGian", "TenLoai", khongGianNhaHang.LoaiKhongGian);
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", khongGianNhaHang.NguoiDang);
            return View(khongGianNhaHang);
        }

        // POST: KhongGianNhaHangAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhongGian,TenKhongGian,HinhAnh,GioiThieu,LoaiKhongGian,NguoiDang,NgayDang")] KhongGianNhaHang khongGianNhaHang, HttpPostedFileBase HinhAnh, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/KhongGian/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        khongGianNhaHang.HinhAnh = _FileName;
                        // get Path of old image for deleting it
                        _path = Path.Combine(Server.MapPath("~/Content/img/KhongGian/"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                        khongGianNhaHang.HinhAnh = form["oldimage"];
                    int c = Convert.ToInt32(Session["MaTaiKhoan"]);
                    khongGianNhaHang.NguoiDang = c;
                    khongGianNhaHang.NgayDang = DateTime.Now.Date;
                    db.Entry(khongGianNhaHang).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

                return RedirectToAction("Index");
            }
            ViewBag.LoaiKhongGian = new SelectList(db.LoaiKhongGianNhaHang, "MaLoaiKhongGian", "TenLoai", khongGianNhaHang.LoaiKhongGian);
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", khongGianNhaHang.NguoiDang);
            return View(khongGianNhaHang);
        }

        // GET: KhongGianNhaHangAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["LoaiTK"]) == 3)
            {
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhongGianNhaHang khongGianNhaHang = db.KhongGianNhaHang.Find(id);
            if (khongGianNhaHang == null)
            {
                return HttpNotFound();
            }
            return View(khongGianNhaHang);
        }

        // POST: KhongGianNhaHangAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhongGianNhaHang khongGianNhaHang = db.KhongGianNhaHang.Find(id);
            db.KhongGianNhaHang.Remove(khongGianNhaHang);
            db.SaveChanges();
            return RedirectToAction("Index");
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
