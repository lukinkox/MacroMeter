using System;
using System.Collections.Generic;
using System.Text;

namespace MacroMeter
{
    public static class FoodDatabase
    {
        public static List<Food> Foods = new List<Food>
        {
            new Food
            {
                Name = "Jablko",
                Calories = 52,
                Protein = 0.3,
                Carbs = 14,
                Fat = 0.2
            },

            new Food
            {
                Name = "Ryža",
                Calories = 130,
                Protein = 2.7,
                Carbs = 28,
                Fat = 0.3
            },

            new Food
            {
                Name = "Kuracie mäso",
                Calories = 165,
                Protein = 31,
                Carbs = 0,
                Fat = 3.6
            }
        };
    }
}
