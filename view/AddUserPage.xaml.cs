using salemanagementApp.Data;
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
            private List<User> _allUsers;

            public AddUserPage(List<User> allUsers)
            {
                InitializeComponent();
                _allUsers = allUsers;
            }

            private void btnSave_Click(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    cboRole.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedRole = cboRole.SelectedItem as ComboBoxItem;
                string role = selectedRole?.Tag?.ToString() ?? "User"; // lấy theo Tag thay vì Content

                var newUser = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Role = role,
                    Status = "Active"
                };

                try
                {
                    using (var db = new AppDbContext())
                    {
                        db.Users.Add(newUser);
                        db.SaveChanges();
                    }

                    // cập nhật danh sách hiển thị
                    _allUsers.Add(newUser);

                    MessageBox.Show("Người dùng đã được thêm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu vào DB: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void btnCancel_Click(object sender, RoutedEventArgs e)
            {
                this.NavigationService.GoBack();
            }
        }
    }
