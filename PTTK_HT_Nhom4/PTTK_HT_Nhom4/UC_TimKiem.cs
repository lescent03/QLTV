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
    public partial class UC_TimKiem : UserControl
    {
     
        Connector conc = new Connector();
        public UC_TimKiem()
        {
            
            InitializeComponent();
        }

        private void UC_TimKiem_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            string sql = "SELECT s.MaSach as 'Mã Sách', s.TenSach as 'Tên Sách', s.NamXuatBan as 'Năm Xuất Bản' " +
                ", tl.TenTheLoai as 'Thể Loại', tl.TenNganh as 'Ngành'" +
                "FROM Sach s JOIN The_Loai tl ON s.MaTheLoai = tl.MaTheLoai " +
                "WHERE s.TenSach LIKE @key_word OR tl.TenTheLoai LIKE @key_word";
            string key_word = txt_TimKiem.Text;
            try
            {
                conc.openConnection();

                if (key_word == "")
                {
                    MessageBox.Show("Vui lòng nhập từ khóa", "Thông báo");
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conc.getConnection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@key_word", '%' + key_word + '%');


                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                      
                        guna2DataGridView1.DataSource = dataTable;


                    }
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                conc.closeConnection(); 
            }

        }
    }
}
