using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public class SetupWindow : Window
    {
        private User _user;

        ComboBox gender, activity, goal;
        TextBox age, height;

        public SetupWindow(User user)
        {
            _user = user;

            Title = "Setup";
            Width = 420;
            Height = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            StackPanel panel = new StackPanel { Margin = new Thickness(25) };

            panel.Children.Add(new TextBlock
            {
                Text = "Setup",
                FontSize = 28,
                FontWeight = FontWeights.SemiBold
            });

            gender = Combo();
            gender.Items.Add("Muž");
            gender.Items.Add("Žena");

            activity = Combo();
            activity.Items.Add("Sedavý");
            activity.Items.Add("Aktívny");

            goal = Combo();
            goal.Items.Add("Schudnúť");
            goal.Items.Add("Udržať");
            goal.Items.Add("Pribrať");

            panel.Children.Add(Label("Pohlavie")); panel.Children.Add(gender);
            panel.Children.Add(Label("Vek")); age = Box(); panel.Children.Add(age);
            panel.Children.Add(Label("Výška")); height = Box(); panel.Children.Add(height);
            panel.Children.Add(Label("Aktivita")); panel.Children.Add(activity);
            panel.Children.Add(Label("Cieľ")); panel.Children.Add(goal);

            Button btn = new Button
            {
                Content = "Pokračovať",
                Background = Brushes.Green,
                Foreground = Brushes.White,
                Height = 40
            };

            btn.Click += Save_Click;
            panel.Children.Add(btn);

            Content = panel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(age.Text, out int vek) ||
                !int.TryParse(height.Text, out int vyska))
            {
                MessageBox.Show("Zlé čísla");
                return;
            }

            _user.Vek = vek;
            _user.Vyska = vyska;
            _user.Pohlavie = gender.SelectedItem?.ToString();
            _user.Aktivita = activity.SelectedItem?.ToString();
            _user.Ciel = goal.SelectedItem?.ToString();

            MainWindow main = new MainWindow(_user);
            main.Show();
            Close();
        }

        TextBlock Label(string t) => new TextBlock { Text = t, Margin = new Thickness(0, 10, 0, 5) };
        TextBox Box() => new TextBox { Height = 30 };
        ComboBox Combo() => new ComboBox { Height = 30 };
    }
}