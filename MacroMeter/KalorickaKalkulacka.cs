using System;
using System.Collections.Generic;
using System.Text;

namespace MacroMeter
{
    public static class CaloriesCalculator
    {
        public static double Calculate(User u)
        {
            double bmr;

            if (u.Pohlavie == "Muž")
            {
                bmr = 10 * u.Vaha + 6.25 * u.Vyska - 5 * u.Vek + 5;
            }
            else
            {
                bmr = 10 * u.Vaha + 6.25 * u.Vyska - 5 * u.Vek - 161;
            }

            double multiplier = u.Aktivita switch
            {
                "Sedavý" => 1.2,
                "Aktívny" => 1.55,
                "Veľmi aktívny" => 1.725,
                _ => 1.2
            };

            double tdee = bmr * multiplier;

            return u.Ciel switch
            {
                "Schudnúť" => tdee - 500,
                "Pribrať" => tdee + 300,
                _ => tdee
            };
        }
    }
}
