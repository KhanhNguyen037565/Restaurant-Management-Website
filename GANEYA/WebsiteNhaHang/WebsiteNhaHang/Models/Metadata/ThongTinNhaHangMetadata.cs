using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(ThongTinNhaHangMetadata))]
    public partial class ThongTinNhaHang
    {
        internal sealed class ThongTinNhaHangMetadata
        {
            public int MaThongTin { get; set; }
            [Display(Name = "Người đăng")]
            public int NguoiDang { get; set; }
            [Display(Name = "Map google")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string URLMap { get; set; }
            [Display(Name = "Tên nhà hàng")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenNhaHang { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string DiaChi { get; set; }
            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string Email { get; set; }
            [Display(Name = "Số điện thoại 1")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string SoDienThoai1 { get; set; }
            [Display(Name = "Số điện thoại 2")]
            public string SoDienThoai2 { get; set; }
            [Display(Name = "Thông tin khác")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string ThongTinKhac { get; set; }
        }
    }
}