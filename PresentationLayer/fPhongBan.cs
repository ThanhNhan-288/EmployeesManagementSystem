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
    public partial class fPhongBan : Form
    {
        private PhongBanBUS pbBUS = new PhongBanBUS();
        private BoPhanBUS bpBUS = new BoPhanBUS();
        private ChucVuBUS cvBUS = new ChucVuBUS();
        public fPhongBan()
        {
            InitializeComponent();
            LoadPhongBan();
            LoadBoPhan();
            LoadChucVu();
        }

        private void LoadPhongBan()
        {
            List<PhongBanDTO> phongBanList = pbBUS.LayTatCaPhongBan();
            cboPhongBan.DataSource = phongBanList;
            cboPhongBan.DisplayMember = "TenPhongBan";
            cboPhongBan.ValueMember = "MaPhongBan";
            cboPhongBan.SelectedIndex = -1;

            dgvPhongBan.DataSource = phongBanList;
        }

        private void LoadBoPhan()
        {
            if (cboPhongBan.SelectedValue != null)
            {
                PhongBanDTO selectedPhongBan = (PhongBanDTO)cboPhongBan.SelectedItem;
                int maPhongBan = selectedPhongBan.MaPhongBan;
                List<BoPhanDTO> boPhanList = bpBUS.LayBoPhanTheoPhongBan(maPhongBan);
                cboBoPhan.DataSource = boPhanList;
                cboBoPhan.DisplayMember = "TenBoPhan";
                cboBoPhan.ValueMember = "MaBoPhan";
                cboBoPhan.SelectedIndex = -1;

                dgvBoPhan.DataSource = boPhanList;
            }
        }

        private void LoadChucVu()
        {
            BoPhanDTO selectedBoPhan = cboBoPhan.SelectedItem as BoPhanDTO;
            if (cboBoPhan.SelectedValue != null)
            {
                int maBoPhan = selectedBoPhan.MaBoPhan;
                List<ChucVuDTO> chucVuList = cvBUS.LayChucVuTheoBoPhan(maBoPhan);
                dgvChucVu.DataSource = chucVuList;
            }
        }
        private void btnThemPhongBan_Click(object sender, EventArgs e)
        {
            string tenPhongBan = txtTenPhongBan.Text.Trim();
            if (!string.IsNullOrEmpty(tenPhongBan))
            {
                if (pbBUS.ThemPhongBan(tenPhongBan))
                {
                    MessageBox.Show("Thêm Phòng Ban thành công!");
                    LoadPhongBan();
                    txtTenPhongBan.Clear();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm Phòng Ban!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên Phòng Ban!");
            }
        }

        private void btnThemBoPhan_Click(object sender, EventArgs e)
        {
            if (cboPhongBan.SelectedValue != null)
            {
                int maPhongBan = (int)cboPhongBan.SelectedValue;
                string tenBoPhan = txtTenBoPhan.Text.Trim();
                if (!string.IsNullOrEmpty(tenBoPhan))
                {
                    if (bpBUS.ThemBoPhan(maPhongBan, tenBoPhan))
                    {
                        MessageBox.Show("Thêm Bộ Phận thành công!");
                        LoadBoPhan();
                        txtTenBoPhan.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thêm Bộ Phận!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập tên Bộ Phận!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Phòng Ban!");
            }
        }

        private void btnThemChucVu_Click(object sender, EventArgs e)
        {
            if (cboBoPhan.SelectedValue != null)
            {
                int maBoPhan = (int)cboBoPhan.SelectedValue;
                string tenChucVu = txtTenChucVu.Text.Trim();
                if (!string.IsNullOrEmpty(tenChucVu))
                {
                    if (cvBUS.ThemChucVu(maBoPhan, tenChucVu))
                    {
                        MessageBox.Show("Thêm Chức Vụ thành công!");
                        LoadChucVu();
                        txtTenChucVu.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thêm Chức Vụ!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập tên Chức Vụ!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Bộ Phận!");
            }
        }

        private void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBoPhan();
        }

        private void cboBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChucVu();
        }

        private void fPhongBan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["fMain"].Show();
        }
    }
}
