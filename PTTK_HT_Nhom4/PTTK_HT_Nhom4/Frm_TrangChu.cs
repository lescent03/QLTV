﻿using System;
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
    public partial class Frm_TrangChu : Form
    {
        public Frm_TrangChu()
        {
            InitializeComponent();
        }

        private void Frm_TrangChu_Load(object sender, EventArgs e)
        {
            UC_TrangChu uc = new UC_TrangChu();
            uc.Dock = DockStyle.Fill;
            this.Controls.Add(uc);
        }
    }
}
