using Microsoft.Win32;
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
    public partial class ProductManagementPage : Page
    {
        public ProductManagementPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == txtProductName)
                txtProductNamePlaceholder.Visibility = string.IsNullOrEmpty(txtProductName.Text) ? Visibility.Visible : Visibility.Collapsed;

            if (sender == txtProductPrice)
                txtProductPricePlaceholder.Visibility = string.IsNullOrEmpty(txtProductPrice.Text) ? Visibility.Visible : Visibility.Collapsed;

            if (sender == txtCategoryName)
                txtCategoryNamePlaceholder.Visibility = string.IsNullOrEmpty(txtCategoryName.Text) ? Visibility.Visible : Visibility.Collapsed;

            if (sender == txtSearch)
                txtSearchPlaceholder.Visibility = string.IsNullOrEmpty(txtSearch.Text) ? Visibility.Visible : Visibility.Collapsed;

            if (sender == txtDescription)
                txtDescriptionPlaceholder.Visibility = string.IsNullOrEmpty(txtDescription.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý thêm sản phẩm
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý thêm danh mục
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý tìm kiếm sản phẩm
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chọn ảnh từ máy
        }
    }
}
