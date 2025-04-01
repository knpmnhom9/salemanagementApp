using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace salemanagementApp.view
{
    public partial class MainWindow : Window
    {
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
