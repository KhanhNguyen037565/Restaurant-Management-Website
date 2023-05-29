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
namespace WebsiteNhaHang.Controllers
{
    public class DatBanAdminController : Controller
    {
        private NhaHangEntities db = new NhaHangEntities();

        // GET: DatBanAdmin
        public ActionResult Index(int? page)
        {
            if (Session["LoaiTK"] == null || Convert.ToInt32(Session["LoaiTK"]) == 2)
            {
                return Redirect("/TaiKhoanAdmin/DangNhap");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var datBan = db.DatBan.Include(d => d.TaiKhoan);
            return View(datBan.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: DatBanAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatBan datBan = db.DatBan.Find(id);
            if (datBan == null)
            {
                return HttpNotFound();
            }
            return View(datBan);
        }
        // GET: DatBanAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatBan datBan = db.DatBan.Find(id);
            if (datBan == null)
            {
                return HttpNotFound();
            }
            return View(datBan);
        }

        // POST: DatBanAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Convert.ToInt32(Session["LoaiTK"]) == 3)
            {
                return RedirectToAction("Index");
            }
            DatBan datBan = db.DatBan.Find(id);                        
            while (db.DanhSachMonDatBan.FirstOrDefault(n => n.MaDatBan == id) != null)
            {
                DanhSachMonDatBan monDB = db.DanhSachMonDatBan.FirstOrDefault(n => n.MaDatBan == id);
                db.DanhSachMonDatBan.Remove(monDB);
                db.SaveChanges();
            }
            while (db.DanhSachDatCombo.FirstOrDefault(n => n.MaDatBan == id) != null)
            {
                DanhSachDatCombo comboDB = db.DanhSachDatCombo.FirstOrDefault(n => n.MaDatBan == id);
                db.DanhSachDatCombo.Remove(comboDB);
                db.SaveChanges();
            }
            db.DatBan.Remove(datBan);
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
