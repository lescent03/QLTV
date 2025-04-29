using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTK_HT_Nhom4
{
    public partial class UC_TrangChu : UserControl
    {
        int currentImageIndex = 0;
        string[] imagePaths;
        Timer slideshowTimer = new Timer();


        public UC_TrangChu()
        {
            InitializeComponent();
            loadPicturebox1();
            loadPicturebox2();
           
        }

        private void homepage_picturebox_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        void loadPicturebox1()
        {
            string imagePath = Path.Combine(Application.StartupPath, "Images", "Tagline web add QS Ranking-02.jpg");
            guna2PictureBox1.Image = Image.FromFile(imagePath);
        }

        void loadPicturebox2()
        {
            string imageFolder = Path.Combine(Application.StartupPath, "Images");

            // Log thử đường dẫn để kiểm tra
            //MessageBox.Show("Đường dẫn folder ảnh: " + imageFolder);

            var supportedExtensions = new[] { "*.jpg", "*.jpeg", "*.png"};
            imagePaths = supportedExtensions
                .SelectMany(ext => Directory.GetFiles(imageFolder, ext))
                .Where(path => !Path.GetFileName(path).Equals("Tagline web add QS Ranking-02.jpg", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            // Log tổng số ảnh tìm được
            //MessageBox.Show("Tổng số ảnh tìm được: " + imagePaths.Length);

            if (imagePaths.Length == 0)
            {
                MessageBox.Show("Không tìm thấy ảnh trong thư mục Images!");
                return;
            }

            homepage_picturebox.Image = Image.FromFile(imagePaths[currentImageIndex]);

            slideshowTimer.Interval = 3000;
            slideshowTimer.Tick += SlideshowTimer_Tick;
            slideshowTimer.Start();
        }

        private void SlideshowTimer_Tick(object sender, EventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex >= imagePaths.Length)
                currentImageIndex = 0;

            homepage_picturebox.Image = Image.FromFile(imagePaths[currentImageIndex]);
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            Frm_TimKiem form = new Frm_TimKiem();
            form.Show();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            Frm_DangNhap form = new Frm_DangNhap();
            form.Show();
        }

        private void UC_TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
