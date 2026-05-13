using System;
using System.Windows;
using System.Windows.Controls;

namespace MacroMeter
{
    public partial class MainWindow : Window
    {
        private User _user;

        public MainWindow(User user)
        {
            InitializeComponent();
            _user = user;

            WelcomeText.Text = $"Vitaj, {_user.Meno}! 👋";

            UpdateDashboardValues();
        }

        private void HideAllSections()
        {
            DashboardSection.Visibility = Visibility.Collapsed;
            AddFoodSection.Visibility = Visibility.Collapsed;
            DailyIntakeSection.Visibility = Visibility.Collapsed;
            ProfileSection.Visibility = Visibility.Collapsed;
        }

        private void SearchFood_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            DashboardSection.Visibility = Visibility.Visible;
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            AddFoodSection.Visibility = Visibility.Visible;
        }

        private void DailyIntake_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            DailyIntakeSection.Visibility = Visibility.Visible;

            // Výpočet cieľových makroživín (orientačné hodnoty)
            double targetCalories = CalculateCalories();

            // Bielkoviny: 1.8g na kg váhy
            ProteinsText.Text = $"0g / {(_user.Vaha * 1.8):F0}g";
            // Sacharidy: cca 50% kalórií (1g = 4 kcal)
            CarbsText.Text = $"0g / {(targetCalories * 0.5 / 4):F0}g";
            // Tuky: cca 25% kalórií (1g = 9 kcal)
            FatsText.Text = $"0g / {(targetCalories * 0.25 / 9):F0}g";
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            ProfileSection.Visibility = Visibility.Visible;

            // Naplnenie údajov v sekcii profilu
            ProfileFullName.Text = $"{_user.Meno} {_user.Priezvisko}";
            ProfileEmail.Text = _user.Email;
            ProfileAge.Text = $"{_user.Vek} rokov";
            ProfileHeight.Text = $"{_user.Vyska} cm";
            ProfileActivity.Text = _user.Aktivita;
            ProfileGoal.Text = _user.Ciel;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void UpdateDashboardValues()
        {
            double bmi = CalculateBMI(_user.Vaha, _user.Vyska);
            BMIText.Text = $"{bmi:F1}";

            double targetCalories = CalculateCalories();
            double eatenToday = 0;
            double remaining = targetCalories - eatenToday;

            CaloriesText.Text = $"{eatenToday:F0} / {targetCalories:F0} kcal";
            RemainingCaloriesText.Text = $"Zostáva doplniť {remaining:F0} kcal";

            WeightText.Text = $"{_user.Vaha} kg";
            TargetWeightText.Text = $"{_user.CielovaVaha} kg";
        }

        private double CalculateBMI(double weight, int height)
        {
            double heightMeters = height / 100.0;
            return weight / (heightMeters * heightMeters);
        }

        private double CalculateCalories()
        {
            double bmr;
            if (_user.Pohlavie == "Muž")
            {
                bmr = (10 * _user.Vaha) + (6.25 * _user.Vyska) - (5 * _user.Vek) + 5;
            }
            else
            {
                bmr = (10 * _user.Vaha) + (6.25 * _user.Vyska) - (5 * _user.Vek) - 161;
            }

            double multiplier = _user.Aktivita switch
            {
                "Nízka" => 1.2,
                "Stredná" => 1.55,
                "Vysoká" => 1.9,
                _ => 1.2
            };

            return bmr * multiplier;
        }
    }
}