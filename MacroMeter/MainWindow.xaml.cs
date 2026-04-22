using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MacroMeter
{
        public partial class MainWindow : Window
        {
        public MainWindow()
        {
            InitializeComponent();
        }

        private User _user;

            public MainWindow(User user)
            {
                _user = user;

                Title = "MacroMeter";
                Width = 800;
                Height = 500;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;

                Grid grid = new Grid();

                TextBlock welcome = new TextBlock();
                welcome.Text = $"Vitaj {_user.Meno} {_user.Priezvisko}!";
                welcome.FontSize = 26;
                welcome.HorizontalAlignment = HorizontalAlignment.Center;
                welcome.VerticalAlignment = VerticalAlignment.Center;

                grid.Children.Add(welcome);

                Content = grid;
            }
        }
    }