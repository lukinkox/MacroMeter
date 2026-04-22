using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public partial class LoginWindow : Window
    {
        TextBox firstName, lastName, email;
        CheckBox terms;

        public LoginWindow()
        {
            InitializeComponent();

            Title = "Login";
            Width = 420;
            Height = 500;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            FontFamily = new FontFamily("Segoe UI");

            StackPanel panel = new StackPanel { Margin = new Thickness(25) };

            panel.Children.Add(new TextBlock
            {
                Text = "Login",
                FontSize = 28,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0, 0, 0, 20),
                HorizontalAlignment = HorizontalAlignment.Center
            });

            panel.Children.Add(Label("Meno"));
            firstName = Box(); panel.Children.Add(firstName);

            panel.Children.Add(Label("Priezvisko"));
            lastName = Box(); panel.Children.Add(lastName);

            panel.Children.Add(Label("Email"));
            email = Box(); panel.Children.Add(email);

            terms = new CheckBox
            {
                Content = "Súhlasím s podmienkami",
                Margin = new Thickness(0, 10, 0, 10)
            };
            panel.Children.Add(terms);

            Button login = Button("Login", Brushes.Green);
            login.Click += Login_Click;

            Button register = Button("Register", Brushes.DodgerBlue);
            register.Margin = new Thickness(0, 10, 0, 0);
            register.Click += Register_Click;

            panel.Children.Add(login);
            panel.Children.Add(register);

            Content = panel;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstName.Text) ||
                string.IsNullOrWhiteSpace(lastName.Text) ||
                string.IsNullOrWhiteSpace(email.Text))
            {
                MessageBox.Show("Vyplň všetko");
                return;
            }

            if (!email.Text.Contains("@"))
            {
                MessageBox.Show("Zlý email");
                return;
            }

            if (terms.IsChecked != true)
            {
                MessageBox.Show("Súhlas nutný");
                return;
            }

            User user = new User
            {
                Meno = firstName.Text,
                Priezvisko = lastName.Text,
                Email = email.Text
            };

            SetupWindow setup = new SetupWindow(user);
            setup.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            Close();
        }

        TextBlock Label(string t) => new TextBlock { Text = t, Margin = new Thickness(0, 10, 0, 5) };

        TextBox Box() => new TextBox { Height = 30 };

        Button Button(string text, Brush color) => new Button
        {
            Content = text,
            Height = 40,
            Background = color,
            Foreground = Brushes.White
        };
    }
}