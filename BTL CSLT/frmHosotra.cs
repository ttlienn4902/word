using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace quan_ly_thu_vien
{
    public partial class frmHosotra : Form
    {
        public frmHosotra()
        {
            InitializeComponent();
        }

        private void groupHST_Enter(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataToGridviewHST();
            string sqla = "Select MaHSM  from HoSoMuon";
            Functions.FillCombo(sqla, cbMaHSM2, "MaHSM", "MaHSM");
            string sqlb = "Select MaThuThu from ThuThu";
            Functions.FillCombo(sqlb, cbMaThuThu2, "MaThuThu", "MaThuThu");

        }
        private void LoadDataToGridviewHST()
        {
            string sql = "Select * from HoSoTra";
            DataTable HoSoTra = new DataTable();
            HoSoTra = Functions.GetDataToTable(sql);
            dataGridViewHST.DataSource = HoSoTra;
        }
        private void groupCTHST_Enter(object sender, EventArgs e)
        {
            btnLuu9.Enabled = false;
            btnBoQua9.Enabled = false;
            LoadDataToGridviewCTHST();
            string sqla = "Select MaHST from HoSoTra";
            Functions.FillCombo(sqla, cbMaHST9, "MaHST", "MaHST");
            string sqlb = "Select MaSach from dmSach";
            Functions.FillCombo(sqlb, cbMaSach9, "MaSach", "MaSach");
            string sqlc = "Select MaViPham from ViPham";
            Functions.FillCombo(sqlc, cbMaViPham9, "MaViPham", "MaViPham");
        }

        private void LoadDataToGridviewCTHST()
        {
            string sql = "Select * from ChiTietHST where mahst='" + txtMaHST2.Text + "'";
            //string sql = "select  masach   from ChiTietHSM  where MaHSM = '" + cbMaHSM2.Text + "' and masach not in ( select masach from ChiTietHST where MaHST = '" + txtMaHST2.Text + "' ) ";
            DataTable ChiTietHST = new DataTable();
            ChiTietHST = Functions.GetDataToTable(sql);
            dataGridViewCTHST.DataSource = ChiTietHST;
            /*string sql;
            sql = "select b.Mathue, a.Masach, b.Ngaythue, a.Dongiathue, c.Datra from tblSach a, tblHDThue b,tblCTHDThue c where a.Masach=c.Masach and b.Mathue=c.Mathue and c.Datra=N'Chưa' and b.Mathue='" + txtmathue.Text + "' ";
            tbltrasach = Classes.Functions.GetDataToTable(sql);
            dgirdtrasach.DataSource = tbltrasach;
            dgirdtrasach.Columns[0].HeaderText = "Mã thuê";
            dgirdtrasach.Columns[1].HeaderText = "Mã sách";
            dgirdtrasach.Columns[2].HeaderText = "Ngày thuê";
            dgirdtrasach.Columns[3].HeaderText = "Đơn giá thuê";
            dgirdtrasach.Columns[4].HeaderText = "Đã trả";
            dgirdtrasach.AllowUserToAddRows = false;
            dgirdtrasach.EditMode = DataGridViewEditMode.EditProgrammatically;*/
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            txtMaHST2.Text = "";
            cbMaThuThu2.Text = "";
            cbMaHSM2.Text = "";

            txtSoTienThanhToan2.Text = "";
            txtTongTien2.Text = "";
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;

        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from  hosotra  where mahst= '" + txtMaHST2.Text + "'";
            SqlCommand myCommand = new SqlCommand(sql, Functions.con);
            myCommand.ExecuteNonQuery();
            LoadDataToGridviewHST();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "SELECT MaHST FROM HoSoTra WHERE MaHST = '" + txtMaHST2.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hồ sơ trả này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHST2.Focus();
                txtMaHST2.Text = "";
                return;
            }

            if (!Functions.CheckKey(sql))
            {
                try
                {
                    Functions.Connect();
                    string sql2;
                    sql2 = "INSERT INTO HoSoTra(MaHST,MaHSM, NgayTra , TongTien , SoTienThanhToan , MaThuThu) VALUES('" + txtMaHST2.Text + "', '" + cbMaHSM2.Text + "' , '" + dateTimePicker2.Text + "','" + txtTongTien2.Text + "','" + txtSoTienThanhToan2.Text + "','" + cbMaThuThu2.Text + "')";

                    SqlCommand myCommand = new SqlCommand(sql2, Functions.con);                     // Khai báo đối tượng SqlCommand
                    myCommand.ExecuteNonQuery();
                    LoadDataToGridviewHST();


                    //Sau khi thuc hien thanh cong thi lai hien cac nut len de su dung tiep
                    /*  btnXoa.Enabled = true;
                      btnThem.Enabled = true;
                      btnSua.Enabled = true;
                      btnLuu.Enabled = false;*/
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void dataGridViewHST_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHST2.Text = dataGridViewHST.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker2.Text = dataGridViewHST.CurrentRow.Cells[2].Value.ToString();
            cbMaHSM2.Text = dataGridViewHST.CurrentRow.Cells[1].Value.ToString();
            cbMaThuThu2.Text = dataGridViewHST.CurrentRow.Cells[5].Value.ToString();
            txtTongTien2.Text = dataGridViewHST.CurrentRow.Cells[3].Value.ToString();
            txtSoTienThanhToan2.Text = dataGridViewHST.CurrentRow.Cells[4].Value.ToString();
            LoadDataToGridviewCTHST();

        }


        private void dataGridViewCTHST_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbMaHST9.Text = dataGridViewCTHST.CurrentRow.Cells[0].Value.ToString();
            cbMaSach9.Text = dataGridViewCTHST.CurrentRow.Cells[1].Value.ToString();
            cbMaViPham9.Text = dataGridViewCTHST.CurrentRow.Cells[2].Value.ToString();
            txtThanhTien.Text = dataGridViewCTHST.CurrentRow.Cells[3].Value.ToString();

        }

        private void FrmHoSoTra_Load(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql = "update hosotra set MaHSM='" + cbMaHSM2.Text + "',NgayTra ='" + dateTimePicker2.Text + "',tongtien ='" + txtTongTien2.Text + "', sotienthanhtoan='" + txtSoTienThanhToan2.Text + "', mathuthu='" + cbMaThuThu2.Text + " ' where mahst= '" + txtMaHST2.Text + "'";
            SqlCommand myCommand = new SqlCommand(sql, Functions.con);
            myCommand.ExecuteNonQuery();
            LoadDataToGridviewHST();
        }

        private void btnThem9_Click(object sender, EventArgs e)
        {
            btnLuu9.Enabled = true;
            btnBoQua9.Enabled = true;
        }

        private void btnXoa9_Click(object sender, EventArgs e)
        {
            string sql = "delete from chitiethst where mahst= '" + txtMaHST2.Text + "'and masach='" + cbMaSach9.Text + "'";
            SqlCommand myCommand = new SqlCommand(sql, Functions.con);
            myCommand.ExecuteNonQuery();
            LoadDataToGridviewCTHST();
        }

        private void btnSua9_Click(object sender, EventArgs e)
        {
            string sql = "update chitiethst set MaviPHAM='" + cbMaViPham9.Text + "',thanhtien ='" + txtThanhTien.Text + "' where mahst= '" + txtMaHST2.Text + "'and masach='" + cbMaSach9.Text + "'";
            SqlCommand myCommand = new SqlCommand(sql, Functions.con);
            myCommand.ExecuteNonQuery();
            LoadDataToGridviewCTHST();
        }

        private void btnLuu9_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "insert into chitiethst (mahst,masach,thanhtien,mavipham) values('" + cbMaHST9.Text + "','" + cbMaSach9.Text + "','" + txtThanhTien.Text + "','" + cbMaViPham9.Text + "')";
                SqlCommand myCommand = new SqlCommand(sql, Functions.con);
                myCommand.ExecuteNonQuery();
                LoadDataToGridviewCTHST();
                string sql2, sql3;
                sql2 = "update themuon set soluongmuon= soluongmuon-1 where mathemuon= (select mathemuon from hosomuon where mahsm='" + cbMaHSM2.Text + "')";
                SqlCommand myCommand2 = new SqlCommand(sql2, Functions.con);
                myCommand2.ExecuteNonQuery();
                sql3 = "update sach set soluong = soluong +1 where masach='" + cbMaSach9.Text + "'";
                SqlCommand myCommand3 = new SqlCommand(sql3, Functions.con);
                myCommand3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the luu");
            }

        }

        private void btnBoQua9_Click(object sender, EventArgs e)
        {
            btnLuu9.Enabled = false;
            btnBoQua9.Enabled = false;
            cbMaHST9.Text = "";
            cbMaSach9.Text = "";
            cbMaViPham9.Text = "";
            txtThanhTien.Text = "";
        }

        private void btnDong9_Click(object sender, EventArgs e)
        {

            DialogResult dlg = new DialogResult();
            dlg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Đóng_Click(object sender, EventArgs e)
        {

            DialogResult dlg = new DialogResult();
            dlg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                this.Close();
            }
        }



        /*private void SoTienThanhToan(string MaHSM, string MaHST)
        {
            Double s, sl, SLcon;
            string sql;
            sql = " SELECT TamUng FROM HoSoMuon WHERE MaHSM = N '" + MaHSM + "' AND MaHang = N '" + Mahang + "'";
            s = Convert.ToDouble(Functions.GetFieldValues(sql));
            sql = " DELETE tblChitietHDBan WHERE MaHDBan = N '" + Mahoadon +" ' AND Mahang = N'"+Mahang + " '";

            Functions.RunSqlDel(sql);
            // Cập nhật lại số lượng cho các mặt hàng
            sql = " SELECT Soluong FROM tblHang WHERE Mahang = N '" +Mahang + " '";
            sl = Convert.ToDouble(Functions.GetFieldValues(sql));
            SLcon = sl + s;
            sql = " UPDATE tblHang SET Soluong = " +Slcon + " WHERE Mahang = N '" +Mahang + "'";

            Functions.RunSql(sql);
        }*/

    }
}