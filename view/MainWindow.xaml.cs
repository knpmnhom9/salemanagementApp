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
            Button? button = sender as Button;
            if (button != null)
            {
                string moduleName = button.Name;
                MainFrame.Content = null;
                // Cập nhật tiêu đề
                string content = button.Content.ToString();
                txtContentTitle.Text = content.Substring(content.IndexOf(' ') + 1); // Loại bỏ icon đầu tiên

                // Điều hướng đến trang tương ứng
                switch (moduleName)
                {
                    case "UserManagement":
                        MainFrame.Navigate(new salemanagementApp.view.UserManagementPage());
                        break;
                    case "ProductManagement":
                        // MainFrame.Navigate(new ProductManagementPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "CartAndCheckout":
                        // MainFrame.Navigate(new CartAndCheckoutPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "OrderManagement":
                        // MainFrame.Navigate(new OrderManagementPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "WarehouseManagement":
                        // MainFrame.Navigate(new WarehouseManagementPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "ReportsAndStatistics":
                        // MainFrame.Navigate(new ReportsPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "CustomerSupport":
                        // MainFrame.Navigate(new CustomerSupportPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "PromotionManagement":
                        // MainFrame.Navigate(new PromotionPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    case "SystemAndConfiguration":
                        // MainFrame.Navigate(new SystemConfigPage());
                        MessageBox.Show("Chức năng đang được phát triển!", "Thông báo");
                        break;
                    default:
                        MainFrame.Content = null;
                        break;
                }
            }
        }
        //private void BtnLogout_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        Application.Current.Shutdown();
        //    }
        //}
    }
}
