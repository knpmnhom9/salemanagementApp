using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using salemanagementApp.Models;

namespace salemanagementApp.view
{
    public partial class UserManagementPage : Page
    {
        public UserManagementPage()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            
            
                // Sample data for user list
                var users = new List<UserModel>
                {
                    new UserModel { Id = "1", FullName = "Nguyễn Văn A", Username = "nguyenvana", Email = "nguyenvana@example.com", Phone = "0901234567", Role = "Quản trị viên", Status = "Hoạt động" },
                    new UserModel { Id = "2", FullName = "Trần Thị B", Username = "tranthib", Email = "tranthib@example.com", Phone = "0912345678", Role = "Nhân viên", Status = "Hoạt động" },
                    new UserModel { Id = "3", FullName = "Lê Văn C", Username = "levanc", Email = "levanc@example.com", Phone = "0923456789", Role = "Nhân viên", Status = "Hoạt động" },
                    new UserModel { Id = "4", FullName = "Phạm Thị D", Username = "phamthid", Email = "phamthid@example.com", Phone = "0934567890", Role = "Khách hàng", Status = "Hoạt động" },
                    new UserModel { Id = "5", FullName = "Hoàng Văn E", Username = "hoangvane", Email = "hoangvane@example.com", Phone = "0945678901", Role = "Khách hàng", Status = "Khóa" }
                };

                dgUsers.ItemsSource = users;
                txtTotalUsers.Text = users.Count.ToString();
           
        }

        // Xử lý sự kiện khi nhấn nút tìm kiếm
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Thực hiện tìm kiếm dựa trên txtSearch.Text và cboFilter.SelectedItem
            MessageBox.Show("Chức năng tìm kiếm đang được phát triển!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Xử lý sự kiện khi nhấn nút thêm người dùng
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            // Mở form thêm người dùng mới
            RegisterUserWindow registerWindow = new RegisterUserWindow();
            registerWindow.ShowDialog();
        }

        // Xử lý sự kiện khi nhấn nút sửa
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Lấy người dùng được chọn
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Mở form sửa thông tin người dùng
            MessageBox.Show($"Sửa thông tin người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Xử lý sự kiện khi nhấn nút phân quyền
        private void btnPermission_Click(object sender, RoutedEventArgs e)
        {
            // Lấy người dùng được chọn
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần phân quyền!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Mở form phân quyền người dùng
            MessageBox.Show($"Phân quyền cho người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Xử lý sự kiện khi nhấn nút xóa
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Lấy người dùng được chọn
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Xác nhận xóa người dùng
            MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng {selectedUser.FullName}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Thực hiện xóa người dùng
                MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Xử lý sự kiện khi nhấn nút gửi email
        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            // Mở form gửi email thông báo
            MessageBox.Show("Chức năng gửi email đang được phát triển!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Xử lý sự kiện khi nhấn nút xuất báo cáo
        private void btnExportReport_Click(object sender, RoutedEventArgs e)
        {
            // Xuất báo cáo danh sách người dùng
            MessageBox.Show("Chức năng xuất báo cáo đang được phát triển!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
