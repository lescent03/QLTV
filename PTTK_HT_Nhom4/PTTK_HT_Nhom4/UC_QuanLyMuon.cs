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
    public partial class UC_QuanLyMuon : UserControl
    {
        Connector myDB = new Connector();


        public UC_QuanLyMuon()
        {
            InitializeComponent();
        }

        private void txt_SachID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                string sach_id = txt_SachID.Text.Trim();

                loadInfoSach(sach_id);
            }
        }

        void loadInfoSach(string sach_id)
        {
            try
            {
                myDB.openConnection();

                string sql = "SELECT s.TenSach, tl.TenTheLoai, tl.TenNganh " +
                    "FROM [dbo].[Sach] s JOIN [dbo].[The_Loai] tl ON s.MaTheLoai = tl.MaTheLoai " +
                    "WHERE s.MaSach = @sach_id AND s.TrangThai NOT LIKE  'Đang Sử Dụng'";

                using (SqlCommand cmd = new SqlCommand(sql , myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txt_TenSach.Text = reader["TenSach"].ToString();
                        txt_TenTheLoai.Text = reader["TenTheLoai"].ToString();
                        txt_TenNganh.Text = reader["TenNganh"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("LOI");
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string docgia_id = txt_MaDocGia.Text.Trim();

            string sach_id = txt_SachID.Text.Trim();

            int soluong_muon = (int)numericUpDown1.Value;

            

            if (docgia_id == "" || sach_id == "" || soluong_muon == 0)
            {
                MessageBox.Show("Vui lòng nhập mã độc giả hoặc mã sách hoặc xem số lượng mượn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!existDocGia(docgia_id))
            {
                MessageBox.Show("Độc giả này không tồn tại !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                insertPhieuMuon(docgia_id, sach_id, soluong_muon);
            }
        }

        void insertPhieuMuon(string docgia_id, string sach_id, int soluong)
        {




            try
            {
                myDB.openConnection();

                // Tạo DataTable để chứa danh sách sách
                DataTable dtSach = new DataTable();
                dtSach.Columns.Add("MaSach", typeof(string));
                dtSach.Columns.Add("SoLuong", typeof(int));

                // Thêm dữ liệu vào DataTable
                dtSach.Rows.Add(sach_id, soluong); // Giả sử mỗi sách mượn số lượng = 1

                // Tạo lệnh SQL để gọi stored procedure
                string sql = "EXEC [dbo].[TaoPhieuMuon] @MaTaiKhoan, @DanhSachSach";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    // Thêm tham số MaTaiKhoan
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", docgia_id);

                    // Thêm tham số kiểu table DanhSachSach
                    SqlParameter tableParam = new SqlParameter("@DanhSachSach", SqlDbType.Structured);
                    tableParam.Value = dtSach;
                    tableParam.TypeName = "DanhSachSachType"; // Đặt lại kiểu table đã tạo trong SQL Server
                    cmd.Parameters.Add(tableParam);

                    // Thực thi lệnh
                    int row_affected = (int)cmd.ExecuteNonQuery();

                    if (row_affected > 0)
                    {
                        MessageBox.Show("Thêm mới phiếu mượn thành công","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới phiếu mượn thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

                string sql = "SELECT COUNT(*) " +
                    " FROM [dbo].Tai_Khoan tk " +
                    "WHERE tk.MaTaiKhoan = @docgia_id";

                using (SqlCommand cmd = new SqlCommand(sql , myDB.getConnection))
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                myDB.closeConnection();
            }
        }
    }
}
