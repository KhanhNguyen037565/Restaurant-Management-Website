using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteNhaHang.Controllers
{
    public class ThucDonController : Controller
    {
        NhaHangEntities db = new NhaHangEntities();
        // GET: ThucDon
        public ActionResult ThucDon(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.MonAn.ToList().OrderBy(n => n.TenMon).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult MonDuocDatNhieu()
        {
            return PartialView(db.MonAn.Take(3).OrderBy(n => n.SoLuotDat).ToList());
        }
        public PartialViewResult LoaiMonAn()
        {
            return PartialView(db.LoaiMon.ToList());
        }
        public ViewResult MonAnTheoLoai(int maLoai = 0)
        {
            LoaiMon lm = db.LoaiMon.SingleOrDefault(n => n.MaLoai == maLoai);
            if (lm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<MonAn> lstMonAn = db.MonAn.Where(n => n.LoaiMon == maLoai).OrderBy(n => n.TenMon).ToList();
            if (lstMonAn.Count == 0)
            {
                ViewBag.MonAn = "Không có món ăn nào trong loại này!!";
            }
            return View(lstMonAn);
        }
    }
}