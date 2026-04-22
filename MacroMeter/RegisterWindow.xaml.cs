using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public partial class RegisterWindow : Window
    {
        TextBox Meno;
        TextBox Priezvisko;
        TextBox Email;
        PasswordBox password;
        PasswordBox confirmPassword;
        CheckBox terms;

        public RegisterWindow()
        {
            Title = "Registrácia";
            Width = 400;
            Height = 500;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Grid grid = new Grid();
            grid.Margin = new Thickness(20);

            StackPanel panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "Registrácia",
                FontSize = 26,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            });

            panel.Children.Add(new TextBlock { Text = "Meno" });
            Meno = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(Meno);

            panel.Children.Add(new TextBlock { Text = "Priezvisko" });
            Priezvisko = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(Priezvisko);

            panel.Children.Add(new TextBlock { Text = "Email" });
            Email = new TextBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(Email);

            panel.Children.Add(new TextBlock { Text = "Heslo" });
            password = new PasswordBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(password);

            panel.Children.Add(new TextBlock { Text = "Potvrďte heslo" });
            confirmPassword = new PasswordBox { Margin = new Thickness(0, 5, 0, 10) };
            panel.Children.Add(confirmPassword);

            terms = new CheckBox
            {
                Content = "Súhlasím s podmienkami",
                Margin = new Thickness(0, 10, 0, 20)
            };
            panel.Children.Add(terms);

            Button register = new Button
            {
                Content = "Registrovať",
                Height = 40,
                Background = Brushes.Green,
                Foreground = Brushes.White
            };

            register.Click += Register_Click;
            panel.Children.Add(register);

            grid.Children.Add(panel);
            Content = grid;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Meno.Text) ||
                string.IsNullOrWhiteSpace(Priezvisko.Text) ||
                string.IsNullOrWhiteSpace(Email.Text))
            {
                MessageBox.Show("Vyplň všetky polia");
                return;
            }

            if (password.Password != confirmPassword.Password)
            {
                MessageBox.Show("Heslá sa nezhodujú");
                return;
            }

            if (terms.IsChecked != true)
            {
                MessageBox.Show("Musíte súhlasiť s podmienkami");
                return;
            }

            MessageBox.Show("Registrácia úspešná");

            LoginWindow login = new LoginWindow();
            login.Show();

            this.Close();
        }
    }
}