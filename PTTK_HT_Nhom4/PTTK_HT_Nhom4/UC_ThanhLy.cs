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
    public partial class UC_ThanhLy : UserControl
    {
        Connector myDB = new Connector();


        public UC_ThanhLy()
        {
            InitializeComponent();
        }

        private void UC_ThanhLy_Load(object sender, EventArgs e)
        {

            loadData();

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thanh lý sách này không", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result.Equals(DialogResult.Yes)) 
            {
                int selected_row = (int)e.RowIndex;

                DataGridViewRow row = guna2DataGridView1.Rows[selected_row];

                string sach_id = row.Cells["Mã Sách"].Value.ToString();

                updateSachStatus(sach_id);

                guna2DataGridView1.DataSource = null;

                loadData();


            }





        }
        void loadData()
        {
            try
            {
                myDB.openConnection();

                // Load danh sach sach len datagrid

                string sql = "SELECT s.MaSach, s.TenSach, tl.TenTheLoai, " +
                    "CASE " +
                    "WHEN tl.TenNganh IS NULL THEN N'Đại Cương' " +
                    "ELSE tl.TenNganh " +
                    "END as 'TenNganh' " +
                    ",s.NamXuatBan, s.TacGia " +
                    "FROM [dbo].[Sach] s JOIN [dbo].[The_Loai] tl on s.MaTheLoai = tl.MaTheLoai " +
                    "WHERE s.TrangThai = N'Đang Sử Dụng'";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;


                    DataTable dt = new DataTable();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(dt);

                    dt.Columns["MaSach"].ColumnName = "Mã Sách";
                    dt.Columns["TenSach"].ColumnName = "Tên Sách";
                    dt.Columns["TenTheLoai"].ColumnName = "Thể Loại";
                    dt.Columns["TenNganh"].ColumnName = "Ngành";
                    dt.Columns["NamXuatBan"].ColumnName = "Năm Xuất Bản";
                    dt.Columns["TacGia"].ColumnName = "Tác Giả";

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

        void updateSachStatus(string sach_id)
        {
            try
            {
                myDB.openConnection();

                string sql = "UPDATE s " +
                    "SET s.TrangThai = N'Đang Thanh Lý' " +
                    "FROM [dbo].[Sach] s " +
                    "WHERE s.MaSach = @sach_id";

                using (SqlCommand cmd = new SqlCommand(sql , myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@sach_id", sach_id);
                    int row_affected = (int)cmd.ExecuteNonQuery();

                    if (row_affected > 0) 
                    {
                        MessageBox.Show("Cập Nhật Thành Công","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập Nhật Thất Bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch(Exception ex)
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
