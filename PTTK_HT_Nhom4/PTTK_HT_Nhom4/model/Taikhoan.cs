using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTK_HT_Nhom4.model
{
    class Taikhoan
    {
        private string id;
        private string ho_va_ten;
        private string gioi_tinh;
        private string email;
        private string so_dien_thoai;
        private DateTime ngay_sinh;
        private string password;


        // Constructor
        public Taikhoan()
        {

        }

        public Taikhoan(string id, string ho_va_ten, string gioi_tinh, string email, string so_dien_thoai, DateTime ngay_sinh, string password)
        {
            this.id = id;
            this.ho_va_ten = ho_va_ten;
            this.gioi_tinh = gioi_tinh;
            this.Email = email;
            this.so_dien_thoai = so_dien_thoai;
            this.ngay_sinh = ngay_sinh;
            this.Password = password;
        }


        // Getter và Setter
        public string maTaiKhoan
        {
            get { return this.id; }
            set { this.id = value; }
        }


        public string hoTen
        {
            get { return this.ho_va_ten; }
            set { this.ho_va_ten = value; }
        }

    
        public string gioiTinh
        {
            get { return this.gioi_tinh; }
            set { this.gioi_tinh = value; }
        }

       
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

      
        public string soDienThoai
        {
            get { return this.so_dien_thoai; }
            set { this.so_dien_thoai = value; }
        }

     
        public DateTime NgaySinh
        {
            get { return this.ngay_sinh; }
            set { this.ngay_sinh = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

    }


}
