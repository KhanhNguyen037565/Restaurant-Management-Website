using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;

namespace WebsiteNhaHang.Controllers
{
    public class KhuyenMaiSukienController : Controller
    {
        NhaHangEntities db = new NhaHangEntities();
        // GET: KhuyenMai
        public ActionResult KhuyenMai()
        {   
            return View(db.KhuyenMai.OrderBy(n => n.NgayDang).ToList());
        }
        public ActionResult SuKien()
        {
            return PartialView(db.SuKien.OrderBy(n => n.NgayDang).ToList());
        }
        public ActionResult DemSuKien_KhuyenMai() //Đếm
        {
            ViewBag.dem= db.KhuyenMai.Count() + db.SuKien.Count();
            return PartialView();
        }
        public PartialViewResult AnhGioiThieu()
        {
            return  PartialView(db.KhuyenMai.Take(2).ToList());
        }
        public PartialViewResult AnhSuKien()
        {       
            return PartialView(db.SuKien.Take(2).ToList());
        }
    }
}