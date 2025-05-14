using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject.DTO;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer.DAL
{
    public class NhanVienDAL
    {
        private DataProvider dp = new DataProvider();
       
        
        public List<NhanVienDTO> LayTatCaNhanVien()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            string sql = "sp_LayTatCaNhanVien";
            dp.Connect();
            SqlCommand cmd = new SqlCommand(sql, dp.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNhanVien = reader["MaNhanVien"].ToString(),
                    HoTen = reader["HoTen"].ToString(),
                    GioiTinh = reader["GioiTinh"].ToString(),
                    NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                    DiaChi = reader["DiaChi"].ToString(),
                    CCCD = reader["CCCD"].ToString(),
                    SoDienThoai = reader["SoDienThoai"].ToString(),
                    HinhAnh = reader["HinhAnh"] == DBNull.Value ? "" : reader["HinhAnh"].ToString(),
                    TenPhongBan = reader["TenPhongBan"].ToString(),
                    TenBoPhan = reader["TenBoPhan"].ToString(),
                    TenChucVu = reader["TenChucVu"].ToString(),

                    // Đảm bảo kiểu int
                    MaPhongBan = Convert.ToInt32(reader["MaPhongBan"]),
                    MaBoPhan = Convert.ToInt32(reader["MaBoPhan"]),
                    MaChucVu = Convert.ToInt32(reader["MaChucVu"])
                };

                list.Add(nv);
            }

            reader.Close();
            dp.Disconnect();
            return list;
        }
        public bool ThemNhanVien(NhanVienDTO nv)
        {
            string query = @"
                INSERT INTO NhanVien ( MaNhanVien,HoTen, GioiTinh, NgaySinh, DiaChi, CCCD, SoDienThoai, MaPhongBan, MaBoPhan, MaChucVu) 
                VALUES (@MaNhanVien, @HoTen, @GioiTinh, @NgaySinh, @DiaChi, @CCCD, @SoDienThoai, @MaPhongBan, @MaBoPhan, @MaChucVu)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {   
                new SqlParameter("@MaNhanVien", nv.MaNhanVien),
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@GioiTinh", nv.GioiTinh),
                new SqlParameter("@NgaySinh", nv.NgaySinh),
                new SqlParameter("@DiaChi", nv.DiaChi),
                new SqlParameter("@CCCD", nv.CCCD),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai),
                new SqlParameter("@MaPhongBan", nv.MaPhongBan),
                new SqlParameter("@MaBoPhan", nv.MaBoPhan),
                new SqlParameter("@MaChucVu", nv.MaChucVu)
            };

            return dp.ExecuteNonQuery(query, CommandType.Text, parameters.ToArray()) > 0;
        }
        public DataTable LayTatCaPhongBan()
        {
            string query = "SELECT MaPhongBan, TenPhongBan FROM PhongBan";
            return dp.ExecuteQuery(query, CommandType.Text);
        }

        public DataTable LayBoPhanTheoPhongBan(int maPhongBan)
        {
            string query = "SELECT MaBoPhan, TenBoPhan FROM BoPhan WHERE MaPhongBan = @MaPhongBan";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@MaPhongBan", maPhongBan)
            };
            return dp.ExecuteQuery(query, CommandType.Text, pars);
        }

        public DataTable LayChucVuTheoBoPhan(int maBoPhan)
        {
            string query = "SELECT MaChucVu, TenChucVu FROM ChucVu WHERE MaBoPhan = @MaBoPhan";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@MaBoPhan", maBoPhan)
            };
            return dp.ExecuteQuery(query, CommandType.Text, pars);
        }

        public bool KiemTraTrungMaNV(string maNV)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                SqlParameter param = new SqlParameter("@MaNhanVien", maNV);
                int count = (int)dp.ExecuteScalar(sql, CommandType.Text, new SqlParameter[] { param });
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SuaNhanVien(NhanVienDTO nv)
        {
            try
            {
                string sql = "UPDATE NhanVien SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, " +
                             "DiaChi = @DiaChi, CCCD = @CCCD, SoDienThoai = @SoDienThoai, " +
                             "MaPhongBan = @MaPhongBan, MaBoPhan = @MaBoPhan, MaChucVu = @MaChucVu " +
                             "WHERE MaNhanVien = @MaNhanVien";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaNhanVien", nv.MaNhanVien),
            new SqlParameter("@HoTen", nv.HoTen),
            new SqlParameter("@GioiTinh", nv.GioiTinh),
            new SqlParameter("@NgaySinh", nv.NgaySinh),
            new SqlParameter("@DiaChi", nv.DiaChi),
            new SqlParameter("@CCCD", nv.CCCD),
            new SqlParameter("@SoDienThoai", nv.SoDienThoai),
            new SqlParameter("@MaPhongBan", nv.MaPhongBan),
            new SqlParameter("@MaBoPhan", nv.MaBoPhan),
            new SqlParameter("@MaChucVu", nv.MaChucVu)
                };

                return dp.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XoaNhanVien(string maNV)
        {
            try
            {
                string sql = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                SqlParameter param = new SqlParameter("@MaNhanVien", maNV);
                return dp.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter[] { param }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }
        public List<NhanVienDTO> TimKiemNhanVien(string keyword,string loaiTimKiem )
        {
            try
            {
                string sql = @"
            SELECT nv.MaNhanVien, nv.HoTen, nv.GioiTinh, nv.NgaySinh, nv.DiaChi, nv.CCCD, nv.SoDienThoai, 
                   pb.TenPhongBan, bp.TenBoPhan, cv.TenChucVu,
                   nv.MaPhongBan, nv.MaBoPhan, nv.MaChucVu
            FROM NhanVien nv
            LEFT JOIN PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
            LEFT JOIN BoPhan bp ON nv.MaBoPhan = bp.MaBoPhan
            LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
            WHERE {0} LIKE @keyword
            ORDER BY nv.MaNhanVien";
                string column = "nv.HoTen"; 

                if (loaiTimKiem == "Mã Nhân Viên")
                    column = "nv.MaNhanVien";
                else if (loaiTimKiem == "Tên Phòng Ban")
                    column = "pb.TenPhongBan";

                sql = string.Format(sql, column);

                SqlParameter param = new SqlParameter("@keyword", "%" + keyword + "%");
                DataTable dt = dp.ExecuteQuery(sql, CommandType.Text, new SqlParameter[] { param });

                List<NhanVienDTO> list = new List<NhanVienDTO>();
                foreach (DataRow row in dt.Rows)
                {
                    NhanVienDTO nv = new NhanVienDTO
                    {
                        MaNhanVien = row["MaNhanVien"].ToString(),
                        HoTen = row["HoTen"].ToString(),
                        GioiTinh = row["GioiTinh"].ToString(),
                        NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                        DiaChi = row["DiaChi"].ToString(),
                        CCCD = row["CCCD"].ToString(),
                        SoDienThoai = row["SoDienThoai"].ToString(),
                        MaPhongBan = Convert.ToInt32(row["MaPhongBan"]),
                        MaBoPhan = Convert.ToInt32(row["MaBoPhan"]),
                        MaChucVu = Convert.ToInt32(row["MaChucVu"]),
                        TenPhongBan = row["TenPhongBan"].ToString(),
                        TenBoPhan = row["TenBoPhan"].ToString(),
                        TenChucVu = row["TenChucVu"].ToString()
                    };
                    list.Add(nv);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm nhân viên: " + ex.Message);
            }
        }
        public bool CapNhatHinhAnh(string maNhanVien, string hinhAnh)
        {
            try
            {
                string sql = "UPDATE NhanVien SET HinhAnh = @HinhAnh WHERE MaNhanVien = @MaNhanVien";
                dp.Connect();
                SqlCommand cmd = new SqlCommand(sql, dp.Connection);
                cmd.Parameters.AddWithValue("@HinhAnh", hinhAnh);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                int rows = cmd.ExecuteNonQuery();
                dp.Disconnect();

                return rows > 0;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        public bool XoaHinhAnh(string maNhanVien)
        {
            try
            {
                string sql = "UPDATE NhanVien SET HinhAnh = NULL WHERE MaNhanVien = @MaNhanVien";
                dp.Connect();
                SqlCommand cmd = new SqlCommand(sql, dp.Connection);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                int rows = cmd.ExecuteNonQuery();
                dp.Disconnect();

                return rows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NhanVienDTO LayThongTinNhanVien(string maNhanVien)
        {
            try
            {
                NhanVienDTO nv = null;
                string sql = "SELECT * FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                dp.Connect();
                SqlCommand cmd = new SqlCommand(sql, dp.Connection);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    nv = new NhanVienDTO
                    {
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        DiaChi = reader["DiaChi"].ToString(),
                        CCCD = reader["CCCD"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        TenPhongBan = reader["TenPhongBan"].ToString(),
                        TenBoPhan = reader["TenBoPhan"].ToString(),
                        TenChucVu = reader["TenChucVu"].ToString(),
                        HinhAnh = reader["HinhAnh"].ToString()
                    };
                }

                reader.Close();
                dp.Disconnect();

                return nv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

