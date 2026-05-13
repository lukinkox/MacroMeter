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
                Password = PassBox.Password,

                Vaha = 0,
                CielovaVaha = 0,
                Vek = 0,
                Vyska = 0,
                Pohlavie = "",
                Aktivita = "",
                Ciel = ""
            };


            MessageBox.Show("Registrácia úspešná");

            new SetupWindow(user).Show();
            Close();
        }

        private void EmailBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();

            login.Show();
            this.Close();
        }
    }
}