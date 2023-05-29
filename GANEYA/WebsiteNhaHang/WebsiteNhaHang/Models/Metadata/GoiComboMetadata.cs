using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(GoiComboMetadata))]
    public partial class GoiCombo
    {
        internal sealed class GoiComboMetadata
        {
            public int MaCombo { get; set; }
            [Display(Name = "Tên Combo")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenComBo { get; set; }
            [Display(Name = "Hình Ảnh")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string HinhAnh { get; set; }
            [Display(Name = "Giá")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<double> Gia { get; set; }
            [Display(Name = "Giới thiệu")]
            public string GioiThieu { get; set; }
            [Display(Name = "Số lần đặt")]
            public Nullable<int> SoLanDat { get; set; }
        }

    }
}