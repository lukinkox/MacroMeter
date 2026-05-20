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

        private ObservableCollection<FoodEntry> CentralnaDatabazaJedal = new ObservableCollection<FoodEntry>();

        public MainWindow(User user)
        {
            InitializeComponent();
            _user = user;

            WelcomeText.Text = $"Vitaj, {_user.Meno}! 👋";

            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Kuracie prsia raw", Kalorie = 120, Bielkoviny = 23, Sacharidy = 0, Tuky = 2.6 });
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Ryža Basmati raw", Kalorie = 350, Bielkoviny = 7.5, Sacharidy = 78, Tuky = 0.6 });
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Vajíčko celé (ks)", Kalorie = 143, Bielkoviny = 12.6, Sacharidy = 0.7, Tuky = 9.5 });
            CentralnaDatabazaJedal.Add(new FoodEntry { Nazov = "Banán", Kalorie = 89, Bielkoviny = 1.1, Sacharidy = 23, Tuky = 0.3 });

            DatabaseListBox.ItemsSource = CentralnaDatabazaJedal;
            weightHistory.Add(new WeightRecord { Date = DateTime.Now.AddDays(-15), Weight = _user.Vaha + 2.5 });
            weightHistory.Add(new WeightRecord { Date = DateTime.Now.AddDays(-10), Weight = _user.Vaha + 1.8 });
            weightHistory.Add(new WeightRecord { Date = DateTime.Now.AddDays(-5), Weight = _user.Vaha + 0.6 });
            weightHistory.Add(new WeightRecord { Date = DateTime.Now, Weight = _user.Vaha });

            UpdateDashboardValues();
            WeightChartCanvas.SizeChanged += (s, e) => DrawWeightChart();
            HighlightActiveMenu(MenuDashboardBtn);
        }

        private void HighlightActiveMenu(Button activeBtn)
        {
            var menuButtons = new List<Button> { MenuDashboardBtn, MenuZapisatPrijemBtn, MenuDailyIntakeBtn, MenuAddFoodBtn, MenuProfileBtn };

            foreach (var btn in menuButtons)
            {
                if (btn == null) continue;

                if (btn == activeBtn)
                {
                    btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495E"));
                    btn.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    btn.ClearValue(Button.BackgroundProperty);
                    btn.ClearValue(Button.ForegroundProperty);
                }
            }
        }

        private void HideAllSections()
        {
            DashboardSection.Visibility = Visibility.Collapsed;
            AddFoodSection.Visibility = Visibility.Collapsed;
            DailyIntakeSection.Visibility = Visibility.Collapsed;
            ProfileSection.Visibility = Visibility.Collapsed;
            ZapisatPrijemSection.Visibility = Visibility.Collapsed;
        }

        private void SearchFood_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            DashboardSection.Visibility = Visibility.Visible;
            HighlightActiveMenu(MenuDashboardBtn);
            DrawWeightChart();
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            AddFoodSection.Visibility = Visibility.Visible;
            HighlightActiveMenu(MenuAddFoodBtn);
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ZapisatPrijemMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            ZapisatPrijemSection.Visibility = Visibility.Visible;
            HighlightActiveMenu(MenuZapisatPrijemBtn);

            FoodSearchBox.Text = "";
            GramazInput.Text = "";
            DatabaseListBox.ItemsSource = CentralnaDatabazaJedal;
        }

        private void DailyIntake_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            DailyIntakeSection.Visibility = Visibility.Visible;
            HighlightActiveMenu(MenuDailyIntakeBtn);
            UpdateDashboardValues();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            HideAllSections();
            ProfileSection.Visibility = Visibility.Visible;
            HighlightActiveMenu(MenuProfileBtn);

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

            FoodEntry noveJedloDB = new FoodEntry
            {
                Nazov = FoodNameInput.Text,
                Kalorie = zadaneKcal,
                Bielkoviny = isProtOk ? zadaneProt : 0,
                Sacharidy = isCarbOk ? zadaneCarb : 0,
                Tuky = isFatOk ? zadaneFat : 0
            };

            CentralnaDatabazaJedal.Add(noveJedloDB);

            FoodNameInput.Clear();
            CaloriesInput.Clear();
            ProteinsInput.Clear();
            CarbsInput.Clear();
            FatsInput.Clear();

            MessageBox.Show($"Jedlo '{noveJedloDB.Nazov}' bolo úspešne pridané do šablón databázy!");
            ZapisatPrijemMenu_Click(sender, e);
        }

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

        private void ConfirmZapisPrijmu_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseListBox.SelectedItem is FoodEntry vybrateJedloNa100g)
            {
                if (!double.TryParse(GramazInput.Text, out double gramy) || gramy <= 0)
                {
                    MessageBox.Show("Zadajte platné množstvo v gramoch (číslo väčšie ako 0).", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double koeficient = gramy / 100.0;
                string vybranyCas = (ComboTimeline.SelectedItem as ComboBoxItem).Content.ToString();

                double vypocitaneKcal = vybrateJedloNa100g.Kalorie * koeficient;
                double vypocitaneProt = vybrateJedloNa100g.Bielkoviny * koeficient;
                double vypocitaneCarb = vybrateJedloNa100g.Sacharidy * koeficient;
                double vypocitaneFat = vybrateJedloNa100g.Tuky * koeficient;

                eatenCalories += vypocitaneKcal;
                eatenProteins += vypocitaneProt;
                eatenCarbs += vypocitaneCarb;
                eatenFats += vypocitaneFat;

                if (_user.DennýPrijem != null)
                {
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
                }

                UpdateDashboardValues();
                GramazInput.Clear();
                FoodSearchBox.Text = "";

                MessageBox.Show($"Jedlo úspešne pridané do sekcie {vybranyCas}!");
                SearchFood_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Vyberte kliknutím jedlo zo zoznamu pred stlačením zápisu.", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private double CalculateCalories()
        {
            return 2500; 
        }
        private double CalculateBMI(double vaha, double vyska)
        {
            if (vyska <= 0) return 0;
            double vyskaM = vyska / 100.0;
            return vaha / (vyskaM * vyskaM);
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
                Stroke = new SolidColorBrush(Color.FromRgb(52, 152, 219)), // Modrá farba #3498DB
                StrokeThickness = 3
            };

            for (int i = 0; i < weightHistory.Count; i++)
            {
                double x = paddingX + (i * (width - 2 * paddingX) / (weightHistory.Count - 1));
                double y = height - paddingY - ((weightHistory[i].Weight - minW) * (height - 2 * paddingY) / (maxW - minW));
                progressLine.Points.Add(new Point(x, y));

                Ellipse pointCircle = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = new SolidColorBrush(Color.FromRgb(44, 62, 80)),
                    Stroke = Brushes.White,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(pointCircle, x - 4);
                Canvas.SetTop(pointCircle, y - 4);
                WeightChartCanvas.Children.Add(pointCircle);
                TextBlock weightLabel = new TextBlock
                {
                    Text = $"{weightHistory[i].Weight:F1} kg",
                    FontSize = 10,
                    Foreground = Brushes.Gray
                };

                weightLabel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Canvas.SetLeft(weightLabel, x - (weightLabel.DesiredSize.Width / 2));
                Canvas.SetTop(weightLabel, y - 20);

                WeightChartCanvas.Children.Add(weightLabel);
            }

            WeightChartCanvas.Children.Insert(0, progressLine);
        }
    }
}