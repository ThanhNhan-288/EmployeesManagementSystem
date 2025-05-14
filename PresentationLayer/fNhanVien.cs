using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;
using BusinessLayer.BUS;
using TransferObject.DTO;
using System.Diagnostics;
namespace PresentationLayer
{
    public partial class fNhanVien : Form
    {
        private NhanVienBUS bus = new NhanVienBUS();
        private List<NhanVienDTO> nhanVienList;
        private NhanVienDTO nhanVienDuocChon;
        public fNhanVien()
        {
            InitializeComponent();
            Load += fNhanVien_Load;
            dgvNhanVien.SelectionChanged += dgvNhanVien_SelectionChanged;

        }
        private void LoadDanhSachNhanVien()
        {
            List<NhanVienDTO> list = bus.LayTatCaNhanVien();
            dgvNhanVien.DataSource = list;
        }

        private void fNhanVien_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
            LoadPhongBan();
            cboLoaiTimKiem.Items.Add("Tên Nhân Viên");
            cboLoaiTimKiem.Items.Add("Mã Nhân Viên");
            cboLoaiTimKiem.Items.Add("Tên Phòng Ban");
            cboLoaiTimKiem.SelectedIndex = 0; 

        }
        private void LoadPhongBan()
        {
            DataTable dt = bus.LayTatCaPhongBan();
            cboPhongBan.DataSource = dt;
            cboPhongBan.DisplayMember = "TenPhongBan";
            cboPhongBan.ValueMember = "MaPhongBan";
            cboPhongBan.SelectedIndex = -1;
        }
        private void LoadBoPhan(int maPhongBan)
        {
            DataTable dt = bus.LayBoPhanTheoPhongBan(maPhongBan);
            if (dt.Rows.Count > 0)
            {
                cboBoPhan.DataSource = dt;
                cboBoPhan.DisplayMember = "TenBoPhan";
                cboBoPhan.ValueMember = "MaBoPhan";
                cboBoPhan.SelectedIndex = -1;
            }
            else
            {
                cboBoPhan.DataSource = null;
                cboChucVu.DataSource = null;
                MessageBox.Show("Không có bộ phận nào cho phòng ban này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadChucVu(int maBoPhan)
        {
            DataTable dt = bus.LayChucVuTheoBoPhan(maBoPhan);
            if (dt.Rows.Count > 0)
            {
                cboChucVu.DataSource = dt;
                cboChucVu.DisplayMember = "TenChucVu";
                cboChucVu.ValueMember = "MaChucVu";
                cboChucVu.SelectedIndex = -1;
            }
            else
            {
                cboChucVu.DataSource = null;
                MessageBox.Show("Không có chức vụ nào cho bộ phận này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhongBan.SelectedValue != null && int.TryParse(cboPhongBan.SelectedValue.ToString(), out int maPhongBan))
            {
                LoadBoPhan(maPhongBan);
            }
        }
        private void cboBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBoPhan.SelectedValue != null && int.TryParse(cboBoPhan.SelectedValue.ToString(), out int maBoPhan))
            {
                LoadChucVu(maBoPhan);
            }
        }
        private void cboChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(cbGioiTinh.Text) ||
                    dtpNgaySinh.Value == null ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(txtCCCD.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    cboPhongBan.SelectedIndex == -1 ||
                    cboBoPhan.SelectedIndex == -1 ||
                    cboChucVu.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string maNV = txtMaNV.Text.Trim();
                NhanVienBUS bus = new NhanVienBUS();
                if (bus.KiemTraTrungMaNV(maNV))
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại! Vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNhanVien = txtMaNV.Text,
                    HoTen = txtHoTen.Text,
                    GioiTinh = cbGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text,
                    CCCD = txtCCCD.Text,
                    SoDienThoai = txtSDT.Text,                  
                    MaPhongBan = (int)cboPhongBan.SelectedValue,
                    MaBoPhan = (int)cboBoPhan.SelectedValue,
                    MaChucVu = (int)cboChucVu.SelectedValue
                };

                if (bus.ThemNhanVien(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();  // Tải lại danh sách nhân viên
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            fMain mainForm = (fMain)Application.OpenForms["fMain"];
            if (mainForm != null)
            {
                mainForm.UpdateDashboardCounts();
            }

        }
        private void ClearForm()
        {
            txtMaNV.Clear();
            cbGioiTinh.SelectedIndex = -1;
            txtHoTen.Clear();
            txtDiaChi.Clear();          
            txtCCCD.Clear();
            txtSDT.Clear();
            cboPhongBan.SelectedIndex = -1;
            cboBoPhan.DataSource = null;
            cboChucVu.DataSource = null;
        }
    

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (e.RowIndex >= 0)
                {
                    // Lấy dòng được chọn
                    DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                    // Điền thông tin vào các TextBox và ComboBox
                    txtMaNV.Text = row.Cells["MaNhanVien"].Value.ToString();
                    txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                    cbGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                    txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
                    txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                    // Chọn Phòng Ban, Bộ Phận, Chức Vụ
                    cboPhongBan.SelectedValue = row.Cells["MaPhongBan"].Value.ToString();
                    cboBoPhan.SelectedValue = row.Cells["MaBoPhan"].Value.ToString();
                    cboChucVu.SelectedValue = row.Cells["MaChucVu"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                    string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(cbGioiTinh.Text) ||
                    dtpNgaySinh.Value == null ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(txtCCCD.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    cboPhongBan.SelectedIndex == -1 ||
                    cboBoPhan.SelectedIndex == -1 ||
                    cboChucVu.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng NhanVienDTO
                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNhanVien = txtMaNV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = cbGioiTinh.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text.Trim(),
                    CCCD = txtCCCD.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    MaPhongBan = (int)cboPhongBan.SelectedValue,
                    MaBoPhan = (int)cboBoPhan.SelectedValue,
                    MaChucVu = (int)cboChucVu.SelectedValue
                };

                // Gọi BUS để sửa nhân viên
                NhanVienBUS bus = new NhanVienBUS();
                if (bus.SuaNhanVien(nv))
                {
                    MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien(); // Cập nhật lại danh sách
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Sửa nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã chọn nhân viên chưa
                if (string.IsNullOrWhiteSpace(txtMaNV.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hỏi lại người dùng có chắc chắn muốn xóa hay không
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                // Lấy mã nhân viên
                string maNV = txtMaNV.Text.Trim();

                // Gọi BUS để xóa
                NhanVienBUS bus = new NhanVienBUS();
                if (bus.XoaNhanVien(maNV))
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien(); // Cập nhật lại danh sách
                    ClearForm(); // Xóa trắng form
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form Nhân Viên và quay lại Main?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                fMain mainForm = (fMain)Application.OpenForms["fMain"];
                if (mainForm != null)
                {
                    mainForm.UpdateDashboardCounts();
                }
                this.Close();
            }
        }


        private void fNhanVien_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "Nhập vào chỗ trống")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập vào chỗ trống";
                txtTimKiem.ForeColor = Color.DimGray;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy từ khóa tìm kiếm
                string keyword = txtTimKiem.Text.Trim();
                string loaiTimKiem = cboLoaiTimKiem.SelectedItem.ToString();

                // Nếu từ khóa rỗng, tải lại danh sách đầy đủ
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadDanhSachNhanVien();
                    return;
                }

                // Gọi BUS để tìm kiếm
                NhanVienBUS bus = new NhanVienBUS();
                List<NhanVienDTO> dsNhanVien = bus.TimKiemNhanVien(keyword, loaiTimKiem);
                dgvNhanVien.DataSource = dsNhanVien;

                // Kiểm tra có kết quả không
                if (dsNhanVien.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào khớp với từ khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                LoadDanhSachNhanVien(); // Tải lại danh sách đầy đủ
            }
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem_Click(sender, e);
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh lỗi "ding"
            }
        }

      

        private void fNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form Nhân Viên và quay lại Main?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                fMain mainForm = (fMain)Application.OpenForms["fMain"];
                if (mainForm != null)
                {
                    mainForm.UpdateDashboardCounts();
                }
                this.Close();
            }
        }

        private void btnXuatDanhSach_Click(object sender, EventArgs e)
        {      
            try
            {
                if (dgvNhanVien.Rows.Count == 0)
                {
                    MessageBox.Show("Không có nhân viên nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo Workbook
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("DanhSachNhanVien");

                    // Tiêu đề bảng
                    worksheet.Cell(1, 1).Value = "Mã Nhân Viên";
                    worksheet.Cell(1, 2).Value = "Họ Tên";
                    worksheet.Cell(1, 3).Value = "Giới Tính";
                    worksheet.Cell(1, 4).Value = "Ngày Sinh";
                    worksheet.Cell(1, 5).Value = "Địa Chỉ";
                    worksheet.Cell(1, 6).Value = "CCCD";
                    worksheet.Cell(1, 7).Value = "Số Điện Thoại";
                    worksheet.Cell(1, 8).Value = "Phòng Ban";
                    worksheet.Cell(1, 9).Value = "Bộ Phận";
                    worksheet.Cell(1, 10).Value = "Chức Vụ";

                    // Định dạng tiêu đề
                    var headerRange = worksheet.Range("A1:J1");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Điền dữ liệu từ DataGridView
                    int rowIndex = 2;
                    foreach (DataGridViewRow row in dgvNhanVien.Rows)
                    {
                        worksheet.Cell(rowIndex, 1).Value = row.Cells["MaNhanVien"].Value.ToString();
                        worksheet.Cell(rowIndex, 2).Value = row.Cells["HoTen"].Value.ToString();
                        worksheet.Cell(rowIndex, 3).Value = row.Cells["GioiTinh"].Value.ToString();
                        worksheet.Cell(rowIndex, 4).Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value).ToString("dd/MM/yyyy");
                        worksheet.Cell(rowIndex, 5).Value = row.Cells["DiaChi"].Value.ToString();
                        worksheet.Cell(rowIndex, 6).Value = row.Cells["CCCD"].Value.ToString();
                        worksheet.Cell(rowIndex, 7).Value = row.Cells["SoDienThoai"].Value.ToString();
                        worksheet.Cell(rowIndex, 8).Value = row.Cells["TenPhongBan"].Value.ToString();
                        worksheet.Cell(rowIndex, 9).Value = row.Cells["TenBoPhan"].Value.ToString();
                        worksheet.Cell(rowIndex, 10).Value = row.Cells["TenChucVu"].Value.ToString();
                        rowIndex++;
                    }

                    // Định dạng cột
                    worksheet.Columns().AdjustToContents();

                    // Lưu file
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx",
                        Title = "Lưu File Excel"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        workbook.SaveAs(filePath);
                        MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();

        }

       

        private void btnInNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có nhân viên nào được chọn hay chưa
                if (dgvNhanVien.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để in chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy nhân viên được chọn
                NhanVienDTO nv = (NhanVienDTO)dgvNhanVien.SelectedRows[0].DataBoundItem;

                // Tạo tài liệu in
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (s, ev) =>
                {
                    int x = 50;
                    int y = 100;
                    int lineHeight = 40;
                    Font font = new Font("Segoe UI", 12);
                    Font headerFont = new Font("Segoe UI", 18, FontStyle.Bold);

                    // In tiêu đề
                    ev.Graphics.DrawString("Thông Tin Chi Tiết Nhân Viên", headerFont, Brushes.Black, new PointF(x, y));
                    y += lineHeight * 2;

                    // In thông tin chi tiết
                    ev.Graphics.DrawString($"Mã Nhân Viên: {nv.MaNhanVien}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Họ và Tên: {nv.HoTen}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Giới Tính: {nv.GioiTinh}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Ngày Sinh: {nv.NgaySinh:dd/MM/yyyy}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Địa Chỉ: {nv.DiaChi}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"CCCD: {nv.CCCD}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Số Điện Thoại: {nv.SoDienThoai}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Phòng Ban: {nv.TenPhongBan}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Bộ Phận: {nv.TenBoPhan}", font, Brushes.Black, new PointF(x, y)); y += lineHeight;
                    ev.Graphics.DrawString($"Chức Vụ: {nv.TenChucVu}", font, Brushes.Black, new PointF(x, y)); y += lineHeight * 2;

                    // In hình ảnh nếu có
                    if (!string.IsNullOrEmpty(nv.HinhAnh) && File.Exists(nv.HinhAnh))
                    {
                        try
                        {
                            Image img = Image.FromFile(nv.HinhAnh);

                            // Tạo khung hình ảnh với kích thước cố định
                            int imgWidth = 150;
                            int imgHeight = 150;
                            int imgX = x + 400;
                            int imgY = 100;

                            // Căn giữa hình ảnh theo khung
                            float aspectRatio = (float)img.Width / img.Height;
                            if (aspectRatio > 1)
                            {
                                imgHeight = (int)(imgWidth / aspectRatio);
                            }
                            else
                            {
                                imgWidth = (int)(imgHeight * aspectRatio);
                            }

                            // Vẽ khung nền
                            ev.Graphics.FillRectangle(Brushes.LightGray, imgX, imgY, 150, 150);
                            ev.Graphics.DrawImage(img, imgX + (150 - imgWidth) / 2, imgY + (150 - imgHeight) / 2, imgWidth, imgHeight);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };

                // Hiển thị xem trước khi in
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in chi tiết nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanVien.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên trước khi chọn ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy nhân viên được chọn
                NhanVienDTO nv = (NhanVienDTO)dgvNhanVien.SelectedRows[0].DataBoundItem;

                // Mở hộp thoại chọn ảnh
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                    Title = "Chọn ảnh nhân viên"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Cập nhật hình ảnh cho nhân viên
                    if (bus.CapNhatHinhAnh(nv.MaNhanVien, filePath))
                    {
                        // Hiển thị ảnh trong PictureBox
                        picHinhAnh.Image = Image.FromFile(filePath);
                        MessageBox.Show("Cập nhật hình ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật danh sách nhân viên
                        LoadDanhSachNhanVien();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật hình ảnh thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                nhanVienDuocChon = (NhanVienDTO)dgvNhanVien.SelectedRows[0].DataBoundItem;

                // Hiển thị ảnh nếu có
                if (!string.IsNullOrEmpty(nhanVienDuocChon.HinhAnh) && File.Exists(nhanVienDuocChon.HinhAnh))
                {
                    picHinhAnh.Image = Image.FromFile(nhanVienDuocChon.HinhAnh);
                }
                else
                {
                    picHinhAnh.Image = null;
                }
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            try
            {
                if (nhanVienDuocChon == null)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên trước khi xóa ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa ảnh của nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (bus.XoaHinhAnh(nhanVienDuocChon.MaNhanVien))
                    {
                        picHinhAnh.Image = null;
                        MessageBox.Show("Xóa ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachNhanVien();
                    }
                    else
                    {
                        MessageBox.Show("Xóa ảnh thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fNhanVien_Load_1(object sender, EventArgs e)
        {
            toolTip = new ToolTip();
            toolTip.InitialDelay = 100;   
            toolTip.ReshowDelay = 100;     
            toolTip.AutoPopDelay = 5000;  
            toolTip.ShowAlways = true;    
            toolTip.SetToolTip(btnInNhanVien, "In  nhân viên");
            toolTip.SetToolTip(btnThem, "Thêm nhân viên mới");
            toolTip.SetToolTip(btnSua, "Chỉnh sửa thông tin nhân viên");
            toolTip.SetToolTip(btnXoa, "Xóa nhân viên");
            toolTip.SetToolTip(btnClear, "Làm Mới");
            toolTip.SetToolTip(btnXuatDanhSach, "Xuất Danh Sách nhân viên");
            toolTip.SetToolTip(btnTimKiem, "Tìm Kiếm"); 
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboLoaiTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }


        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
