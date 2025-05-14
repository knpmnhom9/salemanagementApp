using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
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
                // Reset màu các nút cũ
                foreach (var child in SidebarMenu.Children)
                {
                    if (child is Button btn)
                    {
                        btn.Background = (Brush)this.FindResource("MenuDefaultBackground");
                        btn.Foreground = Brushes.White;
                    }
                }

                // Đổi màu nút đang chọn
                clickedButton.Background = (Brush)this.FindResource("MenuSelectedBackground");
                clickedButton.Foreground = Brushes.White;
                currentSelectedButton = clickedButton;

                // Điều hướng đến Page tương ứng
                string pageTag = clickedButton.Tag.ToString();

                switch (pageTag)
                {
                    case "UserManagementPage":
                        MainFrame.Navigate(new UserManagementPage());
                        break;
                    case "ProductManagementPage":
                        MainFrame.Navigate(new ProductManagementPage());
                        break;
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
            if (sender is Button btn && btn.Tag is string pageName)
            {
                Uri pageUri = new Uri($"Pages/{pageName}.xaml", UriKind.Relative);
                MainFrame.Navigate(pageUri);
            }
        }
        
           
        


    }

}
namespace salemanagementApp.Services
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        static ApiService()
        {
            client.BaseAddress = new Uri("http://localhost:8000/api/"); // sửa đúng URL Laravel của bạn
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<string> GetAsync(string endpoint)
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> PostAsync(string endpoint, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Add thêm các method PutAsync, DeleteAsync nếu cần
    }
}
