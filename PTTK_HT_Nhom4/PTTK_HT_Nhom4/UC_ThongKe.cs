using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PTTK_HT_Nhom4
{
    public partial class UC_ThongKe : UserControl
    {
        Connector myDB = new Connector();

        private bool chart_flag;
        private bool datagrid_flag;
        public UC_ThongKe()
        {
            InitializeComponent();
            this.chart_flag = false;
            this.datagrid_flag = false;
        }

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear(); // Nếu muốn xóa series cũ
            dataGridView1.DataSource = null;


            chart1.Dock = DockStyle.Fill;
            dataGridView1.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(chart1);
            this.panel1.Controls.Add(dataGridView1);

            chart1.Visible = false;
            dataGridView1.Visible = false;
            //Series series = new Series("Sản lượng");
            //series.ChartType = SeriesChartType.Line; // Column, Pie, Bar...

            //series.Points.AddXY("T1", 120);
            //series.Points.AddXY("T2", 150);
            //series.Points.AddXY("T3", 180);

            //chart1.Series.Add(series);
        }

        private void cbb_TieuChi_SelectedValueChanged(object sender, EventArgs e)
        {



        }

        void loadDataToChart_Muon()
        {

            //Xử lý lọc năm học và học kỳ
            string hocky = cbb_HocKy.Text.ToString();

            string namhoc = cbb_namhoc.Text.ToString();

            if (hocky == "HK1")
            {
                namhoc = namhoc.Split('-')[0].ToString();
            }
            else
            {
                namhoc = namhoc.Split('-')[1].ToString();
            }



            try
            {
                myDB.openConnection();

                string sql = "SELECT FORMAT(pm.NgayMuon, 'MM-yyyy') AS ThangMuon, YEAR(pm.NgayMuon) AS Nam,  SUM(ct.SoLuong) AS TongSachMuon " +
                    "FROM  Phieu_Muon pm JOIN Chi_Tiet_Phieu_Muon ct ON pm.MaPhieuMuon = ct.MaPhieuMuon " +
                    "GROUP BY FORMAT(pm.NgayMuon, 'MM-yyyy'), YEAR(pm.NgayMuon) " +
                    "ORDER BY  Nam, ThangMuon;";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);


                    // Xử lý chart
                    chart1.Series.Clear();
                    Series seriesMuon = new Series("Sách mượn theo tháng");
                    seriesMuon.ChartType = SeriesChartType.Column;

                    // Đẩy data lên chart
                    foreach (DataRow dr in dt.Rows)
                    {
                        string thang = int.Parse(dr["ThangMuon"].ToString().Split('-')[0]).ToString();

                        string nam = int.Parse(dr["ThangMuon"].ToString().Split('-')[1]).ToString();


                        if (nam == namhoc)
                        {
                            if (hocky == "HK1")
                            {


                                int thangInt = int.Parse(thang);
                                // Chỉ xử lý những tháng từ 9 đến 12
                                if (thangInt >= 9 && thangInt <= 12)
                                {
                                    int tong = Convert.ToInt32(dr["TongSachMuon"]);
                                    seriesMuon.Points.AddXY(thang, tong);
                                }
                            }
                            else if (hocky == "HK2")
                            {
                                MessageBox.Show(nam, namhoc);
                                int thangInt = int.Parse(thang);
                                // Chỉ xử lý những tháng từ 1 đến 5
                                if (thangInt >= 1 && thangInt <= 5)
                                {
                                    int tong = Convert.ToInt32(dr["TongSachMuon"]);
                                    seriesMuon.Points.AddXY(thang, tong);
                                }
                            }
                            else
                            {
                                int thangInt = int.Parse(thang);
                                // Chỉ xử lý những tháng từ 1 đến 5
                                if (thangInt >= 6 && thangInt <= 8)
                                {
                                    int tong = Convert.ToInt32(dr["TongSachMuon"]);
                                    seriesMuon.Points.AddXY(thang, tong);
                                }
                            }
                        }

                    }
                    chart1.Series.Add(seriesMuon);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                myDB.closeConnection();
            }

        }

        private void btn_ThongKe_Click(object sender, EventArgs e)
        {
            string tieu_chi = cbb_TieuChi.Text.ToString();

            string namhoc = cbb_namhoc.Text.ToString();

            string hocky = cbb_HocKy.Text.ToString();

            if (namhoc == "" || hocky == "")
            {
                MessageBox.Show("Vui lòng chọn năm học hoặc học kỳ thích hợp", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                if (tieu_chi == "Tổng Số Sách Mượn Theo Tháng")
                {
                    datagrid_flag = false;
                    chart_flag = true;

                    loadDataToChart_Muon();

                }
                else if (tieu_chi == "Tổng Số Sách Trả Theo Tháng")
                {
                    datagrid_flag = false;
                    chart_flag = true;

                    loadDataToChart_Tra();
                }
                else if (tieu_chi == "Sách Được Mượn Nhiều Nhất")
                {
                    datagrid_flag = true;
                    chart_flag = false;

                    loadDataToGrid_SachMuonNhieuNhat();
                }
                else
                {

                    // Xử lý các tiêu chí sử dụng datagrid
                    datagrid_flag = true;
                    chart_flag = false;
                }
            }


            if (datagrid_flag)
            {
                chart1.Visible = false;
                dataGridView1.Visible = true;
            }
            else if (chart_flag)
            {
                chart1.Visible = true;
                dataGridView1.Visible = false;
            }

        }

        void loadDataToGrid_SachMuonNhieuNhat()
        {

            try
            {

                myDB.openConnection();

                string sql = "SELECT TOP 1 s.TenSach,tl.TenNganh, tl.TenTheLoai,s.NamXuatBan,SUM(pm_detail.SoLuong) as 'SoLuong' " +
                    "FROM [dbo].[Chi_Tiet_Phieu_Muon] pm_detail JOIN [dbo].[Sach] s ON s.MaSach = pm_detail.MaSach JOIN [dbo].[The_Loai] tl ON s.MaTheLoai = tl.MaTheLoai " +
                    "GROUP BY s.TenSach,s.NamXuatBan,tl.TenNganh, tl.TenTheLoai " +
                    "ORDER BY SUM(pm_detail.SoLuong) DESC";

                using (SqlCommand cmd = new SqlCommand(sql , myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    DataTable dt = new DataTable();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myDB.closeConnection();
            }

        }

        void loadDataToChart_Tra()
        {

            //Xử lý lọc năm học và học kỳ
            string hocky = cbb_HocKy.Text.ToString();

            string namhoc = cbb_namhoc.Text.ToString();

            if (hocky == "HK1")
            {
                namhoc = namhoc.Split('-')[0].ToString();
            }
            else
            {
                namhoc = namhoc.Split('-')[1].ToString();
            }



            try
            {
                myDB.openConnection();

                string sql = "SELECT FORMAT(pm.NgayTra, 'MM-yyyy') AS ThangTra, SUM(ct.SoLuongTra) AS TongSachTra " +
                    "FROM Phieu_Muon pm JOIN Chi_Tiet_Phieu_Muon ct ON pm.MaPhieuMuon = ct.MaPhieuMuon " +
                    "WHERE pm.NgayTra IS NOT NULL " +
                    "GROUP BY FORMAT(pm.NgayTra, 'MM-yyyy') " +
                    "ORDER BY ThangTra;";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);


                    // Xử lý chart
                    chart1.Series.Clear();
                    Series seriesMuon = new Series("Sách mượn theo tháng");
                    seriesMuon.ChartType = SeriesChartType.Column;

                    // Đẩy data lên chart
                    foreach (DataRow dr in dt.Rows)
                    {
                        string thang = int.Parse(dr["ThangTra"].ToString().Split('-')[0]).ToString();

                        string nam = int.Parse(dr["ThangTra"].ToString().Split('-')[1]).ToString();


                        if (nam == namhoc)
                        {
                            if (hocky == "HK1")
                            {
                                int thangInt = int.Parse(thang);
                                // Chỉ xử lý những tháng từ 9 đến 12
                                if (thangInt >= 9 && thangInt <= 12)
                                {
                                    int tong = Convert.ToInt32(dr["TongSachTra"]);
                                    seriesMuon.Points.AddXY(thang, tong);
                                }
                            }
                            else if (hocky == "HK2")
                            {
                               
                                int thangInt = int.Parse(thang);
                                // Chỉ xử lý những tháng từ 1 đến 5
                                if (thangInt >= 1 && thangInt <= 5)
                                {
                                    int tong = Convert.ToInt32(dr["TongSachTra"]);
                                    seriesMuon.Points.AddXY(thang, tong);
                                }
                            }
                            else
                            {
                                int thangInt = int.Parse(thang);
                                // Chỉ xử lý những tháng từ 1 đến 5
                                if (thangInt >= 6 && thangInt <= 8)
                                {
                                    int tong = Convert.ToInt32(dr["TongSachTra"]);
                                    seriesMuon.Points.AddXY(thang, tong);
                                }
                            }
                        }

                    }
                    chart1.Series.Add(seriesMuon);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                myDB.closeConnection();
            }

        }


    }
}


