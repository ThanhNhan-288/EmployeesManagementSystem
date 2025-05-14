using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer.BUS;
using DataLayer.DAL;
using TransferObject.DTO;
namespace PresentationLayer
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txt_DN_TenTaiKhoan.Text;
            string pass = txt_DN_MatKhau.Text;

            TaiKhoanBUS bus = new TaiKhoanBUS();
            TaiKhoanDTO tk = bus.DangNhap(user, pass);

            if (tk != null)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fMain main = new fMain(tk.TenHienThi);
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txt_DN_MatKhau.PasswordChar = ShowPass.Checked ? '\0' : '*';
        }

        private void btn_DN_DangKi_Click(object sender, EventArgs e)
        {
            fDangKi dk = new fDangKi();
            dk.Show();
            this.Hide();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fDangNhap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        private void txt_DN_MatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
                e.SuppressKeyPress = true; // Ngăn chặn âm thanh lỗi "ding"
            }
        }
    }
}
