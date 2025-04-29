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
    public partial class Frm_OTP : Form
    {
        private string email;
        private string otp;

        public Frm_OTP(string email, string OTP)
        {
            this.email = email;
            this.otp = OTP;
            InitializeComponent();
        }

        private void Frm_OTP_Load(object sender, EventArgs e)
        {
            UC_OTP uc = new UC_OTP(email,otp);
            uc.Dock = DockStyle.Fill;
            this.Controls.Add(uc);
        }
    }
}
