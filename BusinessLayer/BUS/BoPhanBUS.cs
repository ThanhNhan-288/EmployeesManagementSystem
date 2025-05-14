using DataLayer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject.DTO;

namespace BusinessLayer.BUS
{
    public class BoPhanBUS
    {
        private BoPhanDAL bpDAL = new BoPhanDAL();

        public List<BoPhanDTO> LayBoPhanTheoPhongBan(int maPhongBan)
        {
            if (maPhongBan <= 0)
                return new List<BoPhanDTO>();

            return bpDAL.LayBoPhanTheoPhongBan(maPhongBan);
        }

        public bool ThemBoPhan(int maPhongBan, string tenBoPhan)
        {
            if (maPhongBan <= 0 || string.IsNullOrEmpty(tenBoPhan))
                return false;

            return bpDAL.ThemBoPhan(maPhongBan, tenBoPhan);
        }
    }
}
