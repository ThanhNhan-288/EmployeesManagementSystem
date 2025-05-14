using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject.DTO;

namespace DataLayer.DAL
{
    public class ChucVuDAL
    {
        private DataProvider dp = new DataProvider();

        public List<ChucVuDTO> LayChucVuTheoBoPhan(int maBoPhan)
        {
            List<ChucVuDTO> list = new List<ChucVuDTO>();
            string sql = "SELECT MaChucVu, TenChucVu FROM ChucVu WHERE MaBoPhan = @MaBoPhan";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaBoPhan", maBoPhan)
            };
            DataTable dt = dp.ExecuteQuery(sql, CommandType.Text, pars.ToArray());

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChucVuDTO
                {
                    MaChucVu = Convert.ToInt32(row["MaChucVu"]),
                    TenChucVu = row["TenChucVu"].ToString(),
                    MaBoPhan = maBoPhan
                });
            }
            return list;
        }

        public bool ThemChucVu(int maBoPhan, string tenChucVu)
        {
            string sql = "INSERT INTO ChucVu (MaBoPhan, TenChucVu) VALUES (@MaBoPhan, @TenChucVu)";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@MaBoPhan", maBoPhan),
                new SqlParameter("@TenChucVu", tenChucVu)
            };
            return dp.ExecuteNonQuery(sql, CommandType.Text, pars.ToArray()) > 0;
        }
    }
}
