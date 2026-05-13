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

            LoadDashboard();
        }

        private void LoadDashboard()
        {
            WeightText.Text = $"Aktuálna váha: {_user.Vaha} kg";

            double bmi = CalculateBMI(_user.Vaha, _user.Vyska);

            BMIText.Text = $"BMI: {bmi:F1} ({GetBMICategory(bmi)})";

            double calories = CalculateCalories();

            CaloriesText.Text = $"Denné kalórie: {calories:F0} kcal";
        }

        private double CalculateBMI(double weight, int height)
        {
            double heightMeters = height / 100.0;

            return weight / (heightMeters * heightMeters);
        }

        private string GetBMICategory(double bmi)
        {
            if (bmi < 18.5)
                return "Podváha";

            if (bmi < 25)
                return "Normálna váha";

            if (bmi < 30)
                return "Nadváha";

            return "Obezita";
        }

        private double CalculateCalories()
        {
            double bmr;

            if (_user.Pohlavie == "Muž")
            {
                bmr = 10 * _user.Vaha +
                      6.25 * _user.Vyska -
                      5 * _user.Vek + 5;
            }
            else
            {
                bmr = 10 * _user.Vaha +
                      6.25 * _user.Vyska -
                      5 * _user.Vek - 161;
            }

            double multiplier = 1.2;

            switch (_user.Aktivita)
            {
                case "Nízka":
                    multiplier = 1.2;
                    break;

                case "Stredná":
                    multiplier = 1.55;
                    break;

                case "Vysoká":
                    multiplier = 1.9;
                    break;
            }

            return bmr * multiplier;
        }

        private void SearchFood_Click(object sender, RoutedEventArgs e)
        {
            new FoodSearchWindow().ShowDialog();
        }

        private void DailyIntake_Click(object sender, RoutedEventArgs e)
        {
            string info =
                $"Denný prehľad:\n\n" +
                $"Váha: {_user.Vaha} kg\n" +
                $"BMI: {CalculateBMI(_user.Vaha, _user.Vyska):F1}\n" +
                $"Odporúčané kalórie: {CalculateCalories():F0} kcal";

            MessageBox.Show(info, "Denný príjem");
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            new AddFoodWindow().ShowDialog();
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
            Close();
        }

    }
}
