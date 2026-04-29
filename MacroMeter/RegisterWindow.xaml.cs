using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public partial class RegisterWindow : Window
    {
        TextBox meno, priezvisko, email;
        PasswordBox pass, confirm;
        CheckBox terms;
        private User user;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (pass.Password != confirm.Password)
            {
                MessageBox.Show("Heslá nesedia");
                return;
            }

            User user = new User()
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