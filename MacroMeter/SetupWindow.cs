using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public class SetupWindow : Window
    {
        ComboBox gender;
        ComboBox activity;
        ComboBox goal;

        TextBox age;
        TextBox height;

        public SetupWindow()
        {
            Title = "MacroMeter - Nastavenie";
            Width = 400;
            Height = 500;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Grid grid = new Grid();
            grid.Margin = new Thickness(20);

            StackPanel panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "Základné údaje",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 20),
                HorizontalAlignment = HorizontalAlignment.Center
            });

            panel.Children.Add(new TextBlock { Text = "Pohlavie" });
            gender = new ComboBox { Margin = new Thickness(0, 5, 0, 10) };
            gender.Items.Add("Muž");
            gender.Items.Add("Žena");
            panel.Children.Add(gender);

            // Vek
            panel.Children.Add(new TextBlock { Text = "Vek" });
            age = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(age);

            panel.Children.Add(new TextBlock { Text = "Výška (cm)" });
            height = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(height);

            panel.Children.Add(new TextBlock { Text = "Aktivita" });
            activity = new ComboBox { Margin = new Thickness(0, 5, 0, 10) };
            activity.Items.Add("Sedavý život");
            activity.Items.Add("Ľahká aktivita");
            activity.Items.Add("Stredná aktivita");
            activity.Items.Add("Vysoká aktivita");
            panel.Children.Add(activity);

            panel.Children.Add(new TextBlock { Text = "Cieľ" });
            goal = new ComboBox { Margin = new Thickness(0, 5, 0, 20) };
            goal.Items.Add("Schudnúť");
            goal.Items.Add("Udržať váhu");
            goal.Items.Add("Pribrať svaly");
            panel.Children.Add(goal);

            Button save = new Button
            {
                Content = "Pokračovať",
                Height = 40,
                Background = Brushes.Green,
                Foreground = Brushes.White
            };

            save.Click += Save_Click;

            panel.Children.Add(save);

            grid.Children.Add(panel);
            Content = grid;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (gender.SelectedItem == null ||
                activity.SelectedItem == null ||
                goal.SelectedItem == null ||
                string.IsNullOrWhiteSpace(age.Text) ||
                string.IsNullOrWhiteSpace(height.Text))
            {
                MessageBox.Show("Vyplň všetky údaje");
                return;
            } 
            if (!int.TryParse(age.Text, out int vek))
            {
                MessageBox.Show("Vek musí byť číslo!");
                return;
            }

            if (!int.TryParse(height.Text, out int vyska))
            {
                MessageBox.Show("Výška musí byť číslo!");
                return;
            }

            string pohlavie = gender.SelectedItem.ToString();
            string aktivita = activity.SelectedItem.ToString();
            string ciel = goal.SelectedItem.ToString();

            MessageBox.Show(
                "Údaje uložené ✅\n\n" +
                $"Vek: {vek}\n" +
                $"Výška: {vyska}\n" +
                $"Pohlavie: {pohlavie}\n" +
                $"Aktivita: {aktivita}\n" +
                $"Cieľ: {ciel}"
            );

            Close();
        }
    }
}