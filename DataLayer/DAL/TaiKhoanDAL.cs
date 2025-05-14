using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TransferObject;
using TransferObject.DTO;

namespace DataLayer.DAL
{
    public class TaiKhoanDAL
    {
        private DataProvider dp = new DataProvider();

        public TaiKhoanDTO DangNhap(string tenDangNhap, string matKhau)
        {
            string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            List<SqlParameter> paras = new List<SqlParameter>()
    {
        new SqlParameter("@TenDangNhap", tenDangNhap),
        new SqlParameter("@MatKhau", matKhau)
    };

            DataTable dt = dp.ExecuteAdapter(sql, paras);
            if (dt.Rows.Count > 0)
            {
                TaiKhoanDTO tk = new TaiKhoanDTO();
                tk.TenDangNhap = dt.Rows[0]["TenDangNhap"].ToString();
                tk.MatKhau = dt.Rows[0]["MatKhau"].ToString();
                tk.TenHienThi = dt.Rows[0]["TenHienThi"].ToString();
                return tk;
            }

            return null;
        }
        public bool DangKy(TaiKhoanDTO tk)
        {
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] check = { new SqlParameter("@TenDangNhap", tk.TenDangNhap) };
            object count = dp.ExecuteScalar(sql, CommandType.Text, check);

            if (Convert.ToInt32(count) > 0)
                return false; 

            SqlParameter[] pars =
            {
        new SqlParameter("@TenDangNhap", tk.TenDangNhap),
        new SqlParameter("@MatKhau", tk.MatKhau),
        new SqlParameter("@TenHienThi", tk.TenHienThi)
            };

            string insertSql = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, TenHienThi) VALUES (@TenDangNhap, @MatKhau, @TenHienThi)";
            return dp.ExecuteNonQuery(insertSql, CommandType.Text, pars) > 0;
        }
        public List<TaiKhoanDTO> LayTatCaTaiKhoan()
        {
            List<TaiKhoanDTO> list = new List<TaiKhoanDTO>();

            string sql = "SELECT MaTaiKhoan,TenDangNhap,MatKhau ,TenHienThi FROM TaiKhoan";
            DataTable dt = dp.ExecuteAdapter(sql, null);

            foreach (DataRow row in dt.Rows)
            {
                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    TenHienThi = row["TenHienThi"].ToString(),
                    MatKhau = row["MatKhau"].ToString()
                };

                list.Add(tk);
            }

            return list;
        }
    }

}
