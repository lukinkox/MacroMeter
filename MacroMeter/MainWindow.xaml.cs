using System;
using System.Windows;

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
            double calories = CalculateCalories();
            MessageBox.Show($"Tvoj odporúčaný denný príjem je: {calories:F0} kcal", "Denný príjem");
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            string profile =
                $"Meno: {_user.Meno}\n" +
                $"Priezvisko: {_user.Priezvisko}\n" +
                $"Email: {_user.Email}\n" +
                $"Vek: {_user.Vek}\n" +
                $"Výška: {_user.Vyska} cm\n" +
                $"Váha: {_user.Vaha} kg";

            MessageBox.Show(profile, "Profil používateľa 👤");
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

            double calories = CalculateCalories();
            CaloriesText.Text = $"0 / {calories:F0} kcal";

            WeightText.Text = $"{_user.Vaha} kg";
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