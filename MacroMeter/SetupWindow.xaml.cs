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
            if (!int.TryParse(AgeBox.Text, out int vek) ||
                !int.TryParse(HeightBox.Text, out int vyska) ||
                !int.TryParse(CurrentWeightBox.Text, out int vaha) ||
                !int.TryParse(GoalWeightBox.Text, out int cielovaVaha))
            {
                MessageBox.Show("Zlé čísla");
                return;
            }

            _user.Vek = vek;
            _user.Vyska = vyska;
            _user.Vaha = vaha;
            _user.CielovaVaha = cielovaVaha;

            _user.Kalorie = CaloriesCalculator.Calculate(_user);
            _user.Pohlavie = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            _user.Aktivita = (ActivityBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            _user.Ciel = (GoalBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            LoadingWindow loading = new LoadingWindow(_user);
            loading.Show();

            Close();
        }
    }
}
