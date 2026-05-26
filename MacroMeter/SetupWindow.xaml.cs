using System;
using System.Windows;
using System.Windows.Controls;

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
                    GoalBox.SelectedItem == null ||
                    DietBox.SelectedItem == null)
                {
                    MessageBox.Show("Prosím, vyplňte všetky výbery (pohlavie, aktivita, cieľ a diétu).");
                    return;
                }

                // 2. Priradenie hodnot user
                _user.Vek = vek;
                _user.Vyska = vyska;
                _user.Vaha = aktualnaVaha;
                _user.CielovaVaha = cielovaVaha;

                _user.Pohlavie = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.Aktivita = (ActivityBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.Ciel = (GoalBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                _user.Dieta = (DietBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                double bmr = (10 * aktualnaVaha) + (6.25 * vyska) - (5 * vek);
                bmr = (_user.Pohlavie == "Muž") ? bmr + 5 : bmr - 161;

                //aktivita podľa výberu
                double nasobitel = 1.2;
                if (_user.Aktivita.Contains("Ľahko")) nasobitel = 1.375;
                else if (_user.Aktivita.Contains("Aktívny")) nasobitel = 1.55;
                else if (_user.Aktivita.Contains("Veľmi")) nasobitel = 1.725;

                double kalorie = bmr * nasobitel;

                // upava kalori podla ciela
                if (_user.Ciel == "Schudnúť") kalorie -= 400;
                else if (_user.Ciel == "Pribrať svaly") kalorie += 300;

                //rozdelenie makier podla diety
                double pBielkoviny = 0.25, pSacharidy = 0.50, pTuky = 0.25; // Predvolená Vyvážená

                switch (_user.Dieta)
                {
                    case "Nízkosacharidová":
                        pBielkoviny = 0.30;
                        pSacharidy = 0.20;
                        pTuky = 0.50;
                        break;
                    case "Keto":
                        pBielkoviny = 0.25;
                        pSacharidy = 0.05;
                        pTuky = 0.70;
                        break;
                    case "Vysokobielkovinová":
                        pBielkoviny = 0.40;
                        pSacharidy = 0.35;
                        pTuky = 0.25;
                        break;
                }

                _user.TargetCalories = Math.Round(kalorie);
                _user.TargetProteins = Math.Round((kalorie * pBielkoviny) / 4);
                _user.TargetCarbs = Math.Round((kalorie * pSacharidy) / 4);
                _user.TargetFats = Math.Round((kalorie * pTuky) / 9);


                // kontrola usera
                if (Database.UserExists(_user.Email))
                {
                    MessageBox.Show("Používateľ s týmto emailom už existuje!");
                    return;
                }

                Database.SaveUser(_user);

                MessageBox.Show("Profil bol úspešne vytvorený!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

                // Otvorenie mainwindow
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