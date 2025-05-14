using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject.DTO
{
    public class NhanVienDTO
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string TenPhongBan { get; set; }
        public string TenBoPhan { get; set; }
        public string TenChucVu { get; set; }
        public int MaPhongBan { get; set; }
        public int MaBoPhan { get; set; }
        public int MaChucVu { get; set; }
        public string HinhAnh { get; set; }
    }
}
