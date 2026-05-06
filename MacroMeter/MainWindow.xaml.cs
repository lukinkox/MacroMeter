using System.Windows;

namespace MacroMeter
{
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            InitializeComponent();
        }

        private void SearchFood_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("vyhladavanie jedla");
        }

        private void DailyIntake_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tu je dennny prijem");
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("tu bdue vlastne jedlo");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("nastavenia");
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
 

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("profil usera");
        }
    }
}
