using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 
namespace quan_ly_thu_vien
{
    public partial class frmHosomuon : Form
    {
        public frmHosomuon()
        {
            InitializeComponent();
        }

        private void FrmHoSoMuon_Load(object sender, EventArgs e)
        {

        }

        private void groupHSM_Enter(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataToGridviewHSM();
            string sqla = "Select MaTheMuon from TheMuon";
            Functions.FillCombo(sqla, cbMaTheMuon3, "MaTheMuon", "MaTheMuon");
            string sqlb = "Select MaThuThu from ThuThu";
            Functions.FillCombo(sqlb, cbMaThuThu3, "MaThuThu", "MaTheMuon");
            cbMaTheMuon3.SelectedIndex = -1;
            cbMaThuThu3.SelectedIndex = -1;
        }
        private void LoadDataToGridviewHSM()
        {
            string sql = "Select * from HoSoMuon";
            DataTable HoSoMuon = new DataTable();
            HoSoMuon = Functions.GetDataToTable(sql);
            dataGridViewHSM.DataSource = HoSoMuon;
            LoadDataToGridviewCTHSM();
        }
        private void dataGridViewHSM_Click(object sender, EventArgs e)
        {
            txtMaHSM3.Text = dataGridViewHSM.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridViewHSM.CurrentRow.Cells[1].Value.ToString();
            txtTamUng3.Text = dataGridViewHSM.CurrentRow.Cells[2].Value.ToString();
            cbMaTheMuon3.Text = dataGridViewHSM.CurrentRow.Cells[4].Value.ToString();
            cbMaThuThu3.Text = dataGridViewHSM.CurrentRow.Cells[3].Value.ToString();
            LoadDataToGridviewCTHSM();
        }
        /* private void dataGridViewHSM_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             txtMaHSM3.Text = dataGridViewHSM.CurrentRow.Cells[0].Value.ToString();
             dateTimePicker1.Text = dataGridViewHSM.CurrentRow.Cells[1].Value.ToString();
             txtTamUng3.Text = dataGridViewHSM.CurrentRow.Cells[2].Value.ToString();
             cbMaTheMuon3.Text = dataGridViewHSM.CurrentRow.Cells[4].Value.ToString();
             cbMaThuThu3.Text = dataGridViewHSM.CurrentRow.Cells[3].Value.ToString();
             LoadDataToGridviewCTHSM();
         }*/
        private void dataGridViewCTHSM_Click(object sender, EventArgs e)
        {
            cbTinhTrang.Text = dataGridViewCTHSM.CurrentRow.Cells[2].Value.ToString();
            cbMaHSM7.Text = dataGridViewCTHSM.CurrentRow.Cells[0].Value.ToString();
            cbMaSach7.Text = dataGridViewCTHSM.CurrentRow.Cells[1].Value.ToString();
        }
        private void groupBoxCTHSM_Enter(object sender, EventArgs e)
        {

            btnLuu7.Enabled = false;
            btnBoqua7.Enabled = false;
            LoadDataToGridviewCTHSM();
            string sqlc = "Select MaHSM from HoSoMuon";
            Functions.FillCombo(sqlc, cbMaHSM7, "MaHSM", "MaHSM");
            string sqld = "Select MaSach from Sach";
            Functions.FillCombo(sqld, cbMaSach7, "MaSach", "MaSach");
            cbMaHSM7.SelectedIndex = -1;
            cbMaSach7.SelectedIndex = -1;
            cbTinhTrang.SelectedIndex = -1;
        }
        private void LoadDataToGridviewCTHSM()
        {
            string sql = "Select * from ChiTietHSM where mahsm='" + txtMaHSM3.Text + "'";
            DataTable ChiTietHSM = new DataTable();
            ChiTietHSM = Functions.GetDataToTable(sql);
            dataGridViewCTHSM.DataSource = ChiTietHSM;
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaHSM3.Text = "";
            cbMaTheMuon3.Text = "";
            cbMaThuThu3.Text = "";
            txtTamUng3.Text = "";
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "SELECT MaHSM FROM HoSoMuon WHERE MaHSM = '" + txtMaHSM3.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã sách này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHSM3.Focus();
                txtMaHSM3.Text = "";
                return;
            }

            if (!Functions.CheckKey(sql))
            {
                try
                {
                    Functions.Connect();
                    string sql2;
                    sql2 = "INSERT INTO HoSoMuon(MaHSM,NgayMuon, TamUng , MaThuThu , MaTheMuon) VALUES('" + txtMaHSM3.Text + "', '" + dateTimePicker1.Text + "' , '" + txtTamUng3.Text + "','" + cbMaThuThu3.Text + "','" + cbMaTheMuon3.Text + "')";

                    SqlCommand myCommand = new SqlCommand(sql2, Functions.con);                     // Khai báo đối tượng SqlCommand
                    myCommand.ExecuteNonQuery();
                    LoadDataToGridviewHSM();


                    //Sau khi thuc hien thanh cong thi lai hien cac nut len de su dung tiep
                    /*  btnXoa.Enabled = true;
                      btnThem.Enabled = true;
                      btnSua.Enabled = true;
                      btnLuu.Enabled = false;*/
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Khong the them!");
                }
            }
        }

        private void btnThem7_Click(object sender, EventArgs e)
        {
            btnLuu7.Enabled = true;
            btnBoqua7.Enabled = true;

        }
        //ham kiem tra the muon co thoa man yeu cau khong
        public bool KiemtraTheMuon(String mahosomuon)
        {
            string sql = "Select soluongmuon,ngayhethan from themuon where mathemuon=(select mathemuon from hosomuon where mahsm='" + mahosomuon + "')";
            DataTable themuon = new DataTable();
            themuon = Functions.GetDataToTable(sql);
            int soluong;
            DateTime hsd;
            soluong = int.Parse(themuon.Rows[0][0].ToString());
            hsd = DateTime.Parse(themuon.Rows[0][1].ToString());

            if (hsd < DateTime.Now || soluong > 3)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool KiemtraSach(string masach)
        {
            string sql = "Select soluong from sach where masach='" + masach + "'";
            DataTable sach = new DataTable();
            sach = Functions.GetDataToTable(sql);
            int soluong;
            soluong = int.Parse(sach.Rows[0][0].ToString());
            if (soluong > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void btnLuu7_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemtraSach(cbMaSach7.Text) == false || KiemtraTheMuon(cbMaHSM7.Text) == false)
                {
                    MessageBox.Show("Không đủ điều kiện mượn sách!");

                }
                else
                {
                    Functions.Connect();
                    string sql2;
                    sql2 = "INSERT INTO ChiTietHSM(MaHSM,MaSach, TinhTrang ) VALUES('" + cbMaHSM7.Text + "', '" + cbMaSach7.Text + "' , N'" + cbTinhTrang.Text + "')";
                    SqlCommand myCommand = new SqlCommand(sql2, Functions.con);
                    myCommand.ExecuteNonQuery();


                    // LoadDataToGridviewHSM();
                    LoadDataToGridviewCTHSM();
                    string sql3, sql4;
                    Functions.Connect();
                    sql3 = "update themuon set soluongmuon= soluongmuon + 1 where mathemuon=(select mathemuon from hosomuon where mahsm='" + cbMaHSM7.Text + "')";
                    //MessageBox.Show(sql3);
                    SqlCommand myCommand2 = new SqlCommand(sql3, Functions.con);
                    myCommand2.ExecuteNonQuery();
                    Functions.Connect();
                    sql4 = "update sach set soluong= soluong-1 where masach='" + cbMaSach7.Text.Trim() + "'";
                    SqlCommand myCommand3 = new SqlCommand(sql4, Functions.con);
                    myCommand3.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đủ điều kiện mượn sách!");
            }



            //Sau khi thuc hien thanh cong thi lai hien cac nut len de su dung tiep
            /*  btnXoa.Enabled = true;
              btnThem.Enabled = true;
              btnSua.Enabled = true;
              btnLuu.Enabled = false;*/


        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dlg = new DialogResult();
            dlg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtMaHSM3.Text = "";
            cbMaTheMuon3.Text = "";
            cbMaThuThu3.Text = "";
            txtTamUng3.Text = "";
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }
        private void ResetValueHSM()
        {
            txtMaHSM3.Text = "";
            cbMaTheMuon3.Text = "";
            cbMaThuThu3.Text = "";
            txtTamUng3.Text = "";
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }
        private void ResetValueCTHSM()
        {
            cbMaHSM7.Text = "";
            cbMaSach7.Text = "";
            cbTinhTrang.Text = "";

        }
        private void btnSua_Click(object sender, EventArgs e)
        {

            string sql = "update hosomuon set ngaymuon='" + dateTimePicker1.Text + "',tamung='" + txtTamUng3.Text + "',mathuthu='" + cbMaThuThu3.Text + "',mathemuon='" + cbMaTheMuon3.Text + "' where mahsm= '" + txtMaHSM3.Text + "'";
            SqlCommand myCommand = new SqlCommand(sql, Functions.con);
            myCommand.ExecuteNonQuery();
            LoadDataToGridviewHSM();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHSM3.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "delete from hosomuon  where mahsm= '" + txtMaHSM3.Text + "'";
                    SqlCommand myCommand = new SqlCommand(sql, Functions.con);
                    myCommand.ExecuteNonQuery();
                    LoadDataToGridviewHSM();
                    ResetValueHSM();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa không thành công!" + ex.Message);
                }
            }
        }

        private void btnXoa7_Click(object sender, EventArgs e)
        {
            if (cbMaHSM7.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Functions.Connect();
                    string sqlnew = "delete from chitiethsm  where mahsm= '" + cbMaHSM7.Text + "' and masach= '" + cbMaSach7.Text + "'";
                    SqlCommand myCommand2 = new SqlCommand(sqlnew, Functions.con);
                    myCommand2.ExecuteNonQuery();
                    LoadDataToGridviewCTHSM();
                    ResetValueCTHSM();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa không thành công!" + ex.Message);
                }
            }


            /*Functions.Connect();
            string sqlnew = "delete from chitiethsm  where mahsm= '" + cbMaHSM7.Text + "' and masach= '" +cbMaSach7.Text+ "'";
            SqlCommand myCommand2 = new SqlCommand(sqlnew, Functions.con);
            myCommand2.ExecuteNonQuery();
            LoadDataToGridviewCTHSM();
            ResetValueCTHSM();*/
        }

        private void btnDong7_Click(object sender, EventArgs e)
        {
            DialogResult dlg = new DialogResult();
            dlg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnBoqua7_Click(object sender, EventArgs e)
        {
            ResetValueCTHSM();
            btnLuu7.Enabled = false;
            btnBoqua7.Enabled = false;
        }

        private void btnSua7_Click(object sender, EventArgs e)
        {
            string sql = "update chitiethsm set tinhtrang=N'" + cbTinhTrang.Text + "' where mahsm= '" + cbMaHSM7.Text + "'and masach='" + cbMaSach7.Text + "'";
            SqlCommand myCommand = new SqlCommand(sql, Functions.con);
            myCommand.ExecuteNonQuery();
            LoadDataToGridviewCTHSM();
        }

        private void dataGridViewCTHSM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmHosomuon_Load_1(object sender, EventArgs e)
        {

        }

        /*private void dataGridViewCTHSM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbTinhTrang.Text = dataGridViewCTHSM.CurrentRow.Cells[2].Value.ToString();
            cbMaHSM7.Text = dataGridViewCTHSM.CurrentRow.Cells[0].Value.ToString();
            cbMaSach7.Text = dataGridViewCTHSM.CurrentRow.Cells[1].Value.ToString();

        }*/


    }
}