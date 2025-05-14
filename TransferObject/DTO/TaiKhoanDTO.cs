using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TransferObject.DTO
{
    public class TaiKhoanDTO
    {
        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string TenHienThi { get; set; }

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string tenDangNhap, string matKhau, string tenHienThi)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            TenHienThi = tenHienThi;
        }
    }
}
