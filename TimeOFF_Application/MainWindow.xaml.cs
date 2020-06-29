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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeOFF_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int timeOFF = 0;
        public MainWindow()
        {
            InitializeComponent();
            loadStatus();
        }

        void loadStatus()
        {
            btnStatus.Content = "Waitting...";
            btnTimmer.Content = "You can set time to show timmer left!!";
        }

        DispatcherTimer timer = new DispatcherTimer();
        public void DispatcherTimerSample()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timeOFF--;
            btnTimmer.Content = timeOFF.ToString();
        }

        void timeCalculator()
        {
            if (tbxHours.Text == "" && tbxMinutes.Text == "")
            {
                tbxHours.Text = "0";
                tbxMinutes.Text = "0";
            }
            else if (tbxHours.Text == "" && tbxSeconds.Text == "")
            {
                tbxHours.Text = "0";
                tbxSeconds.Text = "0";
            }
            else if (tbxMinutes.Text == "" && tbxSeconds.Text == "")
            {
                tbxMinutes.Text = "0";
                tbxSeconds.Text = "0";
            }
            else if (tbxHours.Text == "")
            {
                tbxHours.Text = "0";
            }
            else if (tbxMinutes.Text == "")
            {
                tbxMinutes.Text = "0";
            }
            else if (tbxSeconds.Text == "")
            {
                tbxSeconds.Text = "0";
            }

            timeOFF = Int32.Parse(tbxSeconds.Text) + Int32.Parse(tbxMinutes.Text) * 60 + Int32.Parse(tbxHours.Text) * 60 * 60;
        }

        void shutDown(string command)
        {
            System.Diagnostics.Process.Start("ShutDown", command);
        }

        private void btnShutDown_Click(object sender, RoutedEventArgs e)
        {
            timeCalculator();
            shutDown("-s -t " + timeOFF);
            btnStatus.Content = "Shutting Down...";
            DispatcherTimerSample();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            timeCalculator();
            shutDown("-r -t " + timeOFF);
            btnStatus.Content = "Restarting...";
            DispatcherTimerSample();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            shutDown("-a");
            MessageBox.Show("You canceled Shutting Down or Restarting");
            timer.Stop();
            btnTimmer.Content = "You're can set time to show timmer left!!";
        }
    }
}
