using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quan_ly_thu_vien
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            /* if (MessageBox.Show(" Ban co muon thoat khong ?",
               "canh bao !", MessageBoxButtons.OKCancel) == DialogResult.OK) ;
            Application.Exit();*/
            DialogResult dlg = new DialogResult();
            dlg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string userName, userPassword;

            if (txtTenDangNhap.Text.Trim().Length == 0 || txtMatKhau.Text.Trim().Length == 0)
            {
                lblLoi.Text = "**Chưa nhập đầy đủ thông tin!**";
                lblLoi.Visible = true;
                return;
            }

            userName = txtTenDangNhap.Text;
            userPassword = txtMatKhau.Text;
            if (Functions.Login(userName, userPassword))
            {
                MessageBox.Show("Chào Admin, chúc một ngày làm việc vui vẻ :) !");

                frmMain frm = new frmMain();

                this.Hide();
                frm.ShowDialog();
                Functions.Close();
            }
            else
            {
                MessageBox.Show("Sai rồi, mời bạn nhập lại");
                return;
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                //textBox1.BringToFont();
                txtMatKhau.PasswordChar = '\0';
            }
        }
        /*bool KiemTraDangNhap(string TenDangNhap , string MatKhau)
{
   if (TenDangNhap == this.TenDangNhap && MatKhau == this.MatKhau)
   {
       return true;
   }
   else return false;


}*/
    }
}
