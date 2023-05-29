using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(KhongGianNhaHangMetadata))]
    public partial class KhongGianNhaHang
    {
        internal sealed class KhongGianNhaHangMetadata
        {
            public int MaKhongGian { get; set; }
            [Display(Name = "Tên khu vực")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenKhongGian { get; set; }
            [Display(Name = "Hình Ảnh")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string HinhAnh { get; set; }
            [Display(Name = "Giới thiệu")]
            public string GioiThieu { get; set; }
            [Display(Name = "Loại không gian")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<int> LoaiKhongGian { get; set; }
            [Display(Name = "Người đăng")]
            public Nullable<int> NguoiDang { get; set; }
            [Display(Name = "Ngày đăng")]
            public Nullable<System.DateTime> NgayDang { get; set; }
        }
    }
}