using System.Windows;

namespace MacroMeter
{
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            InitializeComponent();

            Title = "MacroMeter";

            MessageBox.Show(
                $"Vitaj {user.Meno}\n" +
                $"Cieľ: {user.Ciel}\n" +
                $"Vek: {user.Vek}"
            );
        }
    }
}