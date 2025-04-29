using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI;
using System.Windows.Forms;

namespace PTTK_HT_Nhom4
{
    public partial class UC_QuanLy : UserControl
    {
        
        UC_QuanLyMuon uc_muon = new UC_QuanLyMuon();
        UC_QuanLyTra uc_tra = new UC_QuanLyTra();

        public UC_QuanLy()
        {
            InitializeComponent();
        }

        private void btn_muon_Click(object sender, EventArgs e)
        {
            uc_tra.Visible = false;
            uc_muon.Visible = true;
            
        }

        private void UC_QuanLy_Load(object sender, EventArgs e)
        {
            //Thực hiện gán UC vào container cha

            uc_muon.Dock = DockStyle.Fill;
            uc_tra.Dock = DockStyle.Fill;

            uc_muon.Visible = false;
            uc_tra.Visible = false;

            container1.Controls.Add(uc_muon);
            container1.Controls.Add(uc_tra);

            

   

        }

        private void btn_tra_Click(object sender, EventArgs e)
        {
            uc_muon.Visible = false;
            uc_tra.Visible = true;

        }
    }
}
