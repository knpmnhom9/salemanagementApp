using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using salemanagementApp.Models;
using salemanagementApp.Data;
using System.IO;

namespace salemanagementApp.view
{
    public partial class UserManagementPage : Page
    {
        private int _currentPage = 1;
        private int _itemsPerPage = 4;
        private List<UserModel> _allUsers = new List<UserModel>();

        public UserManagementPage()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            using (var context = new AppDbContext())
            {
                _allUsers = context.Users.ToList();
            }
            LoadPageData();
        }

        private void LoadPageData()
        {
            using (var db = new AppDbContext())
            {
                _allUsers = db.Users.ToList();
            }

            var pagedUsers = _allUsers.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            dgUsers.ItemsSource = pagedUsers;

            txtTotalUsers.Text = $"{_allUsers.Count} người dùng";
            txtCurrentPage.Text = $"Trang {_currentPage} / {Math.Ceiling((double)_allUsers.Count / _itemsPerPage)}";
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < Math.Ceiling((double)_allUsers.Count / _itemsPerPage))
            {
                _currentPage++;
                LoadPageData();
            }
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadPageData();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            string selectedRole = (cboFilter.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Tất cả người dùng";

            var filteredUsers = _allUsers.Where(u => (u.FullName.ToLower().Contains(searchText) || u.Username.ToLower().Contains(searchText)) && (selectedRole == "Tất cả người dùng" || u.Role == selectedRole)).ToList();

            int totalPages = (int)Math.Ceiling((double)filteredUsers.Count / _itemsPerPage);
            if (_currentPage > totalPages) _currentPage = totalPages;

            dgUsers.ItemsSource = filteredUsers.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
            txtTotalUsers.Text = $"{filteredUsers.Count} người dùng";
            txtCurrentPage.Text = $"Trang {_currentPage} / {totalPages}";
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserPage addUserPage = new AddUserPage(_allUsers);
            this.NavigationService.Navigate(addUserPage);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            UserModel? selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            EditUserPage editUserPage = new EditUserPage(selectedUser.Id);
            this.NavigationService.Navigate(editUserPage);
        }

        private void btnPermission_Click(object sender, RoutedEventArgs e)
        {
            UserModel? selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần phân quyền!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show($"Phân quyền cho người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UserModel? selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng {selectedUser.FullName}?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    var userToDelete = context.Users.FirstOrDefault(u => u.Id == selectedUser.Id);
                    if (userToDelete != null)
                    {
                        context.Users.Remove(userToDelete);
                        context.SaveChanges();
                    }
                }
                LoadDataFromDatabase();
                MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            UserModel? selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần gửi email!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string folderPath = Path.Combine(projectRoot, "EmailTest");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = $"email_{selectedUser.FullName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(folderPath, fileName);

                string emailContent = $"To: {selectedUser.Email}\n" +
                                      $"From: test@gmail.com\n" +
                                      $"Subject: Thông báo từ hệ thống\n\n" +
                                      $"Chào {selectedUser.FullName},\n\n" +
                                      $"Đây là thông báo test từ hệ thống.\n\n---\nEmail này chỉ là mô phỏng, không thực sự được gửi đi.";

                File.WriteAllText(filePath, emailContent);

                MessageBox.Show($"Email đã được lưu vào:\n{filePath}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu email: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public AddUserPage(List<UserModel> allUsers)
        {
            InitializeComponent();
            _allUsers = allUsers;
        }
    }
}
