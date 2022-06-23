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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Functions.Connect();
        }

        private void mãHSTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsSach_Click(object sender, EventArgs e)
        {
            frmSach frm = new frmSach();
            frm.ShowDialog();
        }

        private void tsTheMuon_Click(object sender, EventArgs e)
        {
            FrmTheMuon frm = new FrmTheMuon();
            frm.ShowDialog();
        }

        private void HoSoMuon_Click(object sender, EventArgs e)
        {
            frmHosomuon frm = new frmHosomuon();
            frm.ShowDialog();
        }

        private void hstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoSoTra frm = new HoSoTra();
            frm.ShowDialog();
        }

        private void HoSoTra_Click(object sender, EventArgs e)
        {
            frmHosotra frm = new frmHosotra();
            frm.ShowDialog();
        }

        private void thủThưToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsTongTien_Click(object sender, EventArgs e)
        {
            frmbaocao7 frm = new frmbaocao7();
            frm.ShowDialog();
        }

        private void mãThẻToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void tsDanhSachHSMtheoTM_Click(object sender, EventArgs e)
        {
            frmbaocaosachchuatra frm = new frmbaocaosachchuatra();
            frm.ShowDialog();
        }

        private void tsTimKiemThuThu_Click(object sender, EventArgs e)
        {

        }
    }
}
