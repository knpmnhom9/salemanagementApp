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
    public partial class AddUserPage : Page
    {
        private List<UserModel> _allUsers;

        public AddUserPage(List<UserModel> allUsers)
        {
            InitializeComponent();
            _allUsers = allUsers;
        }

        // Sự kiện khi nhấn nút "Lưu"
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                cboRole.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tạo đối tượng UserModel mới
            var selectedRole = cboRole.SelectedItem as ComboBoxItem;
            var newUser = new UserModel
            {
                Id = (_allUsers.Count + 1).ToString(),
                FullName = txtFullName.Text,
                Username = txtUsername.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                Role = selectedRole?.Content?.ToString() ?? string.Empty,
                Status = "Hoạt động" // Mặc định là "Hoạt động"
            };

            // Thêm người dùng vào danh sách
            _allUsers.Add(newUser);
            MessageBox.Show("Người dùng đã được thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng trang hoặc quay lại trang trước
            this.NavigationService.GoBack();
        }

        // Sự kiện khi nhấn nút "Hủy"
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
