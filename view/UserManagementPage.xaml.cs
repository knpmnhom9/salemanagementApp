using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using salemanagementApp.Models;
using System.IO;
using salemanagementApp.Data;
using Microsoft.EntityFrameworkCore;
namespace salemanagementApp.view
{
    public partial class UserManagementPage : Page
    {
        private int _currentPage = 1;
        private int _itemsPerPage = 7;
        private List<User> _allUsers = new List<User>();

        public UserManagementPage()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }
        private AppDbContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SaleManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
            // đổi nếu cần thiết
            
            return new AppDbContext(optionsBuilder.Options);
        }

        private void LoadDataFromDatabase()
        {
            using (var context = GetContext())
            {
                
                _allUsers = context.Users.ToList();
            }
            LoadPageData();
        }



        private void LoadPageData()
        {
            var pagedUsers = _allUsers
                .Skip((_currentPage - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

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
            string searchText = txtSearch.Text.Trim().ToLower();
            string selectedRole = (cboFilter.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Tất cả người dùng";

            // Lọc danh sách theo từ khóa và role
            var filteredUsers = _allUsers
                .Where(u =>
                    (string.IsNullOrEmpty(searchText) ||
                     u.FullName.ToLower().Contains(searchText) ||
                     u.Username.ToLower().Contains(searchText)) &&
                    (selectedRole == "Tất cả người dùng" || u.Role == selectedRole))
                .OrderBy(u => u.Id)
                .ToList();

            // Tính lại tổng số trang và cập nhật trang hiện tại
            int totalPages = (int)Math.Ceiling((double)filteredUsers.Count / _itemsPerPage);
            if (_currentPage > totalPages) _currentPage = totalPages;
            if (_currentPage <= 0) _currentPage = 1;

            // Gán dữ liệu vào datagrid
            dgUsers.ItemsSource = filteredUsers
                .Skip((_currentPage - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            txtTotalUsers.Text = $"{filteredUsers.Count} người dùng";
            txtCurrentPage.Text = $"Trang {_currentPage} / {Math.Max(1, totalPages)}";
        }


        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserPage addUserPage = new AddUserPage(_allUsers);
            this.NavigationService.Navigate(addUserPage);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                // Điều hướng tới EditUserPage, truyền User được chọn
                this.NavigationService.Navigate(new EditUserPage(selectedUser));
            }
        }
        


        private void btnPermission_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần phân quyền!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show($"Phân quyền cho người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa người dùng {selectedUser.FullName}?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new AppDbContext())
                    {
                        var userToDelete = context.Users.FirstOrDefault(u => u.Id == selectedUser.Id);
                        if (userToDelete != null)
                        {
                            context.Users.Remove(userToDelete);
                            context.SaveChanges();
                            MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy người dùng trong cơ sở dữ liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    LoadDataFromDatabase(); // gọi lại hàm load dữ liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa người dùng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần gửi email!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
                if (projectRoot == null)
                {
                    throw new InvalidOperationException("Không thể xác định thư mục gốc của dự án.");
                }

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
    }
}
