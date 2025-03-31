using System.Windows.Controls;

namespace salemanagementApp.Models
{
    /// <summary>
    /// Interaction logic for UserModel.xaml
    /// </summary>
    public partial class UserModel : Page
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public string Status { get; set; }

        public UserModel()
        {
            InitializeComponent();
        }
    }
}
