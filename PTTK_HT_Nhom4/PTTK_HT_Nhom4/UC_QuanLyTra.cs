using PTTK_HT_Nhom4.model;
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

namespace PTTK_HT_Nhom4
{
    public partial class UC_QuanLyTra : UserControl
    {
        


        Connector myDB = new Connector();

        
        //Biến rowindex dùng để trỏ hết hàng đang chọn trên datagridview
        int selectedrowindex = -1;

        public UC_QuanLyTra()
        {
            InitializeComponent();
        }

        

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                string sach_id = cmb_SachID.Text.Trim();

                if (existSach(sach_id))
                {
                    fillDocGiaCmbbox(sach_id);
                }
            }
        }

        bool existSach(string sach_id) 
        {
            try
            {
                myDB.openConnection();

                string sql = "SELECT COUNT(*) FROM Sach WHERE MaSach = @sach_id";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection)) 
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                myDB.closeConnection();
            }
        }
        void fillDocGiaCmbbox(string sach_id)
        {
            cmd_DocGiaID.Items.Clear();
            try
            {
                myDB.openConnection();

                string sql = "SELECT DISTINCT pm.MaTaiKhoan" +
                    " FROM [dbo].[Phieu_Muon] pm JOIN [dbo].[Chi_Tiet_Phieu_Muon] pm_detail ON pm.MaPhieuMuon = pm_detail.MaPhieuMuon" +
                    " WHERE pm_detail.MaSach = @sach_id AND pm_detail.Status = 0";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string docgia_id = reader["MaTaiKhoan"].ToString();
                        cmd_DocGiaID.Items.Add(docgia_id);
                    }
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

        private void cmb_SachID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmd_DocGiaID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string docgia_id = cmd_DocGiaID.Text.ToString();
                string sach_id = cmb_SachID.Text.ToString();

                if (existDocGia(docgia_id))
                {
                    fillSachCmbbox(docgia_id);

                    if (sach_id != "")
                    {
                        fillInfoSach(docgia_id,sach_id);
                    }
                }
                
               
            }
        }

        void fillInfoSach(string docgia_id,string sach_id)
        {
            try
            {
                myDB.openConnection();

                string sql = "SELECT s.TenSach, pm.NgayMuon,pm.HanTra, pm_detail.SoLuong" +
                    " FROM [dbo].[Phieu_Muon] pm JOIN [dbo].[Chi_Tiet_Phieu_Muon] pm_detail ON pm.MaPhieuMuon = pm_detail.MaPhieuMuon" +
                    " JOIN [dbo].[Sach] s ON s.MaSach = pm_detail.MaSach" +
                    " WHERE pm_detail.MaSach = @sach_id AND pm.MaTaiKhoan = @docgia_id AND pm.NgayTra IS NULL";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);
                    cmd.Parameters.AddWithValue("@docgia_id", docgia_id);

                    

                    DataTable dt = new DataTable(); 
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    dt.Columns["TenSach"].ColumnName = "Tên Sách";
                    dt.Columns["SoLuong"].ColumnName = "Số Lượng";
                    dt.Columns["NgayMuon"].ColumnName = "Ngày Mượn";
                    dt.Columns["HanTra"].ColumnName = "Hạn Trả";


                    foreach (DataGridViewColumn col in guna2DataGridView1.Columns)
                    {
                        
                        if (col.Name == "Ngày Mượn" || col.Name == "Hạn Trả")
                        {
                            col.DefaultCellStyle.Format = "dd/MM/yyyy";
                        }
                    }

                    guna2DataGridView1.DataSource = dt;
                    
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

        void fillSachCmbbox(string docgia_id)
        {
            cmb_SachID.Items.Clear();
            try
            {
                myDB.openConnection();

                string sql = "SELECT DISTINCT pm_detail.MaSach" +
                             " FROM [dbo].[Phieu_Muon] pm JOIN [dbo].[Chi_Tiet_Phieu_Muon] pm_detail ON pm.MaPhieuMuon = pm_detail.MaPhieuMuon" +
                             " WHERE pm.MaTaiKhoan = @docgia_id AND pm_detail.Status = 0";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@docgia_id", docgia_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        string sach_id = reader["MaSach"].ToString();

                        cmb_SachID.Items.Add(sach_id);
                    }
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

        bool existDocGia(string docgia_id)
        {
            try
            {
                myDB.openConnection();

                string sql = "SELECT COUNT(*) FROM Tai_Khoan WHERE MaTaiKhoan = @docgia_id";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@docgia_id", docgia_id);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                myDB.closeConnection();
            }
        }

        private void UC_QuanLyTra_Load(object sender, EventArgs e)
        {

        }

        private void btn_XacNhanTra_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = guna2DataGridView1.Rows[selectedrowindex];

            int soluong_muon = (int)row.Cells["Số Lượng"].Value;

            int soluong_tra = (int)numeric_soluong.Value;

            

            string docgia_id = cmd_DocGiaID.Text.Trim();

            string sach_id = cmb_SachID.Text.Trim();

            if (soluong_tra > soluong_muon)
            {
                MessageBox.Show("Số lượng không hợp lệ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                updateSoLuongTra(docgia_id, sach_id, soluong_tra);
            }

        }

        void updateSoLuongTra(string docgia_id, string sach_id, int soluong)
        {
            try
            {
                myDB.openConnection();

                string sql = "UPDATE pm_detail" +
                    " SET pm_detail.SoLuongTra = pm_detail.SoLuongTra + @soluong" +
                    " FROM [dbo].[Chi_Tiet_Phieu_Muon] pm_detail " +
                    " WHERE pm_detail.MaSach = @sach_id AND pm_detail.Status = 0 AND pm_detail.MaPhieuMuon  IN " +
                    "( SELECT pm.MaPhieuMuon FROM [dbo].[Phieu_Muon] pm " +
                    "WHERE pm.MaTaiKhoan = @docgia_id AND pm.Status = 0 )";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);

                    cmd.Parameters.AddWithValue("@soluong", soluong);

                    cmd.Parameters.AddWithValue("@docgia_id", docgia_id);

                    int row_affected = cmd.ExecuteNonQuery();

                    if (row_affected > 0) 
                    {
                        MessageBox.Show("Cập Nhật Phiếu Mượn Thành Công", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập Nhật Phiếu Mượn Thất Bại", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

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

        private void guna2DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedrowindex = e.RowIndex;
        }

        private void btn_Phat_Click(object sender, EventArgs e)
        {
            string docgia_id = cmd_DocGiaID.Text.Trim();

            string sach_id = cmb_SachID.Text.Trim();

            bool is_trehan = false;

            // Xử lý lỗi nếu user các ô text box chưa có giá trị

            if (docgia_id == "" || sach_id == "")
            {
                MessageBox.Show("Vui Lòng Nhập Mã Sách và Độc Giả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                myDB.openConnection();

                string sql = "SELECT DISTINCT  pm.MaPhieuMuon,pm.NgayMuon,pm.NgayTra, pm.HanTra,pm.MaTaiKhoan,pm.Status " +
                    "FROM [dbo].[Chi_Tiet_Phieu_Muon] pm_detail JOIN [dbo].[Phieu_Muon] pm ON pm.MaPhieuMuon = pm_detail.MaPhieuMuon " +
                    "WHERE pm_detail.MaSach = @sach_id AND pm.MaTaiKhoan = @docgia_id";

                using (SqlCommand cmd = new SqlCommand(sql , myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);

                    cmd.Parameters.AddWithValue("@docgia_id", docgia_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) 
                    {
                        Phieumuon pm = new Phieumuon();



                        if (reader["NgayTra"] != DBNull.Value)
                        {
                            pm.maPhieuMuon = reader["MaPhieuMuon"].ToString().Trim();
                            pm.ngayMuon = Convert.ToDateTime(reader["NgayMuon"]);
                            pm.ngayTra = Convert.ToDateTime(reader["NgayTra"]);
                            pm.hanTra = Convert.ToDateTime(reader["HanTra"]);
                            pm.maDocGia = reader["MaTaiKhoan"].ToString().Trim();
                            pm.trangThai = reader["Status"].ToString().Trim();


                            if (pm.ngayTra > pm.hanTra)
                            {
                                is_trehan = true;
                            }

                            Frm_Phat frm = new Frm_Phat(pm.maDocGia, pm.maPhieuMuon, pm.ngayTra, pm.hanTra, is_trehan);
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Độc giả này chưa trả sách này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }







                    }



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
