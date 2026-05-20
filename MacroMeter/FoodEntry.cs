using System.Collections.Generic;

namespace MacroMeter
{
    public class FoodEntry
    {
        public string Nazov { get; set; }
        public double Kalorie { get; set; }
        public double Bielkoviny { get; set; }
        public double Sacharidy { get; set; }
        public double Tuky { get; set; }
        public double Gramaz { get; set; }
        public string CasDna { get; set; }
    }

    // Tvoja trieda User by mala mať túto vlastnosť:
    // public List<FoodEntry> DennýPrijem { get; set; } = new List<FoodEntry>();
}
