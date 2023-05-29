using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(MonAnMetadata))]
    public partial class MonAn
    {
        internal sealed class MonAnMetadata
        {
            public int MaMon { get; set; }
            [Display(Name = "Tên món")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenMon { get; set; }
            [Display(Name = "Loại món")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<int> LoaiMon { get; set; }
            [Display(Name = "Hình Ảnh")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string HinhAnh { get; set; }
            [Display(Name = "Giá")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<double> Gia { get; set; }
            [Display(Name = "Chi tiết")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string ChiTiet { get; set; }
            [Display(Name = "Số lượt đặt")]
            public Nullable<int> SoLuotDat { get; set; }
        }
    }
}