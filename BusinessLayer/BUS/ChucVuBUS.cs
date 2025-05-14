using DataLayer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject.DTO;

namespace BusinessLayer.BUS
{
    public class ChucVuBUS
    {
        private ChucVuDAL cvDAL = new ChucVuDAL();

        public List<ChucVuDTO> LayChucVuTheoBoPhan(int maBoPhan)
        {
            if (maBoPhan <= 0)
                return new List<ChucVuDTO>();

            return cvDAL.LayChucVuTheoBoPhan(maBoPhan);
        }

        public bool ThemChucVu(int maBoPhan, string tenChucVu)
        {
            if (maBoPhan <= 0 || string.IsNullOrEmpty(tenChucVu))
                return false;

            return cvDAL.ThemChucVu(maBoPhan, tenChucVu);
        }
    }
}
