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
    public partial class AddFoodWindow : Window
    {
        public AddFoodWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CaloriesBox.Text, out int calories))
            {
                MessageBox.Show("Zlé kalórie");
                return;
            }

            if (!double.TryParse(ProteinBox.Text, out double protein))
            {
                MessageBox.Show("Zlé bielkoviny");
                return;
            }

            if (!double.TryParse(CarbsBox.Text, out double carbs))
            {
                MessageBox.Show("Zlé sacharidy");
                return;
            }

            if (!double.TryParse(FatBox.Text, out double fat))
            {
                MessageBox.Show("Zlé tuky");
                return;
            }

            FoodDatabase.Foods.Add(new Food
            {
                Name = FoodNameBox.Text,
                Calories = calories,
                Protein = protein,
                Carbs = carbs,
                Fat = fat
            });

            MessageBox.Show("Jedlo bolo uložené ✅");

            Close();
        }

    }
}
