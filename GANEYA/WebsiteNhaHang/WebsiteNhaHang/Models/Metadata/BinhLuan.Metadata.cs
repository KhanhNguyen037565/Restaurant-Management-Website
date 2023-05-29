using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(BinhLuanMetadata))]
    public partial class BinhLuan
    {
        internal sealed class BinhLuanMetadata
        {
            public int MaCuaBinhLuan { get; set; }
            [Display(Name = "Tên tài khoản")]
            public int MaTaiKhoan { get; set; }
            [Display(Name = "Bình luận")]
            public string BinhLuan1 { get; set; }
            [Display(Name = "Ngày đăng")]
            public Nullable<System.DateTime> NgayDang { get; set; }
        }
    }
}