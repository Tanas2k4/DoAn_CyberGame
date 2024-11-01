using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string passwordHash = HashPassword(password);

            var userService = new UserService();
            var user = userService.Login(username, passwordHash);

            if (user != null)
            {
                // Hiển thị form loading
                FormLoading loadingForm = new FormLoading();
                loadingForm.Show();

                // Tạo một luồng riêng để xử lý tác vụ nặng và cập nhật ProgressBar
                Thread thread = new Thread(() =>
                {
                    // Giả lập quá trình xử lý bằng cách tăng dần giá trị ProgressBar
                    for (int i = 1; i <= 100; i++)
                    {
                        Thread.Sleep(30); // Chờ một chút để tạo hiệu ứng lấp đầy
                        loadingForm.Invoke(new Action(() =>
                        {
                            loadingForm.UpdateProgress(i); // Cập nhật giá trị của ProgressBar trên UI thread
                        }));
                    }

                    // Sau khi quá trình xử lý hoàn tất, đóng form loading
                    loadingForm.Invoke(new Action(() =>
                    {
                        loadingForm.Close();
                    }));

                    // Chuyển sang form chính sau khi hoàn tất quá trình xử lý
                    this.Invoke(new Action(() =>
                    {
                        this.Hide();
                        MainForm mainForm = new MainForm(user);
                        mainForm.Show();
                    }));
                });

                thread.Start(); // Bắt đầu luồng xử lý
            }
            else
            {
                // Thay đổi màu của TextBox để chỉ báo lỗi
                txtUsername.BackColor = Color.LightCoral;
                txtPassword.BackColor = Color.LightCoral;
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ShowLoadingForm()
        {
            FormLoading loadingForm = new FormLoading();
            Application.Run(loadingForm);
        }

        private string HashPassword(string password)
        {
            // Bạn có thể dùng thư viện như BCrypt hoặc SHA256 để băm mật khẩu
            return password; // Placeholder
        }

        

        //Ấn enter để đăng nhập
        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
