using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
using PagedList;
using PagedList.Mvc;
namespace WebsiteNhaHang.Controllers
{
    public class ComboController : Controller
    {
        NhaHangEntities db=new NhaHangEntities();
        // GET: Combo
        public ActionResult GoiCombo(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.GoiCombo.ToList().OrderBy(n => n.TenComBo).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult ComboDatNhieu()
        {
            return PartialView(db.GoiCombo.Take(3).OrderBy(n => n.SoLanDat).ToList());
        }

        public ActionResult DemCombo()
        {
            ViewBag.DemCombo= db.GoiCombo.Count();
            return View();
        }
        

    }
}