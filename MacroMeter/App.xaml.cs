using System.Windows;

using System.Windows;

namespace MacroMeter
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Database.Initialize();
        }
    }
}