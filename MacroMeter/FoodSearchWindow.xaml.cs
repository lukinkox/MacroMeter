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
using System.Globalization;
using System.Text;
using System.Linq;

namespace MacroMeter
{
    public partial class FoodSearchWindow : Window
    {
        public FoodSearchWindow()
        {
            InitializeComponent();
        }

        private string NormalizeText(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);

                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().ToLower();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string search = NormalizeText(FoodBox.Text);

            Food foundFood = FoodDatabase.Foods
                .FirstOrDefault(f => NormalizeText(f.Name) == search);

            if (foundFood != null)
            {
                ResultText.Text =
                    $"🍽 {foundFood.Name}\n\n" +
                    $"Kalórie: {foundFood.Calories} kcal\n" +
                    $"Bielkoviny: {foundFood.Protein} g\n" +
                    $"Sacharidy: {foundFood.Carbs} g\n" +
                    $"Tuky: {foundFood.Fat} g";
            }
            else
            {
                ResultText.Text = "Jedlo nebolo nájdené ❌";
            }


        }


    }
}
