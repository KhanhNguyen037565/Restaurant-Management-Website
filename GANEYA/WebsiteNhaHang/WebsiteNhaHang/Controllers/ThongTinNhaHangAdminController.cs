using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;

namespace TrangQuanLyNhaHang.Controllers
{
    public class ThongTinNhaHangAdminController : Controller
    {
        private NhaHangEntities db = new NhaHangEntities();

        // GET: ThongTinNhaHangs
        public ActionResult Index()
        {
            if (Session["LoaiTK"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return Redirect("/TaiKhoanAdmin/DangNhap");
            }
            var thongTinNhaHangs = db.ThongTinNhaHang;
            return View(thongTinNhaHangs.ToList());
        }

        // GET: ThongTinNhaHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinNhaHang thongTinNhaHang = db.ThongTinNhaHang.Find(id);
            if (thongTinNhaHang == null)
            {
                return HttpNotFound();
            }
            return View(thongTinNhaHang);
        }


        // GET: ThongTinNhaHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["LoaiTK"]) == 3|| Session["LoaiTK"]==null)
            {
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinNhaHang thongTinNhaHang = db.ThongTinNhaHang.Find(id);
            if (thongTinNhaHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", thongTinNhaHang.NguoiDang);
            return View(thongTinNhaHang);
        }

        // POST: ThongTinNhaHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongTin,NguoiDang,URLMap,TenNhaHang,DiaChi,Email,SoDienThoai1,SoDienThoai2,ThongTinKhac")] ThongTinNhaHang thongTinNhaHang)
        {

            if (ModelState.IsValid)
            {
                db.Entry(thongTinNhaHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiDang = new SelectList(db.TaiKhoan, "MaTaiKhoan", "Email", thongTinNhaHang.NguoiDang);
            return View(thongTinNhaHang);
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
