using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTK_HT_Nhom4.model
{
    class Phieumuon
    {
        private string id;
        private DateTime ngay_muon;
        private DateTime ngay_tra;
        private DateTime han_tra;
        private string taikhoan_id;
        private string status;



        public Phieumuon()
        {

        }


        public Phieumuon(string id, DateTime ngay_muon, DateTime ngay_tra, DateTime han_tra, string taikhoan_id, string status)
        {
            this.id = id;
            this.ngay_muon = ngay_muon;
            this.han_tra = han_tra;
            this.taikhoan_id = taikhoan_id;
            this.ngay_tra = ngay_tra;
            this.status = status;
        }
        

        

        public string maPhieuMuon
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public DateTime ngayMuon
        {
            get { return this.ngay_muon; }
            set { this.ngay_muon = value; }
        }

        public DateTime ngayTra
        {
            get { return this.ngay_tra; }
            set { this.ngay_tra = value; }
        }

        public DateTime hanTra
        {
            get { return this.han_tra; }
            set { this.han_tra = value; }
        }

        public string maDocGia
        {
            get { return this.taikhoan_id; }
            set { this.taikhoan_id = value; }
        }

        public string trangThai
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
