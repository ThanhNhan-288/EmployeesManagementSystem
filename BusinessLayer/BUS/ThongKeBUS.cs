using DataLayer.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BUS
{
    public class ThongKeBUS
    {
        private DataProvider dp = new DataProvider();

        public int DemNhanVien()
        {
            string sql = "SELECT COUNT(*) FROM NhanVien";
            object result = dp.ExecuteScalar(sql, CommandType.Text, null);
            return Convert.ToInt32(result);
        }

        public int DemPhongBan()
        {
            string sql = "SELECT COUNT(*) FROM PhongBan";
            object result = dp.ExecuteScalar(sql, CommandType.Text, null);
            return Convert.ToInt32(result);
        }
    }
}
