using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTK_HT_Nhom4
{
    public partial class Frm_Test : Form
    {
        public Frm_Test()
        {
            InitializeComponent();
        }

        private void Frm_Test_Load(object sender, EventArgs e)
        {
            UC_DangNhap uc = new UC_DangNhap();
            uc.Dock = DockStyle.Fill;
            this.Controls.Add(uc);
        }
    }
}
