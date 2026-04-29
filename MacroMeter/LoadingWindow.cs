using System.Windows;

namespace MacroMeter
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Title = "MacroMeter Login";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show("Vyplňte všetky polia.");
                return;
            }


            var user = new User
            {
                Email = EmailBox.Text.Trim()
            };

            var setup = new SetupWindow(user);
            setup.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            Close();
        }
    }
}