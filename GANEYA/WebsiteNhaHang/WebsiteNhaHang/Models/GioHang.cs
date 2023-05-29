using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNhaHang.Models
{
    public class GioHang
    {
        NhaHangEntities db = new NhaHangEntities();
        public int iMaSP { get; set; }
        public int iLoaiDat { get; set; }
        public string sTenSp { get; set; }
        public string sHinhAnh { get; set; }

        [Range(1, 15)]
        public int iSoLuong { get; set; }
        public double dDonGia { get; set; }
        public double dThanhTien { get { return iSoLuong * dDonGia; } }
        //TaoGioHang
        public GioHang(int MaSP,int LoaiDat)
        {
            iMaSP = MaSP;
            iLoaiDat = LoaiDat;
            if (LoaiDat == 1)
            {
                MonAn monAn = db.MonAn.Single(n => n.MaMon == iMaSP); // lấy ra 1 đối tượng món ăn từ cơ sở dl sao cho mã món = mã sp
                sTenSp = monAn.TenMon; // gán giá trị
                sHinhAnh = monAn.HinhAnh; // gán gtri
                dDonGia =double.Parse(monAn.Gia.ToString()); // gán gtri
                iSoLuong = 1; // gán giá trị
            }
            else
            {
                GoiCombo goiCombo = db.GoiCombo.Single(n => n.MaCombo == iMaSP);
                sTenSp = goiCombo.TenComBo;
                sHinhAnh = goiCombo.HinhAnh;
                dDonGia = double.Parse(goiCombo.Gia.ToString());
                iSoLuong = 1;
            }

        }
    }
}