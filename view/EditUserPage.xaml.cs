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
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private User _userToEdit;

        // Constructor mặc định (nếu cần)
        public EditUserPage()
        {
            InitializeComponent();
        }

        // Constructor có tham số User
        public EditUserPage(User user)
        {
            InitializeComponent();
            _userToEdit = user;

            // Gán thông tin người dùng vào các TextBox
            txtFullName.Text = _userToEdit.FullName;
            txtUsername.Text = _userToEdit.Username;
            txtEmail.Text = _userToEdit.Email;
            txtPhone.Text = _userToEdit.Phone;
            txtRole.Text = _userToEdit.Role;
            txtStatus.Text = _userToEdit.Status;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _userToEdit.FullName = txtFullName.Text;
            _userToEdit.Username = txtUsername.Text;
            _userToEdit.Email = txtEmail.Text;
            _userToEdit.Phone = txtPhone.Text;
            _userToEdit.Role = txtRole.Text;
            _userToEdit.Status = txtStatus.Text;

            using (var context = new AppDbContext()) // dùng AppDbContext thay vì DbContext
            {
                context.Users.Update(_userToEdit);
                context.SaveChanges();
            }

            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack(); // 👉 Quay lại trang trước sau khi lưu
        }   
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // 👉 Quay lại trang trước
        }

    }

}
