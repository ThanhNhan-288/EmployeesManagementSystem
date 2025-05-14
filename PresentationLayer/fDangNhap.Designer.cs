namespace PresentationLayer
{
    partial class fDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDangNhap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_DN_DangKi = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_DN_TenTaiKhoan = new System.Windows.Forms.TextBox();
            this.txt_DN_MatKhau = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ShowPass = new System.Windows.Forms.CheckBox();
            this.btn_DN_DangNhap = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CadetBlue;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_DN_DangKi);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 492);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(97, 146);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_DN_DangKi
            // 
            this.btn_DN_DangKi.BackColor = System.Drawing.Color.Teal;
            this.btn_DN_DangKi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DN_DangKi.FlatAppearance.BorderSize = 0;
            this.btn_DN_DangKi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DN_DangKi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DN_DangKi.ForeColor = System.Drawing.Color.White;
            this.btn_DN_DangKi.Location = new System.Drawing.Point(39, 402);
            this.btn_DN_DangKi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DN_DangKi.Name = "btn_DN_DangKi";
            this.btn_DN_DangKi.Size = new System.Drawing.Size(267, 41);
            this.btn_DN_DangKi.TabIndex = 1;
            this.btn_DN_DangKi.Text = "Đăng Kí";
            this.btn_DN_DangKi.UseVisualStyleBackColor = false;
            this.btn_DN_DangKi.Click += new System.EventHandler(this.btn_DN_DangKi_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(111, 367);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tạo Tài Khoản";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(360, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Đăng Nhập";
            // 
            // exit
            // 
            this.exit.AutoSize = true;
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.Location = new System.Drawing.Point(732, 11);
            this.exit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(20, 21);
            this.exit.TabIndex = 2;
            this.exit.Text = "X";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(361, 171);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tên Tài Khoản :";
            // 
            // txt_DN_TenTaiKhoan
            // 
            this.txt_DN_TenTaiKhoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_DN_TenTaiKhoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txt_DN_TenTaiKhoan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DN_TenTaiKhoan.Location = new System.Drawing.Point(365, 209);
            this.txt_DN_TenTaiKhoan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_DN_TenTaiKhoan.Multiline = true;
            this.txt_DN_TenTaiKhoan.Name = "txt_DN_TenTaiKhoan";
            this.txt_DN_TenTaiKhoan.Size = new System.Drawing.Size(365, 36);
            this.txt_DN_TenTaiKhoan.TabIndex = 4;
            // 
            // txt_DN_MatKhau
            // 
            this.txt_DN_MatKhau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_DN_MatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txt_DN_MatKhau.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DN_MatKhau.Location = new System.Drawing.Point(365, 294);
            this.txt_DN_MatKhau.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_DN_MatKhau.Multiline = true;
            this.txt_DN_MatKhau.Name = "txt_DN_MatKhau";
            this.txt_DN_MatKhau.PasswordChar = '*';
            this.txt_DN_MatKhau.Size = new System.Drawing.Size(365, 36);
            this.txt_DN_MatKhau.TabIndex = 6;
            this.txt_DN_MatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_DN_MatKhau_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(361, 263);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Mật Khẩu :";
            // 
            // ShowPass
            // 
            this.ShowPass.AutoSize = true;
            this.ShowPass.Location = new System.Drawing.Point(604, 350);
            this.ShowPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ShowPass.Name = "ShowPass";
            this.ShowPass.Size = new System.Drawing.Size(114, 20);
            this.ShowPass.TabIndex = 7;
            this.ShowPass.Text = "Hiện mật khẩu";
            this.ShowPass.UseVisualStyleBackColor = true;
            this.ShowPass.CheckedChanged += new System.EventHandler(this.ShowPass_CheckedChanged);
            // 
            // btn_DN_DangNhap
            // 
            this.btn_DN_DangNhap.BackColor = System.Drawing.Color.Teal;
            this.btn_DN_DangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DN_DangNhap.FlatAppearance.BorderSize = 0;
            this.btn_DN_DangNhap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.btn_DN_DangNhap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(8)))), ((int)(((byte)(138)))));
            this.btn_DN_DangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DN_DangNhap.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DN_DangNhap.ForeColor = System.Drawing.Color.White;
            this.btn_DN_DangNhap.Location = new System.Drawing.Point(368, 367);
            this.btn_DN_DangNhap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DN_DangNhap.Name = "btn_DN_DangNhap";
            this.btn_DN_DangNhap.Size = new System.Drawing.Size(133, 37);
            this.btn_DN_DangNhap.TabIndex = 8;
            this.btn_DN_DangNhap.Text = "Đăng Nhập";
            this.btn_DN_DangNhap.UseVisualStyleBackColor = false;
            this.btn_DN_DangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // fDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(767, 492);
            this.Controls.Add(this.btn_DN_DangNhap);
            this.Controls.Add(this.ShowPass);
            this.Controls.Add(this.txt_DN_MatKhau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_DN_TenTaiKhoan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fDangNhap";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fDangNhap_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label exit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_DN_TenTaiKhoan;
        private System.Windows.Forms.TextBox txt_DN_MatKhau;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ShowPass;
        private System.Windows.Forms.Button btn_DN_DangNhap;
        private System.Windows.Forms.Button btn_DN_DangKi;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

