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
    public class LuongDAL
    {
        private DataProvider dp = new DataProvider();

        public List<LuongDTO> LayDanhSachLuongTheoThang(DateTime thangNam)
        {
            string sql = @"
            SELECT 
                nv.MaNhanVien,
                nv.HoTen,
                nv.GioiTinh,
                cv.TenChucVu,
                ISNULL(lv.LuongCoBan, 0) AS LuongCoBan,
                ISNULL(lv.PhuCap, 0) AS PhuCap,
                ISNULL(lv.Thuong, 0) AS Thuong,
                ISNULL(lv.KhauTru, 0) AS KhauTru,
                ISNULL(lv.LuongCoBan + lv.PhuCap + lv.Thuong - lv.KhauTru, 0) AS LuongThucNhan,
                @ThangNam AS ThangNam
            FROM NhanVien nv
            LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
            LEFT JOIN LuongNhanVien lv 
                ON nv.MaNhanVien = lv.MaNhanVien 
                AND lv.ThangNam = @ThangNam
            GROUP BY 
                nv.MaNhanVien, 
                nv.HoTen, 
                nv.GioiTinh, 
                cv.TenChucVu, 
                lv.LuongCoBan, 
                lv.PhuCap, 
                lv.Thuong, 
                lv.KhauTru
            ORDER BY nv.MaNhanVien;
        ";

            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@ThangNam", thangNam)
        };

            DataTable dt = dp.ExecuteAdapter(sql, parameters);
            List<LuongDTO> nhanVienList = new List<LuongDTO>();

            foreach (DataRow row in dt.Rows)
            {
                LuongDTO luong = new LuongDTO
                {
                    MaNhanVien = row["MaNhanVien"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    TenChucVu = row["TenChucVu"].ToString(),
                    ThangNam = thangNam,
                    LuongCoBan = Convert.ToDecimal(row["LuongCoBan"]),
                    PhuCap = Convert.ToDecimal(row["PhuCap"]),
                    Thuong = Convert.ToDecimal(row["Thuong"]),
                    KhauTru = Convert.ToDecimal(row["KhauTru"])
                };
                nhanVienList.Add(luong);
            }

            return nhanVienList;
        }
        public bool CapNhatLuong(LuongDTO luong)
        {
            string sql = @"
            IF EXISTS (SELECT 1 FROM LuongNhanVien WHERE MaNhanVien = @MaNhanVien AND ThangNam = @ThangNam)
            BEGIN
                UPDATE LuongNhanVien
                SET LuongCoBan = @LuongCoBan,
                    PhuCap = @PhuCap,
                    Thuong = @Thuong,
                    KhauTru = @KhauTru
                WHERE MaNhanVien = @MaNhanVien AND ThangNam = @ThangNam
            END
            ELSE
            BEGIN
                INSERT INTO LuongNhanVien (MaNhanVien, ThangNam, LuongCoBan, PhuCap, Thuong, KhauTru)
                VALUES (@MaNhanVien, @ThangNam, @LuongCoBan, @PhuCap, @Thuong, @KhauTru)
            END
        ";

            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@MaNhanVien", luong.MaNhanVien),
            new SqlParameter("@ThangNam", luong.ThangNam),
            new SqlParameter("@LuongCoBan", luong.LuongCoBan),
            new SqlParameter("@PhuCap", luong.PhuCap),
            new SqlParameter("@Thuong", luong.Thuong),
            new SqlParameter("@KhauTru", luong.KhauTru)
        };

            return dp.IExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
        }
        public bool XoaLuong(string maNhanVien, DateTime thangNam)
        {
            string sql = "DELETE FROM LuongNhanVien WHERE MaNhanVien = @MaNhanVien AND ThangNam = @ThangNam";
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@MaNhanVien", maNhanVien),
            new SqlParameter("@ThangNam", thangNam)
        };

            return dp.IExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
        }
        public DataTable LayDanhSachLuongBaoCao(DateTime thangNam)
        {
            try
            {
                string sql = @"
                SELECT 
                    nv.MaNhanVien,
                    nv.HoTen,
                    nv.GioiTinh,
                    cv.TenChucVu,
                    ISNULL(lv.LuongCoBan, 0) AS LuongCoBan,
                    ISNULL(lv.PhuCap, 0) AS PhuCap,
                    ISNULL(lv.Thuong, 0) AS Thuong,
                    ISNULL(lv.KhauTru, 0) AS KhauTru,
                    ISNULL(lv.LuongCoBan + lv.PhuCap + lv.Thuong - lv.KhauTru, 0) AS LuongThucNhan,
                    @ThangNam AS ThangNam
                FROM NhanVien nv
                LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
                LEFT JOIN LuongNhanVien lv 
                    ON nv.MaNhanVien = lv.MaNhanVien 
                    AND lv.ThangNam = @ThangNam
                ORDER BY nv.MaNhanVien;
            ";

                List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ThangNam", thangNam)
            };

                DataTable dt = dp.ExecuteAdapter(sql, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);
            }
        }
    }
      
    
}
