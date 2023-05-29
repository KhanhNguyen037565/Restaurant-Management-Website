using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
namespace WebsiteNhaHang.Controllers
{
    public class GioiThieuController : Controller
    {
        NhaHangEntities db=new NhaHangEntities();
        // GET: GioiThieu
        public ActionResult KhongGianNhaHang()
        {   
            return View(db.LoaiKhongGianNhaHang.OrderBy(n=>n.TenLoai).ToList());
        }

        public PartialViewResult AnhGioiThieu(int lanHien)
        {
            return PartialView(db.KhongGianNhaHang.Take(lanHien).OrderBy(n=>n.NgayDang).ToList());
        }
        public PartialViewResult AnhGioiThieuSuKien(int lanHien)
        {
            return PartialView(db.KhongGianNhaHang.Take(lanHien).OrderByDescending(n => n.NgayDang).ToList());
        }
        public ActionResult ThongTinNhaHang()
        {            
            return View(db.ThongTinNhaHang.ToList());
        }
        //public ActionResult ThongTinNhaHang1()
        //{
        //    return View(db.ThongTinNhaHang.ToList());
        //}
        public PartialViewResult TrangDangNhap()
        {
            return PartialView(db.ThongTinNhaHang.ToList());
        }
    }
}