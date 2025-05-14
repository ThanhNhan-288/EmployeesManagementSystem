using BusinessLayer.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using TransferObject.DTO;
using System.Windows.Media;
using ClosedXML.Excel;
using System.Diagnostics;

namespace PresentationLayer
{
    public partial class fLuong : Form
    {
        private LuongBUS luongBUS = new LuongBUS();
        public fLuong()
        {
            InitializeComponent();
            dtpThangNam.CustomFormat = "MM/yyyy";
            dtpThangNam.Format = DateTimePickerFormat.Custom;
            LoadDanhSachLuong();

        }
        private void LoadDanhSachLuong()
        {
            DateTime thangNam = new DateTime(dtpThangNam.Value.Year, dtpThangNam.Value.Month, 1);
            List<LuongDTO> nhanVienList = luongBUS.LayDanhSachLuongTheoThang(thangNam);
            dgvLuong.DataSource = nhanVienList;

            // Cấu hình cột
            dgvLuong.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
            dgvLuong.Columns["HoTen"].HeaderText = "Tên Nhân Viên";
            dgvLuong.Columns["TenChucVu"].HeaderText = "Chức Vụ";
            dgvLuong.Columns["ThangNam"].Visible = true;
            dgvLuong.Columns["LuongCoBan"].HeaderText = "Lương Cơ Bản";
            dgvLuong.Columns["PhuCap"].HeaderText = "Phụ Cấp";
            dgvLuong.Columns["Thuong"].HeaderText = "Thưởng";
            dgvLuong.Columns["KhauTru"].HeaderText = "Khấu Trừ";
            dgvLuong.Columns["LuongThucNhan"].HeaderText = "Lương Thực Nhận";
            dgvLuong.Columns["LuongThucNhan"].DefaultCellStyle.Format = "N0";
        }   

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }
        private void ClearInputs()
        {
            txtMaNhanVien.Clear();
            txtLuongCoBan.Clear();
            txtPhuCap.Clear();
            txtThuong.Clear();
            txtKhauTru.Clear();
            lblLuongThucNhan.Text = "0";
        }

        private void dgvLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLuong.Rows[e.RowIndex];
                txtMaNhanVien.Text = row.Cells["MaNhanVien"].Value.ToString();
                // Điền dữ liệu lương
                txtLuongCoBan.Text = row.Cells["LuongCoBan"].Value.ToString();
                txtPhuCap.Text = row.Cells["PhuCap"].Value.ToString();
                txtThuong.Text = row.Cells["Thuong"].Value.ToString();
                txtKhauTru.Text = row.Cells["KhauTru"].Value.ToString();
                lblLuongThucNhan.Text = string.Format("{0:N0}", row.Cells["LuongThucNhan"].Value);
                // Cập nhật ThangNam cho DateTimePicker
                dtpThangNam.Value = Convert.ToDateTime(row.Cells["ThangNam"].Value);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                LuongDTO luong = new LuongDTO
                {
                    MaNhanVien = txtMaNhanVien.Text,
                    ThangNam = new DateTime(dtpThangNam.Value.Year, dtpThangNam.Value.Month, 1),
                    LuongCoBan = decimal.TryParse(txtLuongCoBan.Text, out decimal luongCoBan) ? luongCoBan : 0,
                    PhuCap = decimal.TryParse(txtPhuCap.Text, out decimal phuCap) ? phuCap : 0,
                    Thuong = decimal.TryParse(txtThuong.Text, out decimal thuong) ? thuong : 0,
                    KhauTru = decimal.TryParse(txtKhauTru.Text, out decimal khauTru) ? khauTru : 0
                };

                if (luongBUS.CapNhatLuong(luong))
                {
                    MessageBox.Show("Cập nhật lương thành công!");
                    LoadDanhSachLuong();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật lương thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maNhanVien = txtMaNhanVien.Text;
                DateTime thangNam = new DateTime(dtpThangNam.Value.Year, dtpThangNam.Value.Month, 1);

                if (luongBUS.XoaLuong(maNhanVien, thangNam))
                {
                    MessageBox.Show("Xóa lương thành công!");
                    LoadDanhSachLuong();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa lương thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dtpThangNam_ValueChanged(object sender, EventArgs e)
        {
            LoadDanhSachLuong();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadDanhSachLuong();
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime thangNam = new DateTime(dtpThangNam.Value.Year, dtpThangNam.Value.Month, 1);
                DataTable nhanVienTable = luongBUS.LayDanhSachLuongBaoCao(thangNam);

                if (nhanVienTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "C:\\Users\\mguye\\source\\repos\\Solution1EmployeeManagementSystemSolution\\BaoCao";
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.FileName = $"BaoCaoLuong_{thangNam:MM_yyyy}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("BaoCaoLuong");

                        // Tạo header
                        string[] headers = { "Mã Nhân Viên", "Tên Nhân Viên", "Giới Tính", "Chức Vụ", "Lương Cơ Bản", "Phụ Cấp", "Thưởng", "Khấu Trừ", "Lương Thực Nhận", "Tháng Năm" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                            worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }

                        // Điền dữ liệu từ DataTable
                        int row = 2;
                        foreach (DataRow dr in nhanVienTable.Rows)
                        {
                            worksheet.Cell(row, 1).Value = dr["MaNhanVien"].ToString();
                            worksheet.Cell(row, 2).Value = dr["HoTen"].ToString();
                            worksheet.Cell(row, 3).Value = dr["GioiTinh"].ToString();
                            worksheet.Cell(row, 4).Value = dr["TenChucVu"].ToString();
                            worksheet.Cell(row, 5).Value = Convert.ToDecimal(dr["LuongCoBan"]);
                            worksheet.Cell(row, 6).Value = Convert.ToDecimal(dr["PhuCap"]);
                            worksheet.Cell(row, 7).Value = Convert.ToDecimal(dr["Thuong"]);
                            worksheet.Cell(row, 8).Value = Convert.ToDecimal(dr["KhauTru"]);
                            worksheet.Cell(row, 9).Value = Convert.ToDecimal(dr["LuongThucNhan"]);
                            worksheet.Cell(row, 10).Value = dr["ThangNam"].ToString();
                            row++;
                        }

                        // Tự động căn chỉnh cột
                        worksheet.Columns().AdjustToContents();
                        // Lưu file                     
                        workbook.SaveAs(filePath);
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                        MessageBox.Show("Xuất báo cáo Excel thành công!\nFile đã lưu tại: " + filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void fLuong_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["fMain"].Show();
        }
    }
    
    
}
