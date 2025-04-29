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
    public partial class Frm_ThongKe : Form
    {
        public Frm_ThongKe()
        {
            InitializeComponent();
        }

        private void Frm_ThongKe_Load(object sender, EventArgs e)
        {
            UC_ThongKe uc = new UC_ThongKe();
            uc.Dock = DockStyle.Fill;

            this.Controls.Add(uc);
        }
    }
}
