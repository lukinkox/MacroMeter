using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace MacroMeter
{
    public partial class SetupWindow : Window
    {
        private User _user;
        private int vaha;
        private int cielovaVaha;

        public SetupWindow(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(AgeBox.Text, out int vek) ||
                    !int.TryParse(HeightBox.Text, out int vyska))
                {
                    MessageBox.Show("Zlé čísla");
                    return;
                }

                if (GenderBox.SelectedItem == null ||
                    ActivityBox.SelectedItem == null ||
                    GoalBox.SelectedItem == null)
                {
                    MessageBox.Show("Niečo si nevybral");
                    return;
                }

                _user.Vek = vek;
                _user.Vyska = vyska;

                _user.Pohlavie = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.Aktivita = (ActivityBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.Ciel = (GoalBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                _user.Vaha = double.TryParse(CurrentWeightBox.Text, out double v) ? v : 0;
                _user.CielovaVaha = double.TryParse(GoalWeightBox.Text, out double c) ? c : 0;

                if (Database.UserExists(_user.Email))
                {
                    MessageBox.Show("Tento email už existuje v databáze");
                    return;
                }

                Database.SaveUser(_user);

                MessageBox.Show("ide to");

                new MainWindow(_user).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
    }
}
