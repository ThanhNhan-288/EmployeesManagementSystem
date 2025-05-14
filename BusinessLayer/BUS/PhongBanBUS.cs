using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DAL;
using TransferObject.DTO;

namespace BusinessLayer.BUS
{
    public class PhongBanBUS
    {
        private PhongBanDAL pbDAL = new PhongBanDAL();

        public List<PhongBanDTO> LayTatCaPhongBan()
        {
            return pbDAL.LayTatCaPhongBan();
        }

        public bool ThemPhongBan(string tenPhongBan)
        {
            // Kiểm tra tên phòng ban có rỗng không
            if (string.IsNullOrEmpty(tenPhongBan))
                return false;

            // Thực hiện thêm phòng ban
            return pbDAL.ThemPhongBan(tenPhongBan);
        }
    }
}
