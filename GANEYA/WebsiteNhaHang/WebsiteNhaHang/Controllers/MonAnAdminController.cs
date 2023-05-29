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
    public class MonAnAdminController : Controller
    {
        private NhaHangEntities db = new NhaHangEntities();

        // GET: QuanLyMonAns
        public ActionResult Index(int? page)
        {
            if (Session["LoaiTK"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return Redirect("/TaiKhoanAdmin/DangNhap");
            }
            if (Session["LoaiTK"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return Redirect("/TaiKhoanAdmin/DangNhap");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var monAns = db.MonAn;
            return View(monAns.ToList().OrderBy(n => n.TenMon).ToPagedList(pageNumber, pageSize));
        }
        // Ajax POST: /Checkout/UseShippingAddress/5
        [HttpPost]
        public ActionResult UseShippingAddress(int id)
        {
            return Content("It worked!");
        }
        public ActionResult GetData()
        {
            using (NhaHangEntities db = new NhaHangEntities())
            {
                var monAnList = db.MonAn.ToList<MonAn>();
                return Json(new { data = monAnList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrSua(int id = 0)
        {
            return View(new MonAn());
        }
        [HttpPost]
        public ActionResult AddOrSua()
        {
            return View();
        }
        // GET: QuanLyMonAns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAn.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // GET: QuanLyMonAns/Create
        public ActionResult Create()
        {
            ViewBag.LoaiMon = new SelectList(db.LoaiMon, "MaLoai", "TenLoai");
            return View();
        }

        // POST: QuanLyMonAns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMon,TenMon,LoaiMon,HinhAnh,Gia,ChiTiet,SoLuotDat")] MonAn monAn, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        monAn.HinhAnh = _FileName;
                    }


                    db.MonAn.Add(monAn);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }

            ViewBag.LoaiMon = new SelectList(db.LoaiMon, "MaLoai", "TenLoai", monAn.LoaiMon);
            return View(monAn);
        }

        // GET: QuanLyMonAns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAn.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiMon = new SelectList(db.LoaiMon, "MaLoai", "TenLoai", monAn.LoaiMon);
            return View(monAn);
        }

        // POST: QuanLyMonAns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMon,TenMon,LoaiMon,HinhAnh,Gia,ChiTiet,SoLuotDat")] MonAn monAn, HttpPostedFileBase HinhAnh, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/"), _FileName);
                        HinhAnh.SaveAs(_path);
                        monAn.HinhAnh = _FileName;
                        // get Path of old image for deleting it
                        _path = Path.Combine(Server.MapPath("~/Content/img/MonAn/"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                        monAn.HinhAnh = form["oldimage"];
                    db.Entry(monAn).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

                return RedirectToAction("Index");
            }
            ViewBag.LoaiMon = new SelectList(db.LoaiMon, "MaLoai", "TenLoai", monAn.LoaiMon);
            return View(monAn);
        }
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
            MonAn monAn = db.MonAn.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // POST: MonAns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonAn monAn = db.MonAn.Find(id);
            while (db.MonAn.Find(id).GoiCombo!=null)
            {
                db.MonAn.Find(id).GoiCombo=null;
                db.SaveChanges();
            }
            db.MonAn.Remove(monAn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: QuanLyMonAns/Delete/5

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
