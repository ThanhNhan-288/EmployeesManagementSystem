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
using TransferObject.DTO;

namespace PresentationLayer
{
    public partial class fDangKi : Form
    {
        public fDangKi()
        {
            InitializeComponent();
        }

        private void btn_DK_DangNhap_Click(object sender, EventArgs e)
        {
            fDangNhap dn = new fDangNhap();          
            dn.Show();
            this.Hide();
        }

        private void btn_DK_DangKi_Click(object sender, EventArgs e)
        {
            if (txt_DK_MatKhau.Text != txt_DK_MatKhau.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không đúng!");
                return;
            }

            TaiKhoanDTO tk = new TaiKhoanDTO
            {
                TenDangNhap = txt_DK_TenTaiKhoan.Text,
                MatKhau = txt_DK_MatKhau.Text,
                TenHienThi = txt_DK_TenHienThi.Text 
            };

            TaiKhoanBUS bus = new TaiKhoanBUS();
            bool result = bus.DangKy(tk);

            if (result)
            {
                MessageBox.Show("Đăng ký thành công!");
               
            }
            else
            {
                MessageBox.Show("Tài khoản đã tồn tại!");
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txt_DK_MatKhau.PasswordChar = ShowPass.Checked ? '\0' :'*';
        }
    }
}
