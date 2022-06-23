using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace quan_ly_thu_vien
{
    internal class Functions
    {
        public static SqlConnection con;  //Khai báo đối tượng kết nối
        public static string connString;   //Khai báo biến chứa chuỗi kết nối

        public static void Connect()
        {//Thiết lập giá trị cho chuỗi kết nối
            connString = "Data Source=LAPTOP-OV1HU3M9\\SQLEXPRESS;" + "Initial Catalog=quan_ly_thu_vien1;" + "Integrated Security=True";
            con = new SqlConnection();         		//Cấp phát đối tượng
            con.ConnectionString = connString; 		//Kết nối
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Close()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            return table;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;    // Truong gia tri
            cbo.DisplayMember = ten;    // Truong hien thi
        }
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static void RunSQL(string SQL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = SQL;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static string GetFieldValues(string sql)
        {
            string value = "";
            SqlCommand sc = new SqlCommand(sql, con);
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                value = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            return value;
        }
        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Functions.con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Dữ liệu đang được dùng, không thể xóa...", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
        }
   
        public static string ConvertDateTime(string d)
        {
            string[] parts = d.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dt;
        }
        public static bool Login(string userName, string userPassword)
        {
            Connect();
            DataTable tbl = new DataTable();
            string query;


            query = "SELECT * FROM DangNhap WHERE TenDangNhap = N'" + userName + "' AND MatKhau = N'" + userPassword + "'"; //-- SQL Injection
                                                                                                                            //try
                                                                                                                            //{
            tbl = GetDataToTable(query);


            if (tbl.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsDate(string d)
        {
            string[] parts = d.Split('/');
            int date, month, year;
            date = (Convert.ToInt32(parts[0]));
            month = (Convert.ToInt32(parts[1]));
            year = (Convert.ToInt32(parts[2]));
            if (date >= 1 && date <= 31 &&
                month >= 1 && month <= 12 &&
                year >= 1900)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            // Xoa cac dau "," neu co
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // Vi thu tu di tu 0
            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];

                if (mLen == i) // Chu so cuoi khong xet tiep
                    break;

                switch ((mLen - i) % 9)
                {
                    case 0:
                        mTemp = mTemp + " tỷ";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 6:
                        mTemp = mTemp + " triệu";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 3:
                        mTemp = mTemp + " nghìn";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    default:
                        switch ((mLen - i) % 3)
                        {
                            case 2:
                                mTemp = mTemp + " trăm";
                                break;
                            case 1:
                                mTemp = mTemp + " mươi";
                                break;
                        }
                        break;
                }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", "");
            //Loại bỏ trường hợp 00x
            mTemp = mTemp.Replace("không mươi ", "linh ");
            //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";
            return mTemp;
        }
        public static string CreateKey(string tiento)
        {
            string key = tiento;

            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            string d = string.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            // 15/12/2021 -> {"15", "12", "2021"} -> 15122021
            key += d;

            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');

            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];

            // Xoa Space & AM hoac PM
            partsTime[2] = partsTime[2].Remove(2, 3);

            string t;
            t = String.Format("{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            // 5:12:02 AM -> {"5", "12", "02 AM"} -> 051202
            key = key + t;

            return key;
        }
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            if (Convert.ToInt32(hour) == 12)
            {
                h = 0.ToString();
            }
            else
            {
                for (int i = 1; i < 12; i++)
                {
                    if (Convert.ToInt32(hour) == i)
                    {
                        h = (12 + i).ToString();
                        break;
                    }
                }
            }
            return h;
        }
    }
}
