using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteNhaHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                name: "Đăng nhập Admin",
                url: "Admin",
                defaults: new { controller = "TaiKhoanAdmin", action = "DangNhap", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }

            );
            routes.MapRoute(
                name: "Giới thiệu về Ganeya Nhật Bản",
                url: "GioiThieu",
                defaults: new { controller = "GioiThieu", action = "KhongGianNhaHang", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }

            );
            routes.MapRoute(
                name: "Các món Nhật ngon",
                url: "ThucDon",
                defaults: new { controller = "ThucDon", action = "ThucDon", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }

            );
            routes.MapRoute(
                name: "Các gói Combo món ăn Nhật Bản",
                url: "Combo",
                defaults: new { controller = "Combo", action = "GoiCombo", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }

            );
            routes.MapRoute(
                name: "Các chương trình khuyến mãi của nhà hàng",
                url: "KhuyenMaiSuKien",
                defaults: new { controller = "KhuyenMaiSuKien", action = "KhuyenMai", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }
            );
            routes.MapRoute(
                name: "Thông tin tuyển dụng",
                url: "TuyenDung",
                defaults: new { controller = "TuyenDung", action = "TuyenDung", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }
            );
            routes.MapRoute(
                name: "Trang đăng nhập",
                url: "DangNhap",
                defaults: new { controller = "TaiKhoan", action = "DangNhap", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }
            );
            routes.MapRoute(
                name: "Trang đăng ký thành viên",
                url: "DangKy",
                defaults: new { controller = "TaiKhoan", action = "DangKy", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }
            );
            routes.MapRoute(
                name: "Thông tin tài khoản",
                url: "TaiKhoanChiTiet",
                defaults: new { controller = "TaiKhoan", action = "TaiKhoanChiTiet", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNhaHang.Controllers" }
            );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteNhaHang.Controllers" }
           );
        }
    }
}
