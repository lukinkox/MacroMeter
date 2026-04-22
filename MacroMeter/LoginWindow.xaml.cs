using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MacroMeter
{
    public class LoadingWindow : Window
    {
        private User _user;
        private ProgressBar bar;
        private TextBlock percentText;
        private DispatcherTimer timer;
        private int progress = 0;

        public LoadingWindow(User user)
        {
            _user = user;

            Title = "Loading";
            Width = 350;
            Height = 220;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;

            FontFamily = new FontFamily("Segoe UI");

            StackPanel panel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(20)
            };

            panel.Children.Add(new TextBlock
            {
                Text = "Pripravujeme tvoju fitness appku 💪",
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 20),
                TextAlignment = TextAlignment.Center
            });

            bar = new ProgressBar
            {
                Width = 250,
                Height = 18,
                Minimum = 0,
                Maximum = 100
            };

            panel.Children.Add(bar);

            percentText = new TextBlock
            {
                Text = "0%",
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            panel.Children.Add(percentText);

            Content = panel;

            Loaded += LoadingWindow_Loaded;
        }

        private void LoadingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progress++;

            bar.Value = progress;
            percentText.Text = progress + "%";

            if (progress >= 100)
            {
                timer.Stop();

                MainWindow main = new MainWindow(_user);
                main.Show();
                Close();
            }
        }
    }
}