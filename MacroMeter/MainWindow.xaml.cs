using System;
using System.Windows;
using System.Windows.Controls;

namespace MacroMeter
{
    public partial class MainWindow : Window
    {
        private User _user;
        private double eatenToday = 0;

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
            double targetCalories = CalculateCalories();
            ProteinsText.Text = $"0g / {(_user.Vaha * 1.8):F0}g";
            CarbsText.Text = $"0g / {(targetCalories * 0.5 / 4):F0}g";
            FatsText.Text = $"0g / {(targetCalories * 0.25 / 9):F0}g";
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            ProfileSection.Visibility = Visibility.Visible;

            ProfileFullName.Text = $"{_user.Meno} {_user.Priezvisko}";
            ProfileEmail.Text = _user.Email;
            ProfileAge.Text = $"{_user.Vek} rokov";
            ProfileHeight.Text = $"{_user.Vyska} cm";
            ProfileActivity.Text = _user.Aktivita;
            ProfileGoal.Text = _user.Ciel;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
        private void SaveFood_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodNameInput.Text) ||
                !double.TryParse(CaloriesInput.Text, out double zadaneKalorie))
            {
                MessageBox.Show("Zadajte platné údaje o jedle.");
                return;
            }

            eatenToday += zadaneKalorie;
            UpdateDashboardValues();

            FoodNameInput.Clear();
            AmountInput.Clear();
            CaloriesInput.Clear();

            MessageBox.Show("Jedlo pridané!");
            SearchFood_Click(sender, e); 
        }

        private void UpdateDashboardValues()
        {
            double bmi = CalculateBMI(_user.Vaha, _user.Vyska);
            BMIText.Text = $"{bmi:F1}";

            double targetCalories = CalculateCalories();
            double remaining = targetCalories - eatenToday;

            CaloriesText.Text = $"{eatenToday:F0} / {targetCalories:F0} kcal";
            RemainingCaloriesText.Text = remaining > 0 ? $"Zostáva {remaining:F0} kcal" : "Cieľ splnený!";

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
            double bmr = (_user.Pohlavie == "Muž")
                ? (10 * _user.Vaha) + (6.25 * _user.Vyska) - (5 * _user.Vek) + 5
                : (10 * _user.Vaha) + (6.25 * _user.Vyska) - (5 * _user.Vek) - 161;

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