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
using System.Windows.Threading;

namespace MacroMeter
{
    public partial class LoadingWindow : Window
    {
        private User _user;
        private DispatcherTimer timer;
        private int progress = 0;

        public LoadingWindow(User user)
        {
            InitializeComponent();
            _user = user;

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

            ProgressBar.Value = progress;
            PercentText.Text = progress + "%";

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
