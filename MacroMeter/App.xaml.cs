using System;
using System.Threading.Tasks;
using System.Windows;

namespace MacroMeter
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loading = new LoadingWindow();
            loading.Show();

            loading.LoadingBar.Value = 10;
            loading.StatusText.Text = "Pripravujem databázu...";

            await Task.Run(() => Database.Initialize());

            loading.LoadingBar.Value = 40;
            loading.StatusText.Text = "Načítavam používateľské dáta...";

            for (int i = 41; i <= 100; i++)
            {
                await Task.Delay(15);
                loading.LoadingBar.Value = i;
                loading.PercentText.Text = i + "%";

                if (i == 70) loading.StatusText.Text = "Overujem integritu...";
                if (i == 95) loading.StatusText.Text = "Hotovo!";
            }

            LoginWindow login = new LoginWindow();
            login.Show();

            loading.Close();
        }
    }
}