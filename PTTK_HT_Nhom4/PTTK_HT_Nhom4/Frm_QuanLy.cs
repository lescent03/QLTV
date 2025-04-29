using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PTTK_HT_Nhom4
{
    public partial class Frm_QuanLy : Form
    {
        public Frm_QuanLy()
        {
            InitializeComponent();
        }

        private void Frm_QuanLy_Load(object sender, EventArgs e)
        {
            UC_QuanLy uc = new UC_QuanLy();
            uc.Dock = DockStyle.Fill;
        
            this.Controls.Add(uc);
        }
    }
}
