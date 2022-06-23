using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;


namespace quan_ly_thu_vien
{
    public partial class frmbaocao9 : Form
    {
        DataTable HSMtheoTM;
        public frmbaocao9()
        {
            InitializeComponent();
        }

        private void frmbaocao9_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            Functions.FillCombo("SELECT distinct MaTheMuon FROM TheMuon", cbMaTheMuonBC, "MaTheMuon", "MaTheMuon");
            cbMaTheMuonBC.SelectedIndex = -1;
        }
        private void loadDataTimKiemHSM()
        {
            Functions.Connect();
            String SQL = "select MaHSM, TheMuon.MaTheMuon,  TamUng,MaThuThu , NgayMuon From TheMuon join HoSoMuon on TheMuon.MaTheMuon = HoSoMuon.MaTheMuon ";
            HSMtheoTM = Functions.GetDataToTable(SQL);
            dataGridViewHSMtheoTM.DataSource = HSMtheoTM;
            dataGridViewHSMtheoTM.Columns[0].HeaderText = " Mã hồ sơ mượn";
            dataGridViewHSMtheoTM.Columns[1].HeaderText = " Mã thẻ mượn ";
            dataGridViewHSMtheoTM.Columns[2].HeaderText = " Tạm ứng";
            dataGridViewHSMtheoTM.Columns[3].HeaderText = " Mã thủ thư";
            dataGridViewHSMtheoTM.Columns[4].HeaderText = " Ngày mượn";
            dataGridViewHSMtheoTM.AllowUserToAddRows = false;
            dataGridViewHSMtheoTM.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridViewHSMtheoTM.Columns[0].Width = 200;
            dataGridViewHSMtheoTM.Columns[1].Width = 200;
            dataGridViewHSMtheoTM.Columns[2].Width = 200;
            dataGridViewHSMtheoTM.Columns[3].Width = 200;
            dataGridViewHSMtheoTM.Columns[4].Width = 200;

        }
        private void ResetValues()
        {
            cbMaTheMuonBC.SelectedIndex = -1;
            dataGridViewHSMtheoTM.DataSource = null;

        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridViewHSMtheoTM.DataSource = null;
        }

        private void btnTimKiemBC_Click(object sender, EventArgs e)
        {
            Functions.Connect();

            if (cbMaTheMuonBC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mã thẻ mượn", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbMaTheMuonBC.Focus();
                return;
            }
            string SQL = " select MaHSM, TheMuon.MaTheMuon,  TamUng,MaThuThu , NgayMuon From TheMuon join HoSoMuon on TheMuon.MaTheMuon = HoSoMuon.MaTheMuon  Where 1=1";
            if (cbMaTheMuonBC.Text != "")
                SQL = SQL + " AND TheMuon.MaTheMuon Like N'%" + cbMaTheMuonBC.Text + "%'";
            HSMtheoTM = Functions.GetDataToTable(SQL);
            //dataGridViewHSMtheoTM.DataSource = HSMtheoTM;

            if (HSMtheoTM.Rows.Count == 0)
            {
                MessageBox.Show(" Không có bản ghi thỏa mãn điều kiện !");
                ResetValues();
            }
            else
            {
                MessageBox.Show(" Có " + HSMtheoTM.Rows.Count + " bản ghi thỏa mãn");
                dataGridViewHSMtheoTM.DataSource = HSMtheoTM;
            }
            //HSMtheoTM = Functions.GetDataToTable(SQL);
            dataGridViewHSMtheoTM.DataSource = HSMtheoTM;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook;
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable TM, HSM;
            //DataTable HSMtheoTM;
            // exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            //exSheet = exBook.Worksheets[1];

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5;
            exRange.Range[" A1: A1 "].ColumnWidth = 7;
            exRange.Range[" B1: B1 "].ColumnWidth = 15;
            exRange.Range[" A1: B1 "].MergeCells = true;
            exRange.Range[" A1: B1 "].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range[" A1: B1 "].Value = " Thư Viện HVNH ";
            exRange.Range[" A2: B2 "].MergeCells = true;
            exRange.Range[" A2: B2 "].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range[" A2: B2 "].Value = " Đống Đa - Hà Nội ";
            exRange.Range[" A3: B3 "].MergeCells = true;
            exRange.Range[" A3: B3 "].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range[" A3: B3 "].Value = " Điện thoại: (04)37562222 ";
            exRange.Range[" C2: E2 "].Font.Size = 16;
            exRange.Range[" C2: E2 "].Font.Name = " Times new roman";
            exRange.Range[" C2: E2 "].Font.Bold = true;
            exRange.Range[" C2: E2 "].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range[" C2: E2 "].MergeCells = true;
            exRange.Range[" C2: E2 "].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range[" C2: E2 "].Value = " DANH SÁCH HỒ SƠ MƯỢN ";

            sql = " SELECT MaTheMuon FROM TheMuon WHERE 1=1 and MaTheMuon Like N'%" + cbMaTheMuonBC.Text + "%'";
            TM = Functions.GetDataToTable(sql);
            exRange.Range[" B6: C9 "].Font.Size = 12;
            exRange.Range[" B6: C9 "].Font.Name = " Times new roman";
            exRange.Range[" B6: B6 "].Value = " Mã thẻ mượn : ";
            exRange.Range[" C6: E6 "].MergeCells = true;
            exRange.Range[" C6: E6 "].Value = TM.Rows[0][0].ToString();

            // sql = " SELECT MaHSM , MaTheMuon , TamUng , MaThuThu , NgayMuon  FROM HoSoMuon   WHERE MaTheMuon = N '" + cbMaTheMuonBC.Text + " ' ";
            //sql = " SELECT *  FROM HoSoMuon   WHERE MaTheMuon 1=1 ";
            sql = " select MaHSM, TheMuon.MaTheMuon,  TamUng,MaThuThu , NgayMuon From TheMuon join HoSoMuon on TheMuon.MaTheMuon = HoSoMuon.MaTheMuon  Where 1=1 and TheMuon.MaTheMuon Like N'%" + cbMaTheMuonBC.Text + "%' ";



            HSM = Functions.GetDataToTable(sql);

            exRange.Range[" A11: F11 "].Font.Bold = true;
            exRange.Range[" A11: F11 "].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range[" C11: F11 "].ColumnWidth = 20;
            exRange.Range[" A11: A11 "].Value = " STT ";
            exRange.Range[" B11: B11 "].Value = " Mã HSM";
            exRange.Range[" C11: C11 "].Value = " Mã thẻ mượn";
            exRange.Range[" D11: D11 "].Value = " Tạm Ứng";
            exRange.Range[" E11: E11 "].Value = " Mã thủ thư";
            exRange.Range[" F11: F11 "].Value = " Ngày mượn";

            for (hang = 0; hang <= HSM.Rows.Count - 1; hang++)
            {
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot <= HSM.Columns.Count - 1; cot++)
                {
                    exSheet.Cells[cot + 2][hang + 12] = HSM.Rows[hang][cot].ToString();
                }
            }
            exRange = exSheet.Cells[4][hang + 17];
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = DateTime.Now;
            exRange.Range["A1:C1"].Value = "Chùa Bộc - Đống Đa - Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;

            exSheet.Name = "1";
            exApp.Visible = true;
        }
    }
}
