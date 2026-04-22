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
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show("Vyplňte všetky polia.");
                return;
            }

            if (TermsCheckBox.IsChecked != true)
            {
                MessageBox.Show("Musíte súhlasiť s podmienkami.");
                return;
            }

            var user = new User
            {
                Meno = FirstNameBox.Text.Trim(),
                Priezvisko = LastNameBox.Text.Trim(),
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