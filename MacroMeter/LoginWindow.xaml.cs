using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public partial class LoginWindow : Window
    {
        TextBox firstName;
        TextBox lastName;
        TextBox email;
        CheckBox terms;

        public LoginWindow()
        {
            InitializeComponent();

            Title = "MacroMeter Login";
            Width = 400;
            Height = 450;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Grid grid = new Grid();
            grid.Margin = new Thickness(20);

            StackPanel panel = new StackPanel();

            TextBlock title = new TextBlock
            {
                Text = "MacroMeter",
                FontSize = 26,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            panel.Children.Add(title);

            panel.Children.Add(new TextBlock { Text = "Meno" });
            firstName = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(firstName);

            panel.Children.Add(new TextBlock { Text = "Priezvisko" });
            lastName = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(lastName);

            panel.Children.Add(new TextBlock { Text = "Email" });
            email = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(email);

            terms = new CheckBox
            {
                Content = "Súhlasím s podmienkami",
                Margin = new Thickness(0, 10, 0, 20)
            };
            panel.Children.Add(terms);

            Button login = new Button
            {
                Content = "Prihlásiť",
                Height = 40,
                Background = Brushes.Green,
                Foreground = Brushes.White
            };

            login.Click += Login_Click;

            Button register = new Button
            {
                Content = "Registrovať sa",
                Height = 40,
                Background = Brushes.Blue,
                Foreground = Brushes.White
            };
            register.Margin = new Thickness(0, 10, 0, 0);
            register.Click += Register_Click;

            panel.Children.Add(login);
            panel.Children.Add(register);

            grid.Children.Add(panel);
            Content = grid;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstName.Text) ||
                string.IsNullOrWhiteSpace(lastName.Text) ||
                string.IsNullOrWhiteSpace(email.Text))
            {
                MessageBox.Show("Vyplň všetky polia");
                return;
            }

            if (!email.Text.Contains("@") || !email.Text.Contains("."))
            {
                MessageBox.Show("Zadaj platný email!");
                return;
            }

            if (terms.IsChecked != true)
            {
                MessageBox.Show("Musíš súhlasiť s podmienkami");
                return;
            }

            User user = new User()
            {
                Meno = firstName.Text.Trim(),
                Priezvisko = lastName.Text.Trim(),
                Email = email.Text.Trim()
            };

            SetupWindow setup = new SetupWindow();
            setup.Show();
            this.Close();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            Close();
        }
    }
}
