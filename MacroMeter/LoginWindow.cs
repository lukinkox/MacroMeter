using System.Windows;

namespace MacroMeter
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PassBox.Password))
            {
                MessageBox.Show("Zadaj email a heslo");
                return;
            }

            User user = Database.GetUser(EmailBox.Text, PassBox.Password);

            if (user == null)
            {
                MessageBox.Show("Zlý email alebo heslo");
                return;
            }

            new SetupWindow(user).Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            Close();
        }
    }
}