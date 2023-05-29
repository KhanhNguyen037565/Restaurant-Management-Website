using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(SuKienMetadata))]
    public partial class SuKien
    {
        internal sealed class SuKienMetadata
        {
            public int MaSuKien { get; set; }
            [Display(Name = "Tên")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenSuKien { get; set; }
            [Display(Name = "Ngày bắt đầu")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<System.DateTime> NgayBatDau { get; set; }
            [Display(Name = "Ngày kết thúc")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<System.DateTime> NgayKetThuc { get; set; }
            [Display(Name = "Ngày đăng")]
            public Nullable<System.DateTime> NgayDang { get; set; }
            [Display(Name = "Hình Ảnh")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string HinhAnh { get; set; }
            [Display(Name = "Chi Tiết")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string ChiTiet { get; set; }
            [Display(Name = "Người đăng")]
            public Nullable<int> NguoiDang { get; set; }
        }
    }
}