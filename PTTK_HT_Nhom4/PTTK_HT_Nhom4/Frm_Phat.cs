using PTTK_HT_Nhom4.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTK_HT_Nhom4
{
    public partial class Frm_Phat : Form
    {
        private string docgia_id;
        private string phieumuon_id;
        private DateTime ngaytra;
        private DateTime hantra;
        private bool is_trehan;
          
        public Frm_Phat(string docgia_id, string phieumuon_id, DateTime ngaytra, DateTime hantra, bool is_trehan)
        {
            InitializeComponent();
            this.docgia_id = docgia_id;
            this.phieumuon_id = phieumuon_id;
            this.ngaytra = ngaytra;
            this.hantra = hantra;
            this.is_trehan = is_trehan;
              
        }

       

        private void Frm_Phat_Load(object sender, EventArgs e)
        {
            UC_Phat uc = new UC_Phat(docgia_id,phieumuon_id,ngaytra,hantra, is_trehan);
            uc.Dock = DockStyle.Fill;

            this.Controls.Add(uc);

        }
    }
}
