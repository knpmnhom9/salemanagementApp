using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using salemanagementApp.Models;
using System.IO;

namespace salemanagementApp.view
{
    public partial class UserManagementPage : Page
    {
        private int _currentPage = 1;
        private int _itemsPerPage = 4; // Số lượng người dùng trên mỗi trang
        private List<UserModel> _allUsers = new List<UserModel>();

        public UserManagementPage()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            _allUsers = new List<UserModel>
                    {
                        new UserModel { Id = "1", FullName = "Nguyễn Văn A", Username = "nguyenvana", Email = "nguyenvana@gmail.com", Phone = "0901234567", Role = "Quản trị viên", Status = "Hoạt động" },
                        new UserModel { Id = "2", FullName = "Trần Thị B", Username = "tranthib", Email = "tranthib@gmail.com", Phone = "0912345678", Role = "Nhân viên", Status = "Hoạt động" },
                        new UserModel { Id = "3", FullName = "Lê Văn C", Username = "levanc", Email = "levanc@gmail.com", Phone = "0923456789", Role = "Nhân viên", Status = "Hoạt động" },
                        new UserModel { Id = "4", FullName = "Phạm Thị D", Username = "phamthid", Email = "phamthid@gmail.com", Phone = "0934567890", Role = "Khách hàng", Status = "Hoạt động" },
                        new UserModel { Id = "5", FullName = "Hoàng Văn E", Username = "hoangvane", Email = "hoangvane@gmail.com", Phone = "0945678901", Role = "Khách hàng", Status = "Khóa" }
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
            string selectedRole = (cboFilter.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Tất cả người dùng";

            // Lọc danh sách từ danh sách gốc, không sửa đổi _allUsers
            var filteredUsers = _allUsers.Where(u => (u.FullName.ToLower().Contains(searchText) || u.Username.ToLower().Contains(searchText)) && (selectedRole == "Tất cả người dùng" || u.Role == selectedRole)).ToList();

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
            UserModel? selectedUser = dgUsers.SelectedItem as UserModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show($"Sửa thông tin người dùng: {selectedUser.FullName}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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

            MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng {selectedUser.FullName}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _allUsers.Remove(selectedUser);

                // ✅ Cập nhật lại số thứ tự cho người dùng còn lại
                var pagedUsers = _allUsers.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();

                // Đảm bảo cập nhật số thứ tự sau khi xóa
                for (int i = 0; i < pagedUsers.Count; i++)
                {
                    // Đặt lại STT cho từng người dùng
                    pagedUsers[i].Id = (i + 1).ToString();
                }

                // Hiển thị lại dữ liệu sau khi xóa
                dgUsers.ItemsSource = pagedUsers;

                // Cập nhật lại tổng số người dùng và số trang
                txtTotalUsers.Text = $"{_allUsers.Count} người dùng";
                txtCurrentPage.Text = $"Trang {_currentPage} / {Math.Ceiling((double)_allUsers.Count / _itemsPerPage)}";

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
                // Đường dẫn thư mục đã có sẵn
                string folderPath = @"C:\Users\huyho\source\repos\salemanagementApp\EmailTest";

                // Kiểm tra nếu thư mục không tồn tại thì báo lỗi
                if (!Directory.Exists(folderPath))
                {
                    MessageBox.Show($"Thư mục '{folderPath}' không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Tạo tên file với timestamp
                string fileName = $"email_{selectedUser.FullName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(folderPath, fileName);

                // Nội dung email
                string emailContent = $"To: {selectedUser.Email}\n" +
                                      $"From: test@gmail.com\n" +
                                      $"Subject: Thông báo từ hệ thống\n\n" +
                                      $"Chào {selectedUser.FullName},\n\n" +
                                      $"Đây là thông báo test từ hệ thống.\n\n" +
                                      $"---\nEmail này chỉ là mô phỏng, không thực sự được gửi đi.";

                // Ghi nội dung vào file
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
