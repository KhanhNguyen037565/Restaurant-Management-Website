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
    public class TuyenDungController : Controller
    {
        private NhaHangEntities db = new NhaHangEntities();
        // GET: TuyenDung
        public ActionResult TuyenDung(int? page)
        {
            ViewBag.Dem = 0;
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.TuyenDung.OrderByDescending(n => n.MaTuyenDung).ToList().ToPagedList(pageNumber, pageSize));

        }

        public PartialViewResult DemTuyenDung()
        {
            ViewBag.DemTuyenDung = db.TuyenDung.Count();
            return PartialView();
        }

    }
}