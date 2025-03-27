using salemanagementApp.view;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace salemanagementApp.quanlyusers
{
    /// <summary>
    /// Interaction logic for QL_users.xaml
    /// </summary>
    public partial class QL_users : Page
    {
        private ObservableCollection<User> _users;

        public QL_users()
        {
            InitializeComponent();

            // Khởi tạo dữ liệu mẫu
            InitializeSampleData();

            // Hiển thị dữ liệu
            if (dgUsers != null)
            {
                dgUsers.ItemsSource = _users;
            }

            // Đặt nút mặc định là active
            if (btnDanhSach != null)
            {
                SetActiveButton(btnDanhSach);
            }
        }

        private void InitializeSampleData()
        {
            _users = new ObservableCollection<User>
            {
                new User { Id = 1, FullName = "Nguyễn Thi Mỹ Duyên", Username = "myduyncute", Email = "an@example.com", Phone = "0901234567", Role = "Admin", Status = "Hoạt động" },
                new User { Id = 2, FullName = "Trần Thị Bình", Username = "tranthib", Email = "binh@example.com", Phone = "0912345678", Role = "Nhân viên", Status = "Hoạt động" },
                new User { Id = 3, FullName = "Lê Văn Cường", Username = "levanc", Email = "cuong@example.com", Phone = "0923456789", Role = "Quản lý", Status = "Hoạt động" },
                new User { Id = 4, FullName = "Phạm Thị Dung", Username = "phamthid", Email = "dung@example.com", Phone = "0934567890", Role = "Nhân viên", Status = "Khóa" },
                new User { Id = 5, FullName = "Hoàng Văn Em", Username = "hoangvane", Email = "em@example.com", Phone = "0945678901", Role = "Nhân viên", Status = "Hoạt động" },
                new User { Id = 6, FullName = "Đỗ Thị Phương", Username = "dothip", Email = "phuong@example.com", Phone = "0956789012", Role = "Nhân viên", Status = "Hoạt động" },
                new User { Id = 7, FullName = "Vũ Văn Giang", Username = "vuvang", Email = "giang@example.com", Phone = "0967890123", Role = "Quản lý", Status = "Hoạt động" }
            };
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                // Reset tất cả các nút
                ResetAllButtons();

                // Đặt nút được chọn là active
                SetActiveButton(clickedButton);

                // Cập nhật tiêu đề trang
                if (txtPageTitle != null)
                {
                    txtPageTitle.Text = clickedButton.Content.ToString();
                }

                // Hiện tại chỉ hiển thị danh sách người dùng
                // Trong thực tế, bạn sẽ hiển thị các trang khác nhau tùy thuộc vào nút được nhấn
            }
        }

        private void ResetAllButtons()
        {
            if (btnDangKy != null) btnDangKy.Background = Brushes.Transparent;
            if (btnDangNhap != null) btnDangNhap.Background = Brushes.Transparent;
            if (btnQuenMatKhau != null) btnQuenMatKhau.Background = Brushes.Transparent;
            if (btnHoSoCaNhan != null) btnHoSoCaNhan.Background = Brushes.Transparent;
            if (btnPhanQuyen != null) btnPhanQuyen.Background = Brushes.Transparent;
            if (btnDanhSach != null) btnDanhSach.Background = Brushes.Transparent;
            if (btnTimKiem != null) btnTimKiem.Background = Brushes.Transparent;
            if (btnEmail != null) btnEmail.Background = Brushes.Transparent;
            if (btnSocialLogin != null) btnSocialLogin.Background = Brushes.Transparent;
        }

        private void SetActiveButton(Button button)
        {
            if (button != null)
            {
                button.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#3498db"));
            }
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}

