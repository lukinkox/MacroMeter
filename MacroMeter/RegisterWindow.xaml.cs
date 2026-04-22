using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MacroMeter
{
    public partial class RegisterWindow : Window
    {
        TextBox meno, priezvisko, email;
        PasswordBox pass, confirm;
        CheckBox terms;

        public RegisterWindow()
        {
            InitializeComponent();

            Title = "Register";
            Width = 420;
            Height = 550;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            StackPanel panel = new StackPanel { Margin = new Thickness(25) };

            panel.Children.Add(new TextBlock
            {
                Text = "Register",
                FontSize = 28,
                FontWeight = FontWeights.SemiBold
            });

            meno = Box(); panel.Children.Add(Label("Meno")); panel.Children.Add(meno);
            priezvisko = Box(); panel.Children.Add(Label("Priezvisko")); panel.Children.Add(priezvisko);
            email = Box(); panel.Children.Add(Label("Email")); panel.Children.Add(email);

            pass = new PasswordBox(); panel.Children.Add(Label("Heslo")); panel.Children.Add(pass);
            confirm = new PasswordBox(); panel.Children.Add(Label("Potvrď")); panel.Children.Add(confirm);

            terms = new CheckBox { Content = "Súhlasím" };
            panel.Children.Add(terms);

            Button btn = new Button
            {
                Content = "Register",
                Background = Brushes.Green,
                Foreground = Brushes.White,
                Height = 40
            };
            btn.Click += Register_Click;

            panel.Children.Add(btn);

            Content = panel;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (pass.Password != confirm.Password)
            {
                MessageBox.Show("Heslá nesedia");
                return;
            }

            new LoginWindow().Show();
            Close();
        }

        TextBlock Label(string t) => new TextBlock { Text = t, Margin = new Thickness(0, 10, 0, 5) };
        TextBox Box() => new TextBox { Height = 30 };
    }
}