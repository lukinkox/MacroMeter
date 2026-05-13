using System;
using System.Windows;
using System.Windows.Controls;

namespace MacroMeter
{
    public partial class MainWindow : Window
    {
        private User _user;
        private double eatenCalories = 0;
        private double eatenProteins = 0;
        private double eatenCarbs = 0;
        private double eatenFats = 0;

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
            UpdateDashboardValues(); 
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
            bool isKcalOk = double.TryParse(CaloriesInput.Text, out double zadaneKcal);
            bool isProtOk = double.TryParse(ProteinsInput.Text, out double zadaneProt);
            bool isCarbOk = double.TryParse(CarbsInput.Text, out double zadaneCarb);
            bool isFatOk = double.TryParse(FatsInput.Text, out double zadaneFat);

            if (string.IsNullOrWhiteSpace(FoodNameInput.Text) || !isKcalOk)
            {
                MessageBox.Show("Zadajte aspoň názov jedla a kalórie.");
                return;
            }

            eatenCalories += zadaneKcal;
            eatenProteins += isProtOk ? zadaneProt : 0;
            eatenCarbs += isCarbOk ? zadaneCarb : 0;
            eatenFats += isFatOk ? zadaneFat : 0;

            UpdateDashboardValues();

            FoodNameInput.Clear();
            AmountInput.Clear();
            CaloriesInput.Clear();
            ProteinsInput.Clear();
            CarbsInput.Clear();
            FatsInput.Clear();

            MessageBox.Show("Jedlo pridané!");
            SearchFood_Click(sender, e);
        }

        private void UpdateDashboardValues()
        {
            double targetCalories = CalculateCalories();
            double targetProteins = _user.Vaha * 1.8; 
            double targetCarbs = (targetCalories * 0.5) / 4;
            double targetFats = (targetCalories * 0.25) / 9;         
            double bmi = CalculateBMI(_user.Vaha, _user.Vyska);
            BMIText.Text = $"{bmi:F1}";

            double remaining = targetCalories - eatenCalories;
            CaloriesText.Text = $"{eatenCalories:F0} / {targetCalories:F0} kcal";
            RemainingCaloriesText.Text = remaining > 0 ? $"Zostáva {remaining:F0} kcal" : "Cieľ splnený!";

            WeightText.Text = $"{_user.Vaha} kg";
            TargetWeightText.Text = $"{_user.CielovaVaha} kg";
        
            ProteinsText.Text = $"{eatenProteins:F0}g / {targetProteins:F0}g";
            CarbsText.Text = $"{eatenCarbs:F0}g / {targetCarbs:F0}g";
            FatsText.Text = $"{eatenFats:F0}g / {targetFats:F0}g";
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