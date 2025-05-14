using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject.DTO
{
    public class LuongDTO
    {
        public string MaNhanVien { get; set; }
        public string HoTen {  get; set; }
        public string TenChucVu { get; set; }
        public DateTime ThangNam { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal PhuCap { get; set; }
        public decimal Thuong { get; set; }
        public decimal KhauTru { get; set; }
        public decimal LuongThucNhan
        {
            get { return LuongCoBan + PhuCap + Thuong - KhauTru; }
        }

        public LuongDTO() { }

        public LuongDTO(string maNhanVien, DateTime thangNam, decimal luongCoBan, decimal phuCap, decimal thuong, decimal khauTru)
        {
            MaNhanVien = maNhanVien;
            ThangNam = thangNam;
            LuongCoBan = luongCoBan;
            PhuCap = phuCap;
            Thuong = thuong;
            KhauTru = khauTru;
            HoTen = HoTen;
            TenChucVu = TenChucVu;
        }
    }
}
