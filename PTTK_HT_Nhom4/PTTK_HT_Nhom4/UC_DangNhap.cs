using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace PTTK_HT_Nhom4
{
    public partial class UC_DangNhap : UserControl
    {
        private string OTP = "";
        private string email = "";
        private bool sendable;

        Connector myDB = new Connector();



        public UC_DangNhap()
        {
            InitializeComponent();
        }

        

        


        

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                myDB.openConnection();

                string sql = "SELECT COUNT(*) FROM Tai_Khoan WHERE MaTaiKhoan = @id AND Password = @pwd";

                using (SqlCommand command = new SqlCommand(sql, myDB.getConnection))
                {
                    command.CommandType = CommandType.Text;


                    command.Parameters.AddWithValue("@id", txtUsername.Text.Trim());
                    command.Parameters.AddWithValue("@pwd", txtPassword.Text);

                  
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        Frm_QuanLy form = new Frm_QuanLy();
                        form.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                myDB.closeConnection();
            }

        }

        private void label_forgetpwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            email = getUserEmail(username);

            OTP = genOTP();

            sendable = sendOTP(OTP, email);


            if (sendable)
            {
                MessageBox.Show("Vui lòng kiểm tra Gmail để nhận OTP", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                

                Frm_OTP frm_OTP = new Frm_OTP(email, OTP);
                frm_OTP.ShowDialog();
            }   
            
        }

        public bool sendOTP(string otp, string Email)
        {
            try
            {
                // Thông tin tài khoản email gửi
                string senderEmail = "quocthanhnt2004@gmail.com"; // Thay bằng email của bạn
                string senderPassword = "arzy hhtw xdzq lmzu"; // Mật khẩu hoặc app password
                string senderName = "Xác Nhân Đổi Mật Khẩu";

                // Thiết lập thông tin SMTP (ví dụ cho Gmail)
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                // Tạo thông tin email
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = "Mã xác thực OTP",
                    Body = $"Mã xác thực OTP của bạn là: <b>{otp}</b><br><br>Mã này có hiệu lực trong 1 phút.",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(Email);

                // Gửi email
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public string genOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public string getUserEmail (string username) 
        {
            
            try
            {
                myDB.openConnection();

                string sql = "SELECT Email FROM Tai_Khoan WHERE MaTaiKhoan = @id";

                using (SqlCommand cmd = new SqlCommand(sql, myDB.getConnection))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", username);

                    email = (string)cmd.ExecuteScalar();

                    if (email == null)
                    { 
                        MessageBox.Show("Người dùng không tồn tại email hợp lệ", "Thông Báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
     
                }
            }
            catch(SqlException ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                myDB.closeConnection();
            }
            return email;
        }

        private void UC_DangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
