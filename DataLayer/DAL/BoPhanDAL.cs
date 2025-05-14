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
    public class BoPhanDAL
    {
        private DataProvider dp = new DataProvider();

        public List<BoPhanDTO> LayBoPhanTheoPhongBan(int maPhongBan)
        {
            List<BoPhanDTO> list = new List<BoPhanDTO>();
            string sql = "SELECT MaBoPhan, TenBoPhan FROM BoPhan WHERE MaPhongBan = @MaPhongBan";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaPhongBan", maPhongBan)
            };
            DataTable dt = dp.ExecuteQuery(sql, CommandType.Text, pars.ToArray());

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new BoPhanDTO
                {
                    MaBoPhan = Convert.ToInt32(row["MaBoPhan"]),
                    TenBoPhan = row["TenBoPhan"].ToString(),
                    MaPhongBan = maPhongBan
                });
            }
            return list;
        }

        public bool ThemBoPhan(int maPhongBan, string tenBoPhan)
        {
            string sql = "INSERT INTO BoPhan (MaPhongBan, TenBoPhan) VALUES (@MaPhongBan, @TenBoPhan)";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaPhongBan", maPhongBan),
                new SqlParameter("@TenBoPhan", tenBoPhan)
            };
            return dp.ExecuteNonQuery(sql, CommandType.Text, pars.ToArray()) > 0;
        }
    }
}
