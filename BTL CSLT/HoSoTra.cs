using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlClient;
namespace quan_ly_thu_vien
{
    public partial class HoSoTra : Form
    {
        public HoSoTra()
        {
            InitializeComponent();
        }
        private void HoSoTra_Load(object sender, EventArgs e)
        {
            txtMaSach.Enabled = false;
            txtTenNguoiMuon.Enabled = false;
            txtMaHSM.Enabled = true;
            txtTinhTrang.Enabled = false;
            txtNgayMuon.Enabled = false;
            txtMaHST.Enabled = false;
            txtDonGiaThue.Enabled = false;
            txtTamUng.Enabled = false;
            cbMaTM.Enabled = false;
            txtSoTienThanhToan.Enabled = false;
            txtTongTien.Enabled = false;
            txtTienPhat.Enabled = false;
            txtThanhTien.Enabled = false;
            txtTienPhat.Text = "0";
            txtTongTien.Text = "0";
            Functions.FillCombo("select MaTheMuon from TheMuon ", cbMaTM, "MaTheMuon", "MaTheMuon");
            cbMaTM.SelectedIndex = -1;
            Functions.FillCombo("select MaThuThu from ThuThu", cbMaThuThu, "MaThuThu", "MaThuThu");
            cbMaThuThu.SelectedIndex = -1;
            Functions.FillCombo("select MaViPham from ViPham", cbViPham, "MaViPham", "MaViPham");
            cbViPham.SelectedIndex = -1;
            Functions.FillCombo("select MaHSM from HoSoMuon", cbMaHSM, "MaHSM", "MaHSM");
            cbMaHSM.SelectedIndex = -1;

            if (txtMaHSM.Text != "")
            {
                Load_ttHD();
                btnIn.Enabled = true;
            }
            Load_Datagridview();
        }
        private void Load_Datagridview()
        {
            string sql;
            sql = "select b.MaHSM, a.MaSach, b.NgayMuon, a.DonGiaThue, c.TinhTrang from Sach a, HoSoMuon b, ChiTietHSM c where a.MaSach=c.MaSach and b.MaHSM=c.MaHSM and c.TinhTrang=N'Chưa trả' and b.MaHSM='" + txtMaHSM.Text + "' ";
            //tbltrasach = Classes.Functions.GetDataToTable(sql);
            //dataHST.DataSource = tbltrasach;
            DataTable ChiTietHST = new DataTable();
            ChiTietHST = Functions.GetDataToTable(sql);
            dataHST.DataSource = ChiTietHST;
            dataHST.Columns[0].HeaderText = "Mã hồ sơ mượn";
            dataHST.Columns[1].HeaderText = "Mã sách";
            dataHST.Columns[2].HeaderText = "Ngày mượn";
            dataHST.Columns[3].HeaderText = "Đơn giá thuê";
            dataHST.Columns[4].HeaderText = "Tình trạng";
            dataHST.AllowUserToAddRows = false;
            dataHST.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void Load_ttHD()
        {
            string str;
            str = "select NgayMuon from HoSoMuon where MaHSM=N'" + txtMaHSM.Text + "'";
            txtNgayMuon.Text = Functions.ConvertDateTime(Functions.GetFieldValues(str));
            str = "select MaTheMuon from HoSoMuon where MaHSM=N'" + txtMaHSM.Text + "'";
            cbMaTM.Text = Functions.GetFieldValues(str);
            str = "select HoTen from TheMuon where MaTheMuon =N'" + cbMaTM.Text + "'";
            txtTenNguoiMuon.Text = Functions.GetFieldValues(str);
            str = "select TamUng from HoSoMuon where MaHSM=N'" + txtMaHSM.Text + "'";
            txtTamUng.Text = Functions.GetFieldValues(str);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnTraSach.Enabled = false;
            ResetValues();
            txtMaHST.Text = Functions.CreateKey("HDT");
            txtNgayTra.Text = Functions.ConvertDateTime(DateTime.Now.ToShortDateString());
            dataHST.DataSource = null;
        }
        private void ResetValues()
        {
            txtMaHSM.Text = "";
            txtMaSach.Text = "";
            cbMaTM.Text = "";
            txtTinhTrang.Text = "";
            txtTienPhat.Text = "0";
            txtDonGiaThue.Text = "0";
            txtThanhTien.Text = "0";
            txtTamUng.Text = "0";
            txtSoTienThanhToan.Text = "0";

            txtNgayMuon.Text = "";
            txtTenNguoiMuon.Text = "";
            txtDonGiaThue.Text = "";
            cbMaThuThu.Text = "";
            cbViPham.Text = "";

        }
        private void ResetValuesSach()
        {
            txtMaSach.Text = "";
            txtDonGiaThue.Text = "0";
            txtThanhTien.Text = "0";
            txtSoNgayThue.Text = "0";
            txtTienPhat.Text = "0";
            txtSoTienThanhToan.Text = "0";
        }

        private void dataHST_Click(object sender, EventArgs e)
        {

            //txtmathue.Text = dgirdtrasach.CurrentRow.Cells["Mathue"].Value.ToString();
            cbMaTM.Text = Functions.GetFieldValues("select MaTheMuon from HoSoMuon where MaHSM=N'" + txtMaHSM.Text + "'");
            txtTenNguoiMuon.Text = Functions.GetFieldValues("select HoTen from TheMuon where MaTheMuon=N'" + cbMaTM.Text + "'");
            txtMaSach.Text = dataHST.CurrentRow.Cells["MaSach"].Value.ToString();
            txtTinhTrang.Text = Functions.GetFieldValues("select TinhTrang from ChiTietHSM where MaSach=N'" + txtMaSach.Text + "' and MaHSM=N'" + txtMaHSM.Text + "'");
            txtNgayMuon.Text = dataHST.CurrentRow.Cells["NgayMuon"].Value.ToString();
            txtDonGiaThue.Text = Functions.GetFieldValues("select DonGiaThue from Sach where MaSach=N'" + txtMaSach.Text + "'");
            /*if (btnTraSach.Enabled == true)
            {
                tinhngay();
                double dgt, tp, thanhtien, songay  , tongtien , sotienthanhtoan;
                dgt = Convert.ToDouble(txtDonGiaThue.Text);
                tp = Convert.ToDouble(txtTienPhat.Text);
                songay = Convert.ToDouble(txtSoNgayThue.Text);
                thanhtien = dgt * songay + tp;

                //txtSoNgayThue.Text = songay.ToString();
                txtThanhTien.Text = thanhtien.ToString();
            }*/
            if (btnTraSach.Enabled == false)
            {
                demngay();
                tinhtienthanhtoan();
                double dgt, tphat, thanhtien, songay;
                double sttt, tt, tu;
                dgt = Convert.ToDouble(txtDonGiaThue.Text);
                tphat = Convert.ToDouble(txtTienPhat.Text);
                songay = Convert.ToDouble(txtSoNgayThue.Text);

                tt = Convert.ToDouble(txtTongTien.Text);
                tu = Convert.ToDouble(txtTamUng.Text);
                thanhtien = dgt * songay + tphat;
                sttt = tt - tu;
                txtThanhTien.Text = thanhtien.ToString();
                txtSoTienThanhToan.Text = sttt.ToString();
            }
        }

        private void cbViPham_SelectedValueChanged(object sender, EventArgs e)
        {
            txtTienPhat.Text = Functions.GetFieldValues("select TienPhat from ViPham where MaViPham=N'" + cbViPham.SelectedValue + "'");
        }
        private void demngay()
        {

            DateTime thue = new DateTime();
            thue = Convert.ToDateTime(txtNgayMuon.Text);
            DateTime tra = new DateTime();
            tra = Convert.ToDateTime(txtNgayTra.Text);
            TimeSpan songay = tra - thue;
            int sn = songay.Days;
            txtSoNgayThue.Text = Convert.ToString(sn);
        }
        private void tinhtienthanhtoan()
        {
            double TONGTIEN = new double();
            TONGTIEN = Convert.ToDouble(txtTongTien.Text);
            double TAMUNG = new double();
            TAMUNG = Convert.ToDouble(txtTamUng.Text);
            double Sotienthanhtoan = TONGTIEN - TAMUNG;
            txtSoTienThanhToan.Text = Convert.ToString(Sotienthanhtoan);
        }
        private void txtNgayTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                demngay();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double tong;
            double tongmoi;
            double sotienthanhtoan;
            sql = "select MaHST from HoSoTra where MaHST=N'" + txtMaHST.Text + "'";
            if (!Functions.CheckKey(sql)) // nếu mã hóa đơn không trùng hóa tiến hành lưu được nhiều mã
            {
                if (cbMaThuThu.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbMaThuThu.Focus();
                    return;
                }
                Functions.Connect();
                sql = "INSERT INTO HoSoTra(MaHST, MaHSM, NgayTra, TongTien, SoTienThanhToan , MaThuThu) VALUES(N'" + txtMaHST.Text.Trim() + "',N'" + txtMaHSM.Text.Trim() + "' ,'" + Functions.ConvertDateTime(txtNgayTra.Text.Trim()) + "',N'" + txtTongTien.Text.Trim() + "','" + txtSoTienThanhToan.Text.Trim() + "' , '" + cbMaThuThu.SelectedValue + "')";
                //Functions.RunSQL(sql);
                SqlCommand myCommand = new SqlCommand(sql, Functions.con);                     // Khai báo đối tượng SqlCommand
                myCommand.ExecuteNonQuery();
            }
            string sql1 = "insert into ChiTietHST(MaHST, MaSach, ThanhTien , MaViPham) values(N'" + txtMaHST.Text.Trim() + "', N'" + txtMaSach.Text + "', N'" + txtThanhTien.Text + "', N'" + cbViPham.SelectedValue.ToString() + "')";
            //Functions.RunSQL(sql);
            SqlCommand myCommand1 = new SqlCommand(sql1, Functions.con);                     // Khai báo đối tượng SqlCommand
            myCommand1.ExecuteNonQuery();
            string sql2 = "update Sach set SoLuong=SoLuong+1 where MaSach=N'" + txtMaSach.Text + "'";
            //Functions.RunSQL(sql);
            SqlCommand myCommand2 = new SqlCommand(sql2, Functions.con);                     // Khai báo đối tượng SqlCommand
            myCommand2.ExecuteNonQuery();
            string sql3 = "update TheMuon set SoLuongMuon = SoLuongMuon - 1 where MaTheMuon = N '" + cbMaTM + "'";
            SqlCommand myCommand3 = new SqlCommand(sql3, Functions.con);                     // Khai báo đối tượng SqlCommand
            myCommand3.ExecuteNonQuery();
            string sql4 = "update HoSoMuon set TinhTrang=N'Đã Trả' where Masach=N'" + txtMaSach.Text + "' and MaHoSoMuon=N'" + txtMaHSM.Text + "' ";
            //Classes.Functions.RunSQL(sql);
            SqlCommand myCommand4 = new SqlCommand(sql4, Functions.con);                     // Khai báo đối tượng SqlCommand
            myCommand4.ExecuteNonQuery();
            Load_Datagridview();
            // Cập nhật lại tổng tiền cho hóa đơn 
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM HoSoTra WHERE MaHST = N'" + txtMaHST.Text + "'and MaHSM= '" + txtMaHSM.Text + "'"));
            tongmoi = tong + Convert.ToDouble(txtThanhTien.Text); //tổng mới = tổng cũ + thành tiền
            string sql5 = "UPDATE HoSoTra SET TongTien =" + tongmoi + " WHERE MaHST = N'" + txtMaHST.Text + "'"; //update vào bảng hóa đơn bán
            //Classes.Functions.RunSQL(sql);
            SqlCommand myCommand5 = new SqlCommand(sql5, Functions.con);                     // Khai báo đối tượng SqlCommand
            myCommand5.ExecuteNonQuery();
            txtTongTien.Text = tongmoi.ToString();

            ResetValuesSach();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cbMaHSM.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbMaHSM.Focus();
                return;
            }
            txtMaHSM.Text = cbMaHSM.SelectedValue.ToString();
            Load_ttHD();
            Load_Datagridview();
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
            cbMaHSM.SelectedIndex = -1;
        }

        private void cbMaHSM_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaHSM FROM HoSoMuon", cbMaHSM, "MaHSM", "MaHSM");
            cbMaHSM.SelectedIndex = -1;
        }

        private void txtThanhTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtSoNgayThue_TextChanged(object sender, EventArgs e)
        {
            double sn, tp, dg, tt;
            if (txtSoNgayThue.Text == "")
                sn = 0;
            else
                sn = Convert.ToDouble(txtSoNgayThue.Text);
            if (txtTienPhat.Text == "")
                tp = 0;
            else
                tp = Convert.ToDouble(txtTienPhat.Text);
            if (txtDonGiaThue.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGiaThue.Text);
            tt = sn * dg + tp;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            double sn, tp, dg, tt;
            if (txtSoNgayThue.Text == "")
                sn = 0;
            else
                sn = Convert.ToDouble(txtSoNgayThue.Text);
            if (txtTienPhat.Text == "")
                tp = 0;
            else
                tp = Convert.ToDouble(txtTienPhat.Text);
            if (txtDonGiaThue.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGiaThue.Text);
            tt = sn * dg + tp;
            txtThanhTien.Text = tt.ToString();
        }

        private void dataHST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
