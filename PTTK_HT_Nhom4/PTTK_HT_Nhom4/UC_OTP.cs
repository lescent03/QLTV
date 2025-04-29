using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PTTK_HT_Nhom4
{
    public partial class UC_OTP : UserControl
    {
        // Các biến toàn cục cần thiết
        private Timer countdownTimer;
        private int remainingSeconds = 120; // 2 phút = 120 giây
        private string email;
        private string OTP;

        private bool flag = false;


        public UC_OTP(string email, string otp)
        {
            this.email = email;
            InitializeComponent();
            this.OTP = otp;
        }

        private void UC_OTP_Load(object sender, EventArgs e)
        {

            
            labelCountDown();

        }

        void labelCountDown()
        {
            // Reset thời gian về 2 phút
            remainingSeconds = 60;

            // Cập nhật hiển thị ban đầu
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            label_timer.Text = string.Format("{0:00}:{1:00}", minutes, seconds);
            label_timer.ForeColor = Color.Blue;

            // Khởi tạo timer mới hoặc reset timer cũ
            if (countdownTimer != null)
            {
                countdownTimer.Stop();
            }
            else
            {
                countdownTimer = new Timer();
                countdownTimer.Interval = 1000; // 1 giây
                countdownTimer.Tick += (s, args) =>
                {
                    // Giảm thời gian đi 1 giây
                    remainingSeconds--;

                    // Tính toán phút và giây
                    int mins = remainingSeconds / 60;
                    int secs = remainingSeconds % 60;

                    // Cập nhật hiển thị
                    label_timer.Text = string.Format("{0:00}:{1:00}", mins, secs);

                    // Thay đổi màu khi thời gian còn ít
                    if (remainingSeconds <= 30)
                    {
                        label_timer.ForeColor = Color.Red;
                    }

                    // Kiểm tra hết thời gian
                    if (remainingSeconds <= 0)
                    {
                        countdownTimer.Stop();
                        label_timer.Text = "Hết hạn";
                        label_timer.ForeColor = Color.Red;



                        // Thêm xử lý khi hết thời gian nếu cần
                        // Ví dụ: vô hiệu hóa nút xác nhận OTP
                        flag = true;
                        
                    }
                };
            }

            // Bắt đầu đếm ngược
            countdownTimer.Start();

        }

        private void btn_XacThuc_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                UC_DangNhap uc = new UC_DangNhap();
                string otp = uc.genOTP();
                MessageBox.Show("Kiểm tra Gmail để nhận OTP mới!!!");
                uc.sendOTP(otp, email);
                this.OTP = otp;
                flag = false;
            }
            else
            {
                string otp1 = txt_otp1.Text.Trim();
                string otp2 = txt_otp2.Text.Trim();
                string otp3 = txt_otp3.Text.Trim();
                string otp4 = txt_otp4.Text.Trim();
                string otp5 = txt_otp5.Text.Trim();
                string otp6 = txt_otp6.Text.Trim();

                string otp = otp1 + otp2 + otp3 + otp4 + otp5 + otp6;

                if (otp.Equals(OTP))
                {
                    MessageBox.Show("Đúng");
                }
                else
                {
                    MessageBox.Show("Sai");

                }
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
