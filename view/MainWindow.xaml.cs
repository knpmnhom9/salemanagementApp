using System.Windows;
using System.Windows.Controls;
using salemanagementApp.view;
namespace salemanagementApp.view
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigateToModule(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "UserManagement": // Tên Button trong XAML
                        MainFrame.Navigate(new UserManagementPage()); // This should now use the correct namespace
                        txtContentTitle.Text = "Quản lý người dùng";
                        break;
                        // Thêm các case khác nếu cần
                }
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}

