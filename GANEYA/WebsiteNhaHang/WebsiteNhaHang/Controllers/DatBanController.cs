using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNhaHang.Models;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Text;

namespace WebsiteNhaHang.Controllers
{
    public class DatBanController : Controller
    {
        NhaHangEntities db = new NhaHangEntities();
        // GET: DatBan
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>; // 1 biến lstGioHang được khởi tạo và gán giá trị của Session cho nó
            //kiem tra, khoi tao gio hang
            if (lstGioHang == null) // nếu giỏ hàng rỗng
            {
                lstGioHang = new List<GioHang>(); //khởi tạo và gán 1 danh sách mới cho lstGioHang
                Session["GioHang"] = lstGioHang; // session là 1 cơ chế trong ASP.NET lưu trữ thông tin của mỗi phiên làm việc // sau đó lưu lại thông tin đã thay đổi
            }
            return lstGioHang; // trả về 1 danh sách các đối tượng giỏ hàng
        }
        public ActionResult ThemGioHang(int iMaSp,int iLoaiDat,string strURL)
        {
            MonAn monAn = db.MonAn.SingleOrDefault(n => n.MaMon == iMaSp); //truy xuất món ăn
            GoiCombo goiCombo = db.GoiCombo.SingleOrDefault(n => n.MaCombo == iMaSp); // truy xuất gói combo
            if (monAn == null&&iLoaiDat==1)
            {
                Response.StatusCode = 404; //trả về lỗi kh tìm thấy
                return null; // kh cập nhật
            }
            if (iLoaiDat != 1&& iLoaiDat != 2)
            {
                Response.StatusCode = 404; //trả về lỗi kh tìm thấy
                return null; // kh cập nhật
            }
            if (goiCombo == null && iLoaiDat == 2)
            {
                Response.StatusCode = 404; //trả về lỗi kh tìm thấy
                return null; // kh cập nhật
            }
            List<GioHang> lstGioHang = LayGioHang(); 
            GioHang sanPham = lstGioHang.Find(n => n.iMaSP == iMaSp&&n.iLoaiDat==iLoaiDat); // truy xuất sản phẩm trong giỏ hàng 
            if (sanPham == null)
            {
                sanPham = new GioHang(iMaSp,iLoaiDat); //mục hàng chưa tồn tại trong giỏ, 
                lstGioHang.Add(sanPham); // thêm vào giỏ hàng
                return Redirect(strURL); // chuyển hướng đến URL
            }
            else
            {
                sanPham.iSoLuong++; // đã tồn tại sp, tăng số lượng lên 1
                return Redirect(strURL); // chuyển hướng đến URL 
            }
        }
        
        [HttpPost]
        public JsonResult CapNhapSoLuong(int iSoLuong,int iMaSP,int iLoaiDat) // nhận vào 3 tham số
        {
            List<GioHang> lstGioHang = LayGioHang(); //lấy danh sách các mục trong giỏ hàng. // lstGioHang được khởi tạo bằng cách gọi phương thức LayGioHang
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP && n.iLoaiDat==iLoaiDat); // 1 đối tượng trong giỏ hàng được truy xuất sản phẩm có masp và laoi đặt tương ứng
            if(sanpham!=null)
            {
                sanpham.iSoLuong = iSoLuong; // nếu tìm thấy thì cập nhật lại số lượng
            }
            
            return Json(new
            {
                status = true // trả về trạng thái đã cập nhật
            });
        }
        [HttpPost]
        public JsonResult XoaGioHang(int iMaSP,int iLoaiDat)
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //GioHang sanPham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP&& n.iLoaiDat == iLoaiDat);
            //if (sanPham != null)
            //{
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP && n.iLoaiDat == iLoaiDat);
                Session["GioHang"] = lstGioHang;
            //}
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult XoaTatCa()
        {
            Session.Remove("GioHang");
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public ActionResult ThanhToan(int soNguoi, DateTime ngayAn)
        {
            int i = 0;
            if (Session["MaTaiKhoan"] == null)
            {               
                return Json("Bạn cần đăng nhập trước khi đặt bàn!");
            }
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>; // lấy ra các ds sp trong session ds giỏ hàng & ép kiểu  thành ds giỏ hàng
            var datBan = new DatBan();         // khởi tạo biến đặt bàn
            datBan.MaKhachHang = int.Parse(Session["MaTaiKhoan"].ToString()); // gán gtri
            datBan.NgayDatBan = DateTime.Now; // gán gtri
            datBan.NgayThucHien = ngayAn; // gán gtri
            datBan.TongTien = TongTien(); // gán gtri
            datBan.SoNguoi = soNguoi; // gán gtri
            db.DatBan.Add(datBan); // đối tượng đặt bàn đc thêm vào trong csdl
            db.SaveChanges(); // lưu thông tin đặt bàn
            foreach (var item in lstGioHang) // lặp từng đối tg trong giỏ hàng
            {                
                if (item.iLoaiDat == 1) // loại đặt bằng 1 tức là món ăn
                {
                    var dsMonAn = new DanhSachMonDatBan(); // khai báo biến 
                    dsMonAn.MaMonAn = item.iMaSP; // gán gtri
                    dsMonAn.SoLuong = item.iSoLuong; // gán gtri
                    dsMonAn.MaDatBan = datBan.MaDatBan; // gán gtri
                    dsMonAn.KieuDatBan = 1; // gán gtri
                    db.DanhSachMonDatBan.Add(dsMonAn); //  đối tượng mon đặt bàn đc lưu vào csdl
                    var f = db.MonAn.FirstOrDefault(n => n.MaMon == item.iMaSP); 
                    f.SoLuotDat++; // số lg đặt tăng
                   // db.MonAn.Add(f); 
                    db.SaveChanges();
                }
                if (item.iLoaiDat == 2) // loại đặt bằng 2 tức là combo
                {
                    var dsCombo = new DanhSachDatCombo(); // khai báo biến
                    dsCombo.MaDatBan = datBan.MaDatBan; // gán gtri
                    dsCombo.MaCombo = item.iMaSP; // gán gtri
                    dsCombo.KieuDatBan = 2; // gán gtri
                    dsCombo.SoLuong = item.iSoLuong; // gán gtri
                    db.DanhSachDatCombo.Add(dsCombo); // đối tg đtặ bàn combo đc lưu vào csdl
                    var d = db.GoiCombo.FirstOrDefault(n => n.MaCombo == item.iMaSP);
                    d.SoLanDat++; // số lg đặt tăng
                   // db.GoiCombo.Add(d); 
                    db.SaveChanges();
                }
            }
            
            Session.Remove("GioHang"); // xóa giỏ hàng
            return Json(new
            {
              status = true // cập nhật giỏ hàng thành công
            });
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"]==null)
            {
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TongTien(); // gán và tính tổng của các giá trị trong giỏ hàng
            return View(lstGioHang); //danh sách lstGioHang được truyền vào view để hiển thị các mục hàng trong giỏ hàng.
        }
        private int TongSoLuong()
        {
            int tongSoLuong = 0; 
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>; //danh sách lstGioHang được lấy từ Session["GioHang"] và ép kiểu thành danh sách GioHang.
            if (lstGioHang != null)
            {
                tongSoLuong = lstGioHang.Sum(n => n.iSoLuong); // nhiệm vụ tính tổng số lượng
            }
            return tongSoLuong; // trả về tổng sl
        }
        private double TongTien()
        {
            double tongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>; //danh sách lstGioHang được lấy từ Session["GioHang"] và ép kiểu thành danh sách GioHang.
            if (lstGioHang != null)
            {
                tongTien = lstGioHang.Sum(n => n.dThanhTien); // nhiệm vụ tính tổng tiền
            }
            return tongTien; // trả về tổng tiền
        }
        public PartialViewResult tongSoLuong()
        {
            ViewBag.TongSoLuong = TongSoLuong(); //gán tổng số lượng để hiển thị lên view
            return PartialView();
        }
        
    }
}