using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTK_HT_Nhom4.model
{
    class Theloai
    {
        private int id;
        private string ten_the_loai;
        private string ten_nganh;

        // Constructor
        public Theloai()
        {

        }

        public Theloai(int id, string ten_the_loai, string ten_nganh, int maTheLoai, string tenTheLoai, string tenNganh)
        {
            this.id = id;
            this.ten_the_loai = ten_the_loai;
            this.ten_nganh = ten_nganh;
            this.maTheLoai = maTheLoai;
            this.tenTheLoai = tenTheLoai;
            this.tenNganh = tenNganh;
        }


        // Getter và Setter
        public int maTheLoai
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string tenTheLoai
        {
            get { return this.ten_the_loai; }
            set { this.ten_the_loai = value; }
        }


        public string tenNganh
        {
            get { return this.ten_nganh; }
            set { this.ten_nganh = value; }
        }
    }
}
