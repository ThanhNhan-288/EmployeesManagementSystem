using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TransferObject.DTO;

namespace DataLayer.DAL
{
    public class PhongBanDAL
    {
        private DataProvider dp = new DataProvider();

        public List<PhongBanDTO> LayTatCaPhongBan()
        {
            List<PhongBanDTO> list = new List<PhongBanDTO>();
            string sql = "SELECT MaPhongBan, TenPhongBan FROM PhongBan";
            DataTable dt = dp.ExecuteQuery(sql, CommandType.Text);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PhongBanDTO
                {
                    MaPhongBan = Convert.ToInt32(row["MaPhongBan"]),
                    TenPhongBan = row["TenPhongBan"].ToString()
                });
            }
            return list;
        }

        public bool ThemPhongBan(string tenPhongBan)
        {
            string sql = "INSERT INTO PhongBan (TenPhongBan) VALUES (@TenPhongBan)";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@TenPhongBan", tenPhongBan)
            };
            return dp.ExecuteNonQuery(sql, CommandType.Text, pars.ToArray()) > 0;
        }
    }
}
