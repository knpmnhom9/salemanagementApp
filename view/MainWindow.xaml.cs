using salemanagementApp.quanlyusers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace salemanagementApp.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button _currentSelectedButton;

        public MainWindow()
        {
            InitializeComponent();

            // Mặc định chọn nút Báo cáo & Thống kê
            _currentSelectedButton = ;

            // Hiển thị trang báo cáo mặc định
            MainFrame.Content = new ReportsPage();
        }
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == "Tìm kiếm...")
            {
                textBox.Text = string.Empty;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Tìm kiếm...";
            }
        }

        private void NavigateToModule(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                // Đặt lại style cho nút hiện tại
                if (_currentSelectedButton != null)
                {
                    _currentSelectedButton.Style = (Style)FindResource("MainMenuButtonStyle");
                }

                // Đặt style cho nút được chọn
                clickedButton.Style = (Style)FindResource("SelectedMenuButtonStyle");
                _currentSelectedButton = clickedButton;

                // Lấy nội dung của button
                if (clickedButton.Content is StackPanel stackPanel &&
                    stackPanel.Children.Count > 0 &&
                    stackPanel.Children[0] is TextBlock textBlock)
                {
                    string moduleName = textBlock.Text;
                    txtContentTitle.Text = moduleName;

                    // Điều hướng dựa trên nút được nhấn
                    if (clickedButton == btnUserManagement)
                    {
                        MainFrame.Navigate(new QL_users());
                    }
                    else if (clickedButton == btnProductManagement)
                    {
                        MainFrame.Content = new ProductsPage();
                    }
                    else if (clickedButton == btnCartPayment)
                    {
                        MainFrame.Content = new CartPaymentPage();
                    }
                    else if (clickedButton == btnOrderManagement)
                    {
                        MainFrame.Content = new OrdersPage();
                    }
                    else if (clickedButton == btnInventoryManagement)
                    {
                        MainFrame.Content = new InventoryPage();
                    }
                    else if (clickedButton == btnReportsStatistics)
                    {
                        MainFrame.Content = new ReportsPage();
                    }
                    else if (clickedButton == btnCustomerSupport)
                    {
                        MainFrame.Content = new CustomerSupportPage();
                    }
                    else if (clickedButton == btnPromotionMarketing)
                    {
                        MainFrame.Content = new PromotionMarketingPage();
                    }
                    else if (clickedButton == btnSystemConfig)
                    {
                        MainFrame.Content = new SystemConfigPage();
                    }
                }
            }
        }
    }

    // Placeholder classes cho các trang
    public class ProductsPage : UserControl
    {
        public ProductsPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Quản lý sản phẩm & danh mục đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class CartPaymentPage : UserControl
    {
        public CartPaymentPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Giỏ hàng & Thanh toán đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class OrdersPage : UserControl
    {
        public OrdersPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Quản lý đơn hàng đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class InventoryPage : UserControl
    {
        public InventoryPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Quản lý kho hàng đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class ReportsPage : UserControl
    {
        public ReportsPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Báo cáo & Thống kê đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class CustomerSupportPage : UserControl
    {
        public CustomerSupportPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Đánh giá & Hỗ trợ khách hàng đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class PromotionMarketingPage : UserControl
    {
        public PromotionMarketingPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Quản lý khuyến mãi & Marketing đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }

    public class SystemConfigPage : UserControl
    {
        public SystemConfigPage()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.White;

            TextBlock text = new TextBlock
            {
                Text = "Trang Hệ thống & Cấu hình đang được phát triển",
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(text);
            this.Content = grid;
        }
    }
}

