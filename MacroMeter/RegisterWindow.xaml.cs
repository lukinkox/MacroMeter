using System.Windows;

namespace MacroMeter
{
    public partial class RegisterWindow : Window
    {
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
                MessageBox.Show("Vyplň všetko");
                return;
            }

            if (PassBox.Password != ConfirmBox.Password)
            {
                MessageBox.Show("Heslá nesedia");
                return;
            }         
            User user = new User
            {
                Meno = MenoBox.Text.Trim(),
                Priezvisko = PriezviskoBox.Text.Trim(),
                Email = EmailBox.Text.Trim(),
                Password = PassBox.Password
            };

            try
            {
                Database.SaveUser(user);
            }
            catch
            {
                MessageBox.Show("Email už existuje!");
                return;
            }
            MessageBox.Show("Idem ukladať usera...");
            Database.SaveUser(user);
            MessageBox.Show("User uložený!");
            MessageBox.Show("Registrácia úspešná");

            new LoginWindow().Show();
            Close();
        }
    }
}