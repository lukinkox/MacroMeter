using System.Windows;

namespace MacroMeter
{
    public partial class RegisterWindow : Window
    {
        private User user;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MenoBox.Text) ||
                string.IsNullOrWhiteSpace(PriezviskoBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show("Vyplň všetky polia");
                return;
            }

            if (PassBox.Password != ConfirmBox.Password)
            {
                MessageBox.Show("Heslá nesedia");
                return;

            }
        
            user = new User
            {
                Meno = MenoBox.Text.Trim(),
                Priezvisko = PriezviskoBox.Text.Trim(),
                Email = EmailBox.Text.Trim(),
                Password = PassBox.Password
            };
            SetupWindow setup = new SetupWindow(user);
            setup.Show();

            Close();
        }
    }
}
