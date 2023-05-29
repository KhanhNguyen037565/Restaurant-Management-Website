using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNhaHang.Models
{
    [MetadataTypeAttribute(typeof(TaiKhoanMetadata))]
    public partial class TaiKhoan
    {
        internal sealed class TaiKhoanMetadata
        {
            public int MaTaiKhoan { get; set; }
            [Required(ErrorMessage = "{0} không được để trống!")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Display(Name = "Mật Khẩu")]
            [DataType(DataType.Password)]
            [StringLength(40, MinimumLength = 6, ErrorMessage = "Mật khẩu dài từ 6 đến 20 ký tự!")]
            public string MatKhau { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Xác Nhận Mật Khẩu")]
            public string XacNhanMatKhau { get; set; }
            [Display(Name = "Nhớ Mật Khẩu")]
            public Nullable<bool> NhoMatKhau { get; set; }
            [Display(Name = "Loại Tài Khoản")]
            public int LoaiTaiKhoan { get; set; }
            [Display(Name = "Tên Người Dùng")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TenNguoiDung { get; set; }
            [Display(Name = "Hình Đại Diện")]
            public string HinhAnh { get; set; }
            [Display(Name = "Địa Chỉ")]
            public string DiaChi { get; set; }
            [Display(Name = "Số Diện Thoại")]
            [DataType(DataType.PhoneNumber)]
            //[RegularExpression(@"^\(?([0-9]*?)$", ErrorMessage = "{0} không hợp lệ!")]
            [StringLength(11, ErrorMessage = "Số điện thoại không hợp lệ!")]
            public string SoDienThoai { get; set; }
        }
    }
}