using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace THSchedule
{
    /// <summary>
    /// Interaction logic for ToastNotification.xaml
    /// </summary>
    public partial class ToastNotification : Window
    {
        private int secondsLeft = 20;

        public ToastNotification(string gameName)
        {
            InitializeComponent();
            tbGame.Text = $"{gameName} is starting in 10 minutes";

            var countdownTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (secondsLeft == 0) Close();

            secondsLeft--;
            lblCountdown.Content = $"Closing in {secondsLeft}s...";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var workingArea = SystemParameters.WorkArea;
            Left = workingArea.Right - (Width + 10);
            Top = workingArea.Bottom - (Height + 10);
        }
    }
}
