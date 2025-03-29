using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace salemanagementApp.view
{
    public partial class RegisterUserWindow : Window
    {
        public RegisterUserWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrEmpty(txtFullName.Text) ||
                string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) ||
                txtPassword.Password.Length == 0 ||
                txtConfirmPassword.Password.Length == 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra mật khẩu khớp nhau
            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra đồng ý điều khoản
            if (!chkAgree.IsChecked.HasValue || !chkAgree.IsChecked.Value)
            {
                MessageBox.Show("Vui lòng đồng ý với điều khoản và điều kiện!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Thực hiện đăng ký
            MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng form sau khi đăng ký thành công
            this.Close();
        }

        private void TxtLogin_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Mở form đăng nhập và đóng form đăng ký
            // LoginWindow loginWindow = new LoginWindow();
            // loginWindow.Show();
            this.Close();
        }
    }
}