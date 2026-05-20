using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MacroMeter
{
    public class WeightRecord
    {
        public DateTime Date { get; set; }
        public double Weight { get; set; }
    }

    public partial class MainWindow : Window
    {
        private User _user;
        private double eatenCalories = 0;
        private double eatenProteins = 0;
        private double eatenCarbs = 0;
        private double eatenFats = 0;

        private List<WeightRecord> weightHistory = new List<WeightRecord>();

        // GLOBÁLNA SPOLOČNÁ DATABÁZA JEDÁL (Hodnoty prísne na 100g)
        private ObservableCollection<FoodEntry> CentralnaDatabazaJedal = new ObservableCollection<FoodEntry>();

        public MainWindow(User user)
        {
            InitializeComponent();
            _user = user;

            WelcomeText.Text = $"Vitaj, {_user.Meno}! 👋";

            // Predvolené jedlá pri štarte aplikácie (všetky na 100g)
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Kuracie prsia raw", Kalorie = 120, Bielkoviny = 23, Sacharidy = 0, Tuky = 2.6 });
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Ryža Basmati raw", Kalorie = 350, Bielkoviny = 7.5, Sacharidy = 78, Tuky = 0.6 });
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Vajíčko celé (ks)", Kalorie = 143, Bielkoviny = 12.6, Sacharidy = 0.7, Tuky = 9.5 });
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Banán", Kalorie = 89, Bielkoviny = 1.1, Sacharidy = 23, Tuky = 0.3 });

            // Priradenie k ListBoxu v UI
            DatabaseListBox.ItemsSource = CentralnaDatabazaJedal;

            weightHistory.Add(new WeightRecord { Date = DateTime.Now.AddDays(-15), Weight = _user.Vaha + 2.5 });
            weightHistory.Add(new WeightRecord { Date = DateTime.Now.AddDays(-10), Weight = _user.Vaha + 1.8 });
            weightHistory.Add(new WeightRecord { Date = DateTime.Now.AddDays(-5), Weight = _user.Vaha + 0.6 });
            weightHistory.Add(new WeightRecord { Date = DateTime.Now, Weight = _user.Vaha });

            UpdateDashboardValues();
            WeightChartCanvas.SizeChanged += (s, e) => DrawWeightChart();
        }

        private void HideAllSections()
        {
            DashboardSection.Visibility = Visibility.Collapsed;
            AddFoodSection.Visibility = Visibility.Collapsed;
            DailyIntakeSection.Visibility = Visibility.Collapsed;
            ProfileSection.Visibility = Visibility.Collapsed;
            ZapisatPrijemSection.Visibility = Visibility.Collapsed; // Skrytie novej sekcie
        }

        private void SearchFood_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            DashboardSection.Visibility = Visibility.Visible;
            DrawWeightChart(); 
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            AddFoodSection.Visibility = Visibility.Visible;
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        // Otvorenie novej sekcie pre zápis príjmu jedál
        private void ZapisatPrijemMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            ZapisatPrijemSection.Visibility = Visibility.Visible;
            
            // Reset textových polí
            FoodSearchBox.Text = "";
            GramazInput.Text = "";
            DatabaseListBox.ItemsSource = CentralnaDatabazaJedal;
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void RecordWeight_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(NewWeightInput.Text, out double novaVaha) && novaVaha > 0)
            {
                _user.Vaha = novaVaha; 
                weightHistory.Add(new WeightRecord { Date = DateTime.Now, Weight = novaVaha }); 

                NewWeightInput.Clear();
                UpdateDashboardValues();

                MessageBox.Show($"Nová váha {novaVaha} kg úspešne zaznamenaná!");
            }
            else
            {
                MessageBox.Show("Zadajte platné číslo pre váhu.");
            }
        }

        // ========================================================
        // 1. STRANA: UKLADANIE NOVÉHO JEDLA DO DATABÁZY (ŠABLÓNA)
        // ========================================================
        private void SaveFood_Click(object sender, RoutedEventArgs e)
        {
            bool isKcalOk = double.TryParse(CaloriesInput.Text, out double zadaneKcal);
            bool isProtOk = double.TryParse(ProteinsInput.Text, out double zadaneProt);
            bool isCarbOk = double.TryParse(CarbsInput.Text, out double zadaneCarb);
            bool isFatOk = double.TryParse(FatsInput.Text, out double zadaneFat);

            if (string.IsNullOrWhiteSpace(FoodNameInput.Text) || !isKcalOk)
            {
                MessageBox.Show("Zadajte aspoň názov jedla a kalórie na 100g.");
                return;
            }

            // Vytvoríme jedlo a pridáme ho do našej centrálnej databázy na 100g
            FoodEntry noveJedloDB = new FoodEntry
            {
                Nazov = FoodNameInput.Text,
                Kalorie = zadaneKcal,
                Bielkoviny = isProtOk ? zadaneProt : 0,
                Sacharidy = isCarbOk ? zadaneCarb : 0,
                Tuky = isFatOk ? zadaneFat : 0
            };

            CentralnaDatabazaJedal.Add(noveJedloDB);

            // Vyčistenie formuláru
            FoodNameInput.Clear();
            CaloriesInput.Clear();
            ProteinsInput.Clear();
            CarbsInput.Clear();
            FatsInput.Clear();

            MessageBox.Show($"Jedlo '{noveJedloDB.Nazov}' bolo úspešne pridané do šablón databázy!");
            
            // Automaticky prepneme používateľa na zápis príjmu, aby ho mohol rovno použiť
            ZapisatPrijemMenu_Click(sender, e);
        }

        // ========================================================
        // LIVE VYHĽADÁVANIE V DATABÁZE PRI PÍSANÍ
        // ========================================================
        private void FoodSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string hladanyText = FoodSearchBox.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(hladanyText))
            {
                DatabaseListBox.ItemsSource = CentralnaDatabazaJedal;
            }
            else
            {
                var prefiltrovane = CentralnaDatabazaJedal
                    .Where(j => j.Nazov.ToLower().Contains(hladanyText))
                    .ToList();

                DatabaseListBox.ItemsSource = prefiltrovane;
            }
        }

        // ========================================================
        // 2. STRANA: REÁLNY ZÁPIS VYBRANÉHO JEDLA DO DENNÉHO PRÍJMU
        // ========================================================
        private void ConfirmZapisPrijmu_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseListBox.SelectedItem is FoodEntry vybrateJedloNa100g)
            {
                if (!double.TryParse(GramazInput.Text, out double gramy) || gramy <= 0)
                {
                    MessageBox.Show("Zadajte platné množstvo v gramoch (číslo väčšie ako 0).", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Matematický prepočet: gramy / 100
                double koeficient = gramy / 100.0;
                string vybranyCas = (ComboTimeline.SelectedItem as ComboBoxItem).Content.ToString();

                // Výpočet hodnôt na základe gramáže
                double vypocitaneKcal = vybrateJedloNa100g.Kalorie * koeficient;
                double vypocitaneProt = vybrateJedloNa100g.Bielkoviny * koeficient;
                double vypocitaneCarb = vybrateJedloNa100g.Sacharidy * koeficient;
                double vypocitaneFat = vybrateJedloNa100g.Tuky * koeficient;

                // Pripočítanie ku globálnym denným štatistikám
                eatenCalories += vypocitaneKcal;
                eatenProteins += vypocitaneProt;
                eatenCarbs += vypocitaneCarb;
                eatenFats += vypocitaneFat;

                // Pridanie záznamu do objektu usera pre neskorší zoznam denného príjmu
                _user.DennýPrijem.Add(new FoodEntry
                {
                    Nazov = vybrateJedloNa100g.Nazov,
                    Gramaz = gramy,
                    CasDna = vybranyCas,
                    Kalorie = vypocitaneKcal,
                    Bielkoviny = vypocitaneProt,
                    Sacharidy = vypocitaneCarb,
                    Tuky = vypocitaneFat
                });

                UpdateDashboardValues();
                GramazInput.Clear();
                FoodSearchBox.Text = "";

                MessageBox.Show($"Jedlo úspešne pridané do sekcie {vybranyCas}!");
                
                // Prepneme používateľa na hlavnú plochu
                SearchFood_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Vyberte kliknutím jedlo zo zoznamu pred stlačením zápisu.", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateDashboardValues()
        {
            double targetCalories = CalculateCalories();
            double targetProteins = _user.Vaha * 1.8;
            double targetCarbs = (targetCalories * 0.5) / 4;
            double targetFats = (targetCalories * 0.25) / 9;
            double bmi = CalculateBMI(_user.Vaha, _user.Vyska);
            
            if (BMIText != null) BMIText.Text = $"{bmi:F1}";

            double remaining = targetCalories - eatenCalories;
            CaloriesText.Text = $"{eatenCalories:F0} / {targetCalories:F0} kcal";
            RemainingCaloriesText.Text = remaining > 0 ? $"Zostáva {remaining:F0} kcal" : "Cieľ splnený!";

            WeightText.Text = $"{_user.Vaha} kg";
            TargetWeightText.Text = $"{_user.CielovaVaha} kg";

            ProteinsText.Text = $"{eatenProteins:F0}g / {targetProteins:F0}g";
            CarbsText.Text = $"{eatenCarbs:F0}g / {targetCarbs:F0}g";
            FatsText.Text = $"{eatenFats:F0}g / {targetFats:F0}g";

            DrawWeightChart();
        }

        private void DrawWeightChart()
        {
            if (WeightChartCanvas == null || NoChartDataText == null) return;

            WeightChartCanvas.Children.Clear();

            if (weightHistory.Count < 2)
            {
                NoChartDataText.Visibility = Visibility.Visible;
                return;
            }

            NoChartDataText.Visibility = Visibility.Collapsed;

            double width = WeightChartCanvas.ActualWidth;
            double height = WeightChartCanvas.ActualHeight;

            if (width < 50 || height < 50) return;

            double paddingX = 40;
            double paddingY = 30;

            double minW = double.MaxValue;
            double maxW = double.MinValue;

            foreach (var r in weightHistory)
            {
                if (r.Weight < minW) minW = r.Weight;
                if (r.Weight > maxW) maxW = r.Weight;
            }

            if (maxW == minW)
            {
                minW -= 5;
                maxW += 5;
            }

            Polyline progressLine = new Polyline
            {
                Stroke = new SolidColorBrush(Color.FromRgb(52, 152, 219)),
                StrokeThickness = 3
            };

            for (int i = 0; i < weightHistory.Count; i++)
            {
                double x = paddingX + (i * (width - 2 * paddingX) / (weightHistory.Count - 1));
                double y = height - paddingY - ((weightHistory[i].Weight - minW) * (height - 2 * paddingY) / (maxW - minW));

                Point point = new Point(x, y);
                progressLine.Points.Add(point);

                Ellipse dot = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = new SolidColorBrush(Color.FromRgb(230, 126, 34)),
                    Margin = new Thickness(x - 4, y - 4, 0, 0)
                };
                WeightChartCanvas.Children.Add(dot);

                TextBlock weightLabel = new TextBlock
                {
                    Text = $"{weightHistory[i].Weight:F1} kg",
                    FontSize = 11,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Color.FromRgb(44, 62, 80))
                };
                Canvas.SetLeft(weightLabel, x - 18);
                Canvas.SetTop(weightLabel, y - 20);
                WeightChartCanvas.Children.Add(weightLabel);

                TextBlock dateLabel = new TextBlock
                {
                    Text = weightHistory[i].Date.ToString("d.M."),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(127, 133, 141))
                };
                Canvas.SetLeft(dateLabel, x - 12);
                Canvas.SetTop(dateLabel, height - 15);
                WeightChartCanvas.Children.Add(dateLabel);
            }

            WeightChartCanvas.Children.Add(progressLine);
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