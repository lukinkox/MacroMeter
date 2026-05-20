using System.Windows;

namespace MacroMeter
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Database.Initialize();

        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            MainWindow main = new MainWindow(user);
            main.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            Close();
        }
    }
}