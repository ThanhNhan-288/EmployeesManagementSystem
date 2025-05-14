using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
using DataLayer.DAL;
using TransferObject.DTO;
namespace BusinessLayer.BUS
{
    public class TaiKhoanBUS
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();

        public TaiKhoanDTO DangNhap(string tenDangNhap, string matKhau)
        {
            return dal.DangNhap(tenDangNhap, matKhau);
        }

        public bool DangKy(TaiKhoanDTO tk)
        {
            return dal.DangKy(tk);
        }
        public List<TaiKhoanDTO> LayTatCaTaiKhoan()
        {
            return dal.LayTatCaTaiKhoan();
        }
    }
}
