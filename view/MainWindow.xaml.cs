using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace salemanagementApp.view
{
    public partial class MainWindow : Window
    {
        private bool isMenuExpanded = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Sidebar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExpandMenu();
        }

        private void Sidebar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CollapseMenu();
        }

        private void ExpandMenu()
        {
            if (!isMenuExpanded)
            {
                // Animate the sidebar width
                GridLengthAnimation animation = new GridLengthAnimation
                {
                    From = new GridLength(60),
                    To = new GridLength(300),
                    Duration = new Duration(TimeSpan.FromMilliseconds(200))
                };
                SidebarColumn.BeginAnimation(ColumnDefinition.WidthProperty, animation);

                // Show expanded menu, hide collapsed menu
                ExpandedMenu.Visibility = Visibility.Visible;
                CollapsedMenu.Visibility = Visibility.Collapsed;

                isMenuExpanded = true;
            }
        }

        private void CollapseMenu()
        {
            if (isMenuExpanded)
            {
                // Animate the sidebar width
                GridLengthAnimation animation = new GridLengthAnimation
                {
                    From = new GridLength(300),
                    To = new GridLength(60),
                    Duration = new Duration(TimeSpan.FromMilliseconds(200))
                };
                SidebarColumn.BeginAnimation(ColumnDefinition.WidthProperty, animation);

                // Show collapsed menu, hide expanded menu
                ExpandedMenu.Visibility = Visibility.Collapsed;
                CollapsedMenu.Visibility = Visibility.Visible;

                isMenuExpanded = false;
            }
        }

        private void NavigateToModule(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string moduleName;

            // Get the module name from either the button name or tag
            if (button.Tag != null)
            {
                moduleName = button.Tag.ToString();
            }
            else
            {
                moduleName = button.Name;
            }

            // Update the content title
            txtContentTitle.Text = button.Content.ToString().Replace("👥 ", "").Replace("📦 ", "")
                .Replace("🛍️ ", "").Replace("📜 ", "").Replace("🏬 ", "")
                .Replace("📈 ", "").Replace("💬 ", "").Replace("🎯 ", "").Replace("⚙️ ", "");

            // Navigate to the appropriate page
            switch (moduleName)
            {
                case "UserManagement":
                    MainFrame.Navigate(new UserManagementPage());
                    break;
                case "ProductManagement":
                    // MainFrame.Navigate(new ProductManagementPage());
                    MessageBox.Show("Chức năng đang phát triển", "Thông báo");
                    break;
                // Add other cases for different modules
                default:
                    MessageBox.Show("Chức năng đang phát triển", "Thông báo");
                    break;
            }
        }
    }

    public class GridLengthAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(GridLength);

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));
        public GridLength From
        {
            get { return (GridLength)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));
        public GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromVal = From.Value;
            double toVal = To.Value;

            if (fromVal > toVal)
            {
                return new GridLength((1 - animationClock.CurrentProgress.Value) * (fromVal - toVal) + toVal, GridUnitType.Pixel);
            }
            else
            {
                return new GridLength(animationClock.CurrentProgress.Value * (toVal - fromVal) + fromVal, GridUnitType.Pixel);
            }
        }
    }
}
