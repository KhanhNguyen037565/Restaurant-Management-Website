using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(DatBanMetadata))]
    public partial class DatBan
    {
        internal sealed class DatBanMetadata
        {
            public int MaDatBan { get; set; }
            [Display(Name = "Ngày đặt")]
            public Nullable<System.DateTime> NgayDatBan { get; set; }
            [Display(Name = "Tên Khách hàng")]
            public Nullable<int> MaKhachHang { get; set; }
            [Display(Name = "Ngày đến ăn")]
            public Nullable<System.DateTime> NgayThucHien { get; set; }
            [Display(Name = "Số người")]
            public Nullable<int> SoNguoi { get; set; }
            [Display(Name = "Tổng Tiền")]
            public Nullable<double> TongTien { get; set; }
        }

        }
}