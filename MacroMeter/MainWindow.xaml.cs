using MacroMeter;
using System.Windows;

namespace MacroMeter
{
    public partial class MainWindow : Window
    {
        private User _user;

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(User user)
        {
            InitializeComponent(); 

            _user = user;

            MessageBox.Show($"Vitaj {_user.Meno}!");
        
            SetupWindow setup = new SetupWindow();
            setup.Show();

            this.Close();
        }
    }
}