using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MacroMeter
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MenoBox.Text) || string.IsNullOrWhiteSpace(EmailBox.Text) || string.IsNullOrWhiteSpace(PassBox.Password))
            {
                MessageBox.Show("Prosím, vyplňte všetky povinné údaje.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User newUser = new User
            {
                Meno = MenoBox.Text,
                Priezvisko = PriezviskoBox.Text,
                Email = EmailBox.Text,
                Password = PassBox.Password
            };

            LoadingWindow setupLoading = new LoadingWindow();
            setupLoading.StatusText.Text = "Vytváram váš profil...";
            setupLoading.LoadingBar.Foreground = new SolidColorBrush(Colors.MediumSeaGreen);
            setupLoading.Show();

            this.Close();

            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(15);
                setupLoading.LoadingBar.Value = i;
                setupLoading.PercentText.Text = i + "%";

                if (i == 50) setupLoading.StatusText.Text = "Pripravujem dotazník...";
            }

            SetupWindow setup = new SetupWindow(newUser);
            setup.Show();
            setupLoading.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void EmailBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
          
        }
    }
}