using DataLayer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject.DTO;

namespace BusinessLayer.BUS
{
    public class LuongBUS
    {
        private LuongDAL luongDAL = new LuongDAL();

        public List<LuongDTO> LayDanhSachLuongTheoThang(DateTime thangNam)
        {
            return luongDAL.LayDanhSachLuongTheoThang(thangNam);
        }

        public bool CapNhatLuong(LuongDTO luong)
        {
            return luongDAL.CapNhatLuong(luong);
        }
        public bool XoaLuong(string maNhanVien, DateTime thangNam)
        {
            return luongDAL.XoaLuong(maNhanVien, thangNam);
        }
        //public List<dynamic> LayDanhSachLuongBaoCao(DateTime thangNam)
        //{
        //    return luongDAL.LayDanhSachLuongBaoCao(thangNam);
        //}
        public DataTable LayDanhSachLuongBaoCao(DateTime thangNam)
    {
        return luongDAL.LayDanhSachLuongBaoCao(thangNam);
    }
    }
}
