using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTK_HT_Nhom4.model
{
    class Phieuphat
    {
        private string id;
        private string phieumuon_id;
        private DateTime ngay_tra;
        private int phi_phat;


        public Phieuphat()
        {

        }
        public Phieuphat(string id, string phieumuon_id, DateTime ngay_tra, int phi_phat)
        {
            this.id = id;
            this.phieumuon_id = phieumuon_id;
            this.ngay_tra = ngay_tra;
            this.phi_phat = phi_phat;
        }


        public string maPhieuPhat
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string maPhieuMuon
        {
            get { return this.phieumuon_id; }
            set { this.phieumuon_id = value; }
        }

        public DateTime ngayTra
        {
            get { return this.ngay_tra; }
            set { this.ngay_tra = value; }
        }

        public int phiPhat
        {
            get { return this.phi_phat; }
            set { this.phi_phat = value; }
        }
    }
}
