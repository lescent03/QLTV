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
    public partial class UC_Phat : UserControl
    {
        Connector mydb = new Connector();

        private string docgia_id;
        private string phieumuon_id;
        private DateTime ngaytra;
        private DateTime hantra;
        private bool is_trehan;

        public UC_Phat(string docgia_id, string phieumuon_id, DateTime ngaytra, DateTime hantra, bool is_trehan)
        {
            InitializeComponent();


            this.docgia_id = docgia_id;
            this.phieumuon_id = phieumuon_id;
            this.ngaytra = ngaytra;
            this.hantra = hantra;
            this.is_trehan = is_trehan;
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UC_Phat_Load(object sender, EventArgs e)
        {

        }

        private void btn_Phat_Click(object sender, EventArgs e)
        {
            int tien_phat = 0;

            if (chbox_noptre.Checked) 
            {
                if (is_trehan)
                {
                    tien_phat += tinhTienPhat(ngaytra, hantra);
                }
                else
                {
                    MessageBox.Show("Độc giả này trả sách đúng hạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (chbox_SachHong.Checked)
                {
                    int giatri_sach = Convert.ToInt32(txt_tienSach.Text.ToString().Trim());
                    tien_phat += giatri_sach;
                }

                string mo_ta = txt_LyDo.Text.ToString();

                insert_PhieuPhat(phieumuon_id, ngaytra, tien_phat, mo_ta); 

            }

        }

        void insert_PhieuPhat(string phieumuon_id, DateTime ngaytra, int tien_phat, string mota)
        {
            try
            {
                mydb.openConnection();

                string sql = "INSERT INTO [dbo].[Phieu_Phat] " +
                    "([MaPhieuMuon],[NgayTra], [PhiPhat],  [MoTa])" +
                    " VALUES (@pm_id, @ngaytra, @phiphat, @mota)";

                using (SqlCommand cmd = new SqlCommand(sql , mydb.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@pm_id", phieumuon_id);

                    cmd.Parameters.AddWithValue("@ngaytra", ngaytra);

                    cmd.Parameters.AddWithValue("@phiphat", tien_phat);

                    cmd.Parameters.AddWithValue("@mota", mota);

                    int row_affected = (int)cmd.ExecuteNonQuery();

                    if (row_affected > 0) 
                    {
                        MessageBox.Show("Thêm Mới Phiếu Phạt Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thêm Mới Phiếu Phạt Thất Bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }

        }

        int tinhTienPhat(DateTime ngaytra, DateTime hantra)
        {
            TimeSpan chenh_lech = ngaytra - hantra;
            return (int)chenh_lech.TotalDays * 4000;


        }
    }
}
