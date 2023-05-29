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
    public class SuKiensAdminController : Controller
    {
        private NhaHangEntities db = new NhaHangEntities();

        // GET: SuKiensAdmin
        public ActionResult Index(int? page)
        {
            if (Session["LoaiTK"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return Redirect("/TaiKhoanAdmin/DangNhap");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var suKien = db.SuKien.Include(s => s.TaiKhoan);
            return View(suKien.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: SuKiensAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKien.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // GET: SuKiensAdmin/Create
        public ActionResult Create()
        {
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email");
            return View();
        }

        // POST: SuKiensAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSuKien,TenSuKien,NgayBatDau,NgayKetThuc,NgayDang,HinhAnh,ChiTiet,NguoiDang")] SuKien suKien, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/KhuyenMai-Sukien/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        suKien.HinhAnh = _FileName;
                    }
                    int a = Convert.ToInt32(Session["MaTaiKhoan"]);
                    suKien.NguoiDang = a;
                    suKien.NgayDang = DateTime.Now;
                    db.SuKien.Add(suKien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }


            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", suKien.NguoiDang);
            return View(suKien);
        }

        // GET: SuKiensAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKien.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", suKien.NguoiDang);
            return View(suKien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSuKien,TenSuKien,NgayBatDau,NgayKetThuc,NgayDang,HinhAnh,ChiTiet,NguoiDang")] SuKien suKien, HttpPostedFileBase HinhAnh, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/KhuyenMai-SuKien/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        suKien.HinhAnh = _FileName;

                        //get path of old image deleting it
                        _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/KhuyenMai-SuKien/"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                        {
                            System.IO.File.Delete(_path);
                        }
                    }
                    else
                        suKien.HinhAnh = form["oldimage"];
                    int a = Convert.ToInt32(Session["MaTaiKhoan"]);
                    suKien.NgayDang = DateTime.Now;
                    suKien.NguoiDang = a;
                    db.Entry(suKien).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }

            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", suKien.NguoiDang);
            return View(suKien);
        }

        //// POST: SuKiensAdmin/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaSuKien,TenSuKien,NgayBatDau,NgayKetThuc,NgayDang,HinhAnh,ChiTiet,NguoiDang")] SuKien suKien, HttpPostedFileBase HinhAnh, FormCollection form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (HinhAnh != null)
        //            {
        //                string _FileName = Path.GetFileName(HinhAnh.FileName);
        //                string _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/KhuyenMai-Sukien/"), _FileName);
        //                HinhAnh.SaveAs(_path);
        //                suKien.HinhAnh = _FileName;
        //                // get Path of old image for deleting it
        //                _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/KhuyenMai-Sukien/"), form["oldimage"]);
        //                if (System.IO.File.Exists(_path))
        //                    System.IO.File.Delete(_path);

        //            }
        //            else
        //                suKien.HinhAnh = form["oldimage"];
        //            int a = Convert.ToInt32(Session["MaTaiKhoan"]);
        //            suKien.NguoiDang = a;
        //            suKien.NgayDang = DateTime.Now;
        //            db.Entry(suKien).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            ViewBag.Message = "không thành công!!";
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", suKien.NguoiDang);
        //    return View(suKien);
        //}

        // GET: SuKiensAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKien.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // POST: SuKiensAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuKien suKien = db.SuKien.Find(id);
            db.SuKien.Remove(suKien);
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
