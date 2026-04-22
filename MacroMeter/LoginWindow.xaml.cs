using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MacroMeter
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
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

            TextBlock title = new TextBlock();
            title.Text = "MacroMeter";
            title.FontSize = 26;
            title.FontWeight = FontWeights.Bold;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.Margin = new Thickness(0, 0, 0, 20);

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

            terms = new CheckBox();
            terms.Content = "Súhlasím s podmienkami";
            terms.Margin = new Thickness(0, 10, 0, 20);
            panel.Children.Add(terms);

            Button login = CreateRoundedButton("Prihlásiť");
            login.Click += Login_Click;

            panel.Children.Add(login);

            grid.Children.Add(panel);
            Content = grid;
        }

        private Button CreateRoundedButton(string text)
        {
            Button button = new Button();
            button.Content = text;
            button.Height = 40;
            button.Background = Brushes.Green;
            button.Foreground = Brushes.White;
            button.BorderThickness = new Thickness(0);

            ControlTemplate template = new ControlTemplate(typeof(Button));

            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(15));
            border.SetValue(Border.BackgroundProperty,
                new TemplateBindingExtension(Button.BackgroundProperty));

            FrameworkElementFactory content = new FrameworkElementFactory(typeof(ContentPresenter));
            content.SetValue(ContentPresenter.HorizontalAlignmentProperty,
                HorizontalAlignment.Center);
            content.SetValue(ContentPresenter.VerticalAlignmentProperty,
                VerticalAlignment.Center);

            border.AppendChild(content);
            template.VisualTree = border;

            button.Template = template;

            return button;
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

            if (terms.IsChecked == false)
            {
                MessageBox.Show("Musíte súhlasiť s podmienkami");
                return;
            }

            User user = new User()
            {
                Meno = firstName.Text,
                Priezvisko = lastName.Text,
                Email = email.Text
            };

            MainWindow main = new MainWindow(user);
            main.Show();
            Close();
        }
    }
    }
