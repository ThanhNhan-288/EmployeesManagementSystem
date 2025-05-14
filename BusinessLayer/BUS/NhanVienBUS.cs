using DataLayer.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject.DTO;
namespace BusinessLayer.BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAL nvdal = new NhanVienDAL();

        public List<NhanVienDTO> LayTatCaNhanVien()
        {
            return nvdal.LayTatCaNhanVien();
        }
        public DataTable LayTatCaPhongBan()
        {
            return nvdal.LayTatCaPhongBan();
        }
        
        public DataTable LayBoPhanTheoPhongBan(int maPhongBan)
        {
            return nvdal.LayBoPhanTheoPhongBan(maPhongBan);
        }

        public DataTable LayChucVuTheoBoPhan(int maBoPhan)
        {
            return nvdal.LayChucVuTheoBoPhan(maBoPhan);
        }
        public bool ThemNhanVien(NhanVienDTO nv)
        {
            return nvdal.ThemNhanVien(nv);
        }
        public bool KiemTraTrungMaNV(string maNV)
        {
            return nvdal.KiemTraTrungMaNV(maNV);
        }
        public bool SuaNhanVien(NhanVienDTO nv)
        {
            return nvdal.SuaNhanVien(nv);
        }
        public bool XoaNhanVien(string maNV)
        {
            return nvdal.XoaNhanVien(maNV);
        }
        public List<NhanVienDTO> TimKiemNhanVien(string keyword, string loaiTimKiem)
        {
            return nvdal.TimKiemNhanVien(keyword , loaiTimKiem);
        }
        public bool CapNhatHinhAnh(string maNhanVien, string hinhAnh)
        {
            
                return nvdal.CapNhatHinhAnh(maNhanVien, hinhAnh);                  
        }
        public bool XoaHinhAnh(string maNhanVien)
        {

            return nvdal.XoaHinhAnh(maNhanVien);
        }
        public NhanVienDTO LayThongTinNhanVien(string maNhanVien)
        {
            
                return nvdal.LayThongTinNhanVien(maNhanVien);                  
        }

    }
}
