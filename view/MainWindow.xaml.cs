using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace salemanagementApp.view
{
    public partial class MainWindow : Window
    {
        private Button currentSelectedButton;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Khi di chuột vào ExpandedMenu, không cần xử lý gì thêm vì menu luôn hiển thị
        private void ExpandedMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            // Không cần làm gì, menu đã luôn hiển thị
        }

        private void NavigateToModule(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is not null)
            {
                // Bỏ highlight nút cũ (nếu có)
                if (currentSelectedButton != null)
                {
                    currentSelectedButton.Background = Brushes.Transparent;
                }

                // Cập nhật nút hiện tại và đổi màu highlight
                currentSelectedButton = button;
                currentSelectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1ABC9C"));

                string pageTag = button.Tag.ToString();

                switch (pageTag)
                {
                    case "UserManagementPage":
                        MainFrame.Navigate(new UserManagementPage());
                        break;
                    case "ProductManagementPage":
                        MessageBox.Show("Chức năng đang phát triển", "Thông báo");
                        break;
                    default:
                        MessageBox.Show("Chức năng này chưa có", "Thông báo");
                        break;
                }
            }
        }
    }
}
