using MahApps.Metro.Controls;
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
using System.ComponentModel;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Model.TrackingSession session;
        private bool started = false;
        private BackgroundWorker TrackingStart;
        private BackgroundWorker TrackingStop;
        public MainWindow()
        {
            InitializeComponent();
            TrackingStart = new BackgroundWorker();
            TrackingStop = new BackgroundWorker();
            TrackingStart.DoWork += TrackingStart_DoWork;
            TrackingStop.DoWork += TrackingStop_DoWork;
        }

        void TrackingStop_DoWork(object sender, DoWorkEventArgs e)
        {
            session.Stop();
        }

        void TrackingStart_DoWork(object sender, DoWorkEventArgs e)
        {
            session.startTracking();
            Image.Dispatcher.Invoke(new Action(() => Image.Source = session.getImage()));
            //Image.Source = session.getImage();
        }

        private void Start()
        {
            if (started == false)
            {
                Activate.Content = "Stop";
                started = true;
                session = new Model.TrackingSession();
                session.onTrackingDetected += new Model.TrackingSession.TrackingHandler(UpdatePercent);
                session.OnPercentageReceived += new Model.TrackingSession.TrackingPercentHandler(UpdateGUIPercent);
                //TrackingStart.RunWorkerAsync();
                session.startTracking();
                Image.Source = session.getImage();
            }
            else {
                Activate.Content = "Start";
                started = false;
                session.onTrackingDetected -= new Model.TrackingSession.TrackingHandler(UpdatePercent);
                session.OnPercentageReceived -= new Model.TrackingSession.TrackingPercentHandler(UpdateGUIPercent);
                TrackingStop.RunWorkerAsync();
                //session.Stop();
            }
           
        }

        private void Stop() { 
        
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void UpdatePercent(object myObject, EventArgs myArgs)
        {
            //throw new NotImplementedException();
        }

        private void UpdateGUIPercent(object myObject, Model.EventArguments.PercentEventArgs myArgs)
        {
            PercentLabel.Dispatcher.Invoke(new Action(() => PercentLabel.Text = myArgs.Percentage.ToString()));
            //PercentLabel.Text = myArgs.Percentage.ToString();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow ls = new LoginWindow();
            ls.Show();
            this.Hide();
        }


    }
}
