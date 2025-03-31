using salemanagementApp.Models;
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
    public partial class RegisterUserPage : Page
    {
        private List<UserModel> _allUsers;

        public RegisterUserPage(List<UserModel> allUsers)
        {
            InitializeComponent();
            _allUsers = allUsers;  // Nhận danh sách người dùng từ trang cha
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các điều khiển
            string fullName = txtFullName.Text;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string role = (cboRole.SelectedItem as ComboBoxItem)?.Content.ToString();
            string status = (cboStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Kiểm tra thông tin
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Thêm người dùng mới vào danh sách
            var newUser = new UserModel
            {
                Id = (_allUsers.Count + 1).ToString(), // Tạo ID giả (chỉ mang tính minh họa)
                FullName = fullName,
                Username = username,
                Email = email,
                Phone = phone,
                Role = role,
                Status = status
            };

            _allUsers.Add(newUser);
            MessageBox.Show("Đăng ký người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng cửa sổ này và trở lại trang người dùng (hoặc chuyển về trang cha)
            this.NavigationService.GoBack();
        }
    }
}
