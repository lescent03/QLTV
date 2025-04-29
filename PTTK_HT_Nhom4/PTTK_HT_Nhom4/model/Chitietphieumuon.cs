using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTK_HT_Nhom4.model
{
    class Chitietphieumuon
    {
        private string phieumuon_id;
        private string sach_id;
        private int so_luong;

        public Chitietphieumuon(string phieumuon_id, string sach_id, int so_luong)
        {
            this.phieumuon_id = phieumuon_id;
            this.sach_id = sach_id;
            this.so_luong = so_luong;
        }

        public Chitietphieumuon() 
        {
            
        }


        public string maPhieuMuon
        {
            get { return this.phieumuon_id; }
            set { this.phieumuon_id = value; }
        }

        public string maSach
        {
            get { return this.sach_id; }
            set { this.sach_id = value; }
        }

        public int soLuong
        {
            get { return this.so_luong; }
            set { this.so_luong = value; }
        }
    }
}
