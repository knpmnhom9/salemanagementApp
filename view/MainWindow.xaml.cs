using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace salemanagementApp.view
{
    public partial class MainWindow : Window
    {
        private Button currentSelectedButton;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigateToModule(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag != null)
            {
                // Bỏ chọn các nút cũ
                foreach (var child in SidebarMenu.Children)
                {
                    if (child is Button btn)
                    {
                        btn.Background = (Brush)this.FindResource("MenuDefaultBackground");
                        btn.Foreground = Brushes.White;
                    }
                }

                // Highlight nút đang chọn
                clickedButton.Background = (Brush)this.FindResource("MenuSelectedBackground");
                clickedButton.Foreground = Brushes.White;
                currentSelectedButton = clickedButton;

                // Điều hướng trang
                string pageTag = clickedButton.Tag.ToString();

                switch (pageTag)
                {
                    case "UserManagementPage":
                        MainFrame.Navigate(new UserManagementPage());
                        break;
                    //case "ProductManagementPage":
                    //    MainFrame.Navigate(new ProductManagementPage());
                    //    break;
                    //case "CartAndCheckoutPage":
                    //    MainFrame.Navigate(new CartAndCheckoutPage());
                    //    break;
                    //case "OrderManagementPage":
                    //    MainFrame.Navigate(new OrderManagementPage());
                    //    break;
                    //case "WarehouseManagementPage":
                    //    MainFrame.Navigate(new WarehouseManagementPage());
                    //    break;
                    //case "ReportPage":
                    //    MainFrame.Navigate(new ReportPage());
                    //    break;
                    //case "CustomerSupportPage":
                    //    MainFrame.Navigate(new CustomerSupportPage());
                    //    break;
                    //case "PromotionAndMarketingPage":
                    //    MainFrame.Navigate(new PromotionAndMarketingPage());
                    //    break;
                    //case "SettingsPage":
                    //    MainFrame.Navigate(new SettingsPage());
                    //    break;
                    default:
                        MessageBox.Show("Chức năng này chưa có", "Thông báo");
                        break;
                }
            }
        }
    }
}
