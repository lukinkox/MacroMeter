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
    public partial class SetupWindow : Window
    {
        private User _user;

        public SetupWindow(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AgeBox.Text, out int vek) ||
                !int.TryParse(HeightBox.Text, out int vyska))
            {
                MessageBox.Show("Zlé čísla");
                return;
            }

            _user.Vek = vek;
            _user.Vyska = vyska;

            _user.Pohlavie = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            _user.Aktivita = (ActivityBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            _user.Ciel = (GoalBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            LoginWindow login = new LoginWindow();
            login.Show();

            Close();
        }
    }
}
