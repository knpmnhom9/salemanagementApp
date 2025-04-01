using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using salemanagementApp.Models;

namespace salemanagementApp.view
{
    public partial class UserManagementPage : Page
    {
        private int _currentPage = 1;
        private int _itemsPerPage = 4; // Số lượng người dùng trên mỗi trang
        private List<UserModel> _allUsers;

        public UserManagementPage()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            _allUsers = new List<UserModel>
                {
                    new UserModel { Id = "1", FullName = "Nguyễn Văn A", Username = "nguyenvana", Email = "nguyenvana@example.com", Phone = "0901234567", Role = "Quản trị viên", Status = "Hoạt động" },
                    new UserModel { Id = "2", FullName = "Trần Thị B", Username = "tranthib", Email = "tranthib@example.com", Phone = "0912345678", Role = "Nhân viên", Status = "Hoạt động" },
                    new UserModel { Id = "3", FullName = "Lê Văn C", Username = "levanc", Email = "levanc@example.com", Phone = "0923456789", Role = "Nhân viên", Status = "Hoạt động" },
                    new UserModel { Id = "4", FullName = "Phạm Thị D", Username = "phamthid", Email = "phamthid@example.com", Phone = "0934567890", Role = "Khách hàng", Status = "Hoạt động" },
                    new UserModel { Id = "5", FullName = "Hoàng Văn E", Username = "hoangvane", Email = "hoangvane@example.com", Phone = "0945678901", Role = "Khách hàng", Status = "Khóa" }
                };
            LoadPageData();
        }

        private void LoadPageData()
        {
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
            string selectedRole = (cboFilter.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Lọc danh sách từ danh sách gốc, không sửa đổi _allUsers
            var filteredUsers = _allUsers.Where(u =>
                (u.FullName.ToLower().Contains(searchText) || u.Username.ToLower().Contains(searchText)) &&
                (selectedRole == "Tất cả người dùng" || u.Role == selectedRole)).ToList();

            // Cập nhật lại số trang nếu trang hiện tại vượt quá số trang có sẵn
            int totalPages = (int)Math.Ceiling((double)filteredUsers.Count / _itemsPerPage);
            if (_currentPage > totalPages)
            {
                _currentPage = totalPages; // Đảm bảo không vượt quá số trang có sẵn
            }

            // Hiển thị danh sách đã lọc với phân trang
            dgUsers.ItemsSource = filteredUsers.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();

            // Cập nhật số lượng người dùng tìm thấy và trang hiện tại
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
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show($"Sửa thông tin người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnPermission_Click(object sender, RoutedEventArgs e)
        {
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần phân quyền!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show($"Phân quyền cho người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng {selectedUser.FullName}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _allUsers.Remove(selectedUser);
                LoadPageData();
                MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            UserModel selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần gửi email!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("your_email@gmail.com", "your_password"),
                    EnableSsl = true,
                };

                var mailMessage = new System.Net.Mail.MailMessage
                {
                    From = new System.Net.Mail.MailAddress("your_email@gmail.com"),
                    Subject = "Thông báo từ hệ thống",
                    Body = $"Chào {selectedUser.FullName},\n\nĐây là thông báo từ hệ thống.",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(selectedUser.Email);
                smtpClient.Send(mailMessage);

                MessageBox.Show("Email đã được gửi thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExportReport_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "UserReport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("Id,FullName,Username,Email,Phone,Role,Status");
                    foreach (var user in _allUsers)
                    {
                        writer.WriteLine($"{user.Id},{user.FullName},{user.Username},{user.Email},{user.Phone},{user.Role},{user.Status}");
                    }
                }
                MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
