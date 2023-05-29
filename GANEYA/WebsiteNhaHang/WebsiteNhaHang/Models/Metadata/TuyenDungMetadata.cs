using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(TuyenDungMetadata))]
    public partial class TuyenDung
    {
        internal sealed class TuyenDungMetadata
        {
            public int MaTuyenDung { get; set; }
            [Display(Name = "Tên")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenTuyenDung { get; set; }
            [Display(Name = "Ngày đăng")]
            public System.DateTime NgayDang { get; set; }
            [Display(Name = "Ngày bắt đầu")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<System.DateTime> NgayBatDau { get; set; }
            [Display(Name = "Ngày kêt thúc")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<System.DateTime> NgayKetThuc { get; set; }
            [Display(Name = "Chi tiết")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string ChiTiet { get; set; }
            [Display(Name = "Người đăng")]
            public int NguoiDang { get; set; }
        }
    }
}