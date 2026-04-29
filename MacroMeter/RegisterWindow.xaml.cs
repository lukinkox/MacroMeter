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

            new LoginWindow().Show();
            Close();
        }

    }
}