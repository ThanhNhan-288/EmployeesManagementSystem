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
using PresentationLayer;

namespace PresentationLayer
{
    public partial class fMain : Form
    {
        private string tenHienTHi;
        public fMain(string tenHienTHi)
        {
            InitializeComponent();
            this.tenHienTHi = tenHienTHi;
        }


        private void BáoCáoNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnu_XuatBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void fMain_Load(object sender, EventArgs e)
        {
            ThongKeBUS bus = new ThongKeBUS();

            lblSoNhanVien.Text = bus.DemNhanVien().ToString();
            lblSoPhongBan.Text = bus.DemPhongBan().ToString();
            lblXinChao.Text = $"Xin Chào, {tenHienTHi}";
        }

        private void main_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            fNhanVien f = new fNhanVien();
            f.ShowDialog();
        }

        public void UpdateDashboardCounts()
        {
            try
            {
                // Đếm tổng số nhân viên
                NhanVienBUS nvBUS = new NhanVienBUS();
                int totalNhanVien = nvBUS.LayTatCaNhanVien().Count;
                lblSoNhanVien.Text = totalNhanVien.ToString();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật số liệu: " + ex.Message);
            }
        }

        private void fMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        private void mnuPhongBan_Click(object sender, EventArgs e)
        {
            this.Hide();
            fPhongBan f = new fPhongBan();
            f.ShowDialog();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            // Hiển thị form đăng nhập
            fDangNhap dn = new fDangNhap();
            this.Hide();
            dn.ShowDialog();
            this.Close();   
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            this.Hide();
            fDanhSachTaiKhoan f = new fDanhSachTaiKhoan();
            f.ShowDialog();
        }

        private void lươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            fLuong f = new fLuong();
            f.ShowDialog();
            
        }
    }
}
