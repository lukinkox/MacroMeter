using System.Collections.Generic;

namespace MacroMeter
{
    public class User
    {
        public string Meno { get; set; }
        public string Priezvisko { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Kalorie { get; set; }
        public double CielovaVaha { get; set; }

        public int Vek { get; set; }
        public double Vaha { get; set; }
        public int Vyska { get; set; }
        public string Pohlavie { get; set; }
        public string Aktivita { get; set; }
        public string Ciel { get; set; }

        public string Dieta { get; set; }
        public double TargetCalories { get; set; }
        public double TargetProteins { get; set; }
        public double TargetCarbs { get; set; }
        public double TargetFats { get; set; }
        public List<FoodEntry> DennýPrijem { get; set; } = new List<FoodEntry>();
    }
}