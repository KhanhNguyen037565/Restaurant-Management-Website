using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
namespace WebsiteNhaHang.Controllers
{
    public class BinhLuanController : Controller
    {
        NhaHangEntities db=new NhaHangEntities();
        // GET: BinhLuan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachBinhLuan()
        {
            return View(db.BinhLuan.OrderByDescending(n=>n.NgayDang).Take(15).ToList());
        }

        public PartialViewResult DangBinhLuan()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult DangBinhLuan(BinhLuan bl)
        {
            if (ModelState.IsValid)
            {
                if (Session["MaTaiKhoan"] == null)
                {
                    ViewBag.ThongBao = "Bạn cần đăng nhập trước khi bình luận";
                    return PartialView();
                }
                int a = Convert.ToInt32(Session["MaTaiKhoan"]);
                bl.MaTaiKhoan = a;
                bl.NgayDang = DateTime.Now;
                db.BinhLuan.Add(bl);
                db.SaveChanges();
            }
            return PartialView();
        }
    }
}