using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTK_HT_Nhom4.model
{
    class Sach
    {
        private string id;
        private int theloai_id;
        private string ten_sach;
        private int nam_xuat_ban;
        private string tac_gia;
        private string trang_thai;

        public Sach()
        {

        }


        public Sach(string id, int theloai_id, string ten_sach, int nam_xuat_ban, string tac_gia, string trang_thai)
        {
            this.id = id;
            this.theloai_id = theloai_id;
            this.ten_sach = ten_sach;
            this.nam_xuat_ban = nam_xuat_ban;
            this.tac_gia = tac_gia;
            this.trang_thai = trang_thai;
        }



        public string maSach
        {
            get { return this.id; }
            set { this.id = value; }
        }


        public int maTheLoai
        {
            get { return this.theloai_id; }
            set { this.theloai_id = value; }
        }


        public string tenSach
        {
            get { return this.ten_sach; }
            set { this.ten_sach = value; }
        }

        public int namXuatBan
        {
            get { return this.nam_xuat_ban; }
            set { this.nam_xuat_ban = value; }
        }


        public string tacGia
        {
            get { return this.tac_gia; }
            set { this.tac_gia = value; }
        }

        public string trangThai
        {
            get { return this.trang_thai; }
            set { this.trang_thai = value; }
        }
    }
}
