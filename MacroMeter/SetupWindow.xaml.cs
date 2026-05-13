using System;
using System.Windows;
using System.Windows.Controls;

namespace MacroMeter
{
    public partial class SetupWindow : Window
    {
        private User _user;

        public SetupWindow(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(AgeBox.Text, out int vek) ||
                    !int.TryParse(HeightBox.Text, out int vyska) ||
                    !double.TryParse(CurrentWeightBox.Text, out double aktualnaVaha) ||
                    !double.TryParse(GoalWeightBox.Text, out double cielovaVaha))
                {
                    MessageBox.Show("Zadajte prosím platné číselné údaje pre vek, výšku a váhu.");
                    return;
                }

                if (GenderBox.SelectedItem == null ||
                    ActivityBox.SelectedItem == null ||
                    GoalBox.SelectedItem == null)
                {
                    MessageBox.Show("Prosím, vyplňte všetky výbery (pohlavie, aktivita a cieľ).");
                    return;
                }


                _user.Vek = vek;
                _user.Vyska = vyska;
                _user.Vaha = aktualnaVaha;
                _user.CielovaVaha = cielovaVaha;

                _user.Pohlavie = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.Aktivita = (ActivityBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.Ciel = (GoalBox.SelectedItem as ComboBoxItem)?.Content.ToString();

  
                if (Database.UserExists(_user.Email))
                {
                    MessageBox.Show("Používateľ s týmto emailom už existuje!");
                    return;
                }

                Database.SaveUser(_user);

                MessageBox.Show("Profil bol úspešne vytvorený!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainDash = new MainWindow(_user);
                mainDash.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vyskytla sa chyba: " + ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}