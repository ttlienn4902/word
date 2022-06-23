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
    public partial class frmbaocao7 : Form
    {
        public frmbaocao7()
        {
            InitializeComponent();
        }

        DataTable Timkiem;

        private void frmbaocao7_Load(object sender, EventArgs e)
        {
            ResetValues();
            data_GV.DataSource = null;
            UpdateYear();
            //  this.reportViewer1.RefreshReport();
        }
        private void ResetValues()
        {
            foreach (RadioButton rdo in groupBox1.Controls)
            {
                rdo.Checked = false;
            }

            foreach (Control cbo in groupBox2.Controls)
            {
                if (cbo is ComboBox)
                {
                    ComboBox cbo1 = (ComboBox)cbo;
                    cbo1.SelectedIndex = -1;
                    cbo1.Enabled = false;
                }
            }
            data_GV.DataSource = null;
            txtDT.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (!rdoNam.Checked && !rdoQuy.Checked && !rdoThang.Checked)
            {
                MessageBox.Show("Hãy chọn một tùy chọn đi!");
                return;
            }

            string sql;
            // sql = "SELECT * FROM HoSoTra WHERE 1=1 ";
            sql = "  select a.MaSach ,  c.ngaymuon , b.ngaytra , b.tongtien from hosotra b join chitiethst a on b.mahst = a.mahst join hosomuon c on b.mahsm = c.mahsm where 1 = 1";

            if (rdoThang.Checked)
            {
                if (cboThang.SelectedIndex == -1 || cboNam.SelectedIndex == -1)
                {
                    MessageBox.Show("Hãy chọn đầy đủ tháng và năm!");
                    return;
                }

                sql = sql + " AND MONTH(NgayTra) = " + cboThang.Text + " AND YEAR(NgayTra) = " + cboNam.Text + "";
            }

            if (rdoQuy.Checked)
            {
                if (cboQuy.SelectedIndex == -1 || cboNam.SelectedIndex == -1)
                {
                    MessageBox.Show("Hãy chọn đầy đủ quý và năm!");
                    return;
                }

                switch (cboQuy.SelectedIndex)
                {
                    case 0:
                        {
                            sql = sql + " AND MONTH(NgayTra) BETWEEN 1 AND 3";
                            break;
                        }
                    case 1:
                        {
                            sql = sql + " AND MONTH(NgayTra) BETWEEN 4 AND 6";
                            break;
                        }
                    case 2:
                        {
                            sql = sql + " AND MONTH(Ngaytra) BETWEEN 7 AND 9";
                            break;
                        }
                    case 3:
                        {
                            sql = sql + " AND MONTH(NgayTra) BETWEEN 10 AND 12";
                            break;
                        }
                }
            }


            if (rdoNam.Checked)
            {
                if (cboNam.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn chưa chọn năm");
                    return;
                }

                sql = sql + " AND YEAR(NgayTra) = " + cboNam.Text + "";
            }


            sql = sql + " AND YEAR(NgayTra) = " + cboNam.Text + "";
            Timkiem = Functions.GetDataToTable(sql);

            if (Timkiem.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện ");
                ResetValues();
            }
            else
            {
                MessageBox.Show("Có " + Timkiem.Rows.Count + " bản ghi thỏa mãn điều kiện ");
                data_GV.DataSource = Timkiem;
                Load_DGV();
                UpdateReverage();
            }
        }
        private void Load_DGV()
        {
            //string[] Header = new string[6] { "MaHST", "MaHSM", "NgayTra", "TongTien", "SoTienThanhToan","MaThuTHu" };
            string[] Header = new string[4] { "MaSach", "NgayMuon", "NgayTra", "TongTien" };
            for (int i = 0; i < Header.Length; i++)
            {
                data_GV.Columns[i].HeaderText = Header[i];
            }
            data_GV.AllowUserToAddRows = false;
            data_GV.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void UpdateYear()
        {
            DataTable tblYEAR;
            string sql;
            sql = "SELECT YEAR(NgayTra) FROM HoSoTra group by year(ngaytra)";
            tblYEAR = Functions.GetDataToTable(sql);
            for (int i = 0; i < tblYEAR.Rows.Count; i++)
            {

                cboNam.Items.Add(tblYEAR.Rows[i][0].ToString());

            }
            cboNam.Sorted = true;
        }

        private void UpdateReverage()
        {
            double tien = 0;

            for (int i = 0; i < Timkiem.Rows.Count; i++)
            {
                tien += Convert.ToDouble(Timkiem.Rows[i][3].ToString());
            }
            txtDT.Text = tien.ToString();
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            data_GV.DataSource = null;
        }

        private void rdoThang_CheckedChanged(object sender, EventArgs e)
        {
            cboThang.Enabled = true;
            cboNam.Enabled = true;
            cboQuy.Enabled = false;
        }

        private void rdoQuy_CheckedChanged(object sender, EventArgs e)
        {
            cboThang.Enabled = false;
            cboNam.Enabled = true;
            cboQuy.Enabled = true;
        }

        private void rdoNam_CheckedChanged(object sender, EventArgs e)
        {
            cboThang.Enabled = false;
            cboNam.Enabled = true;
            cboQuy.Enabled = false;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            /*KetNoi kn = new KetNoi();
            DataTable dt = new DataTable();
            if (rdoThang.Checked)
            {
                dt = kn.LoadData("yc7_month '"+cboThang.Text+"','"+cboNam.Text+"'");
            }
            if( rdoQuy.Checked)
            {
                dt = kn.LoadData("yc7_quater '" + cboQuy.Text + "','" + cboNam.Text + "'");
            }
            if(rdoNam.Checked)
            {
                dt = kn.LoadData("yc7_year '" + cboNam.Text + "' ");
            }
           
            if (dt.Rows.Count == 0)
                MessageBox.Show("Không có dữ liệu để xuất");
            else
            {
                kn.OpenConnection();
                if (rdoThang.Checked)A
                {
                     kn.LoadDataSet("yc7_month '" + cboThang.Text + "','" + cboNam.Text + "'").WriteXml(@"D:\Thong ke doanh thu.xls");
                }
                if (rdoQuy.Checked)
                {
                     kn.LoadDataSet("yc7_quater '" + cboQuy.Text + "','" + cboNam.Text + "'").WriteXml(@"D:\Thong ke doanh thu.xls");
                }
                if (rdoNam.Checked)
                {
                    kn.LoadDataSet("yc7_year '" + cboNam.Text + "' ").WriteXml(@"D:\Thong ke doanh thu.xls");
                }
               // kn.LoadDataSet("proc_HienThiDSVaccine").WriteXml(@"E:\Danh sách vaccine.xls");
                MessageBox.Show("Xuất excel thành công");*/
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; // Excel 1 - n Workbook
            COMExcel.Worksheet exSheet; // Workbook 1 - n Worksheet
            COMExcel.Range exRange;

            string sql;
            int hang = 0, cot = 0;
            DataTable tblDT;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];

            //Dinh dang chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times New Roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5;
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Báo cáo doanh thu thư viện";

            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Chùa Bộc - Đống Đa - Hà Nội";

            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 84 918 xxx xxx";

            exRange.Range["D2:I2"].Font.Size = 16;
            exRange.Range["D2:I2"].Font.Name = "Times New Roman";
            exRange.Range["D2:I2"].Font.Bold = true;
            exRange.Range["D2:I2"].Font.ColorIndex = 3;
            exRange.Range["D2:I2"].MergeCells = true;
            exRange.Range["D2:I2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            string header;
            header = "Báo cáo Doanh thu ";
            if (rdoThang.Checked)
            {
                header += "tháng " + cboThang.Text + " năm " + cboNam.Text + "";
            }
            if (rdoQuy.Checked)
            {
                header += "quý " + cboQuy.Text + " năm " + cboNam.Text + "";
            }
            if (rdoNam.Checked)
            {
                header += "năm  " + cboNam.Text + "";
            }
            exRange.Range["D2:I2"].Value = header;

            sql = " select a.MaSach ,  c.ngaymuon , b.ngaytra , b.tongtien from hosotra b join chitiethst a on b.mahst = a.mahst join hosomuon c on b.mahsm = c.mahsm where 1 = 1 ";
            if (rdoThang.Checked)
            {
                sql = sql + " AND MONTH(Ngaytra) = " + cboThang.Text + " AND YEAR(Ngaytra) = " + cboNam.Text + "";
            }

            if (rdoQuy.Checked)
            {
                switch (cboQuy.SelectedIndex)
                {
                    case 0:
                        {
                            sql = sql + " AND MONTH(Ngaytra) BETWEEN 1 AND 3";
                            break;
                        }
                    case 1:
                        {
                            sql = sql + " AND MONTH(Ngaytra) BETWEEN 4 AND 6";
                            break;
                        }
                    case 2:
                        {
                            sql = sql + " AND MONTH(Ngaytra) BETWEEN 7 AND 9";
                            break;
                        }
                    case 3:
                        {
                            sql = sql + " AND MONTH(Ngaytra) BETWEEN 10 AND 12";
                            break;
                        }
                }
            }

            if (rdoNam.Checked)
            {
                sql = sql + " AND YEAR(Ngaytra) = " + cboNam.Text + "";
            }
            tblDT = Functions.GetDataToTable(sql);
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Mã sách";
            exRange.Range["C11:C11"].Value = "Ngày mượn";
            exRange.Range["C11:C11"].ColumnWidth = 20;
            exRange.Range["D11:D11"].Value = "Ngày Trả";
            exRange.Range["D11:D11"].ColumnWidth = 20;
            exRange.Range["E11:E11"].Value = "Tổng Tiền";



            for (hang = 0; hang < tblDT.Rows.Count; hang++)
            {
                // Điền số thứ tự vào cột
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblDT.Columns.Count; cot++)
                {
                    // Điền thông tin hàng từ cột 2, dòng 12
                    exSheet.Cells[cot + 2][hang + 12] = tblDT.Rows[hang][cot].ToString();
                }
            }
            int tmp;
            tmp = hang;

            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = txtDT.Text;

            exRange = exSheet.Cells[4][hang + 15];
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            string tien = txtDT.Text;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChu(tien);

            exRange = exSheet.Cells[4][hang + 17];
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            //DateTime d = Convert.ToDateTime(tblDT.Rows[0][3]);
            //exRange.Range["A1:C1"].Value = "Chùa Bộc, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            DateTime d = DateTime.Now;
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exSheet.Name = "1";
            exApp.Visible = true;
        }
    }
}

    
