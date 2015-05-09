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
using System.Collections.ObjectModel;

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
        private Model.ApplicationUser user;
        private ObservableCollection<Model.AlertListItem> alertList = new ObservableCollection<Model.AlertListItem>();
        public MainWindow()
        {
            InitializeComponent();
            AlertList.ItemsSource = alertList;
            TrackingStart = new BackgroundWorker();
            TrackingStop = new BackgroundWorker();
            TrackingStart.DoWork += TrackingStart_DoWork;
            TrackingStop.DoWork += TrackingStop_DoWork;
        }

        void TrackingStop_DoWork(object sender, DoWorkEventArgs e)
        {
            session.Stop();
            AlertList.Dispatcher.BeginInvoke(new Action(() => alertList.Clear()));
        }

        void TrackingStart_DoWork(object sender, DoWorkEventArgs e)
        {
            Image = new Image();
            session = new Model.TrackingSession();
            session.startTracking();
            Image.Dispatcher.Invoke(new Action(() => Image.Source = session.getImage()));

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
                session.OnInformationEvent += new Model.TrackingSession.InformationHandler(UpdateInfoLabel);
                //TrackingStart.RunWorkerAsync();
                session.startTracking();
                Image.Source = session.getImage();
            }
            else {
                Activate.Content = "Start";
                started = false;
                session.OnInformationEvent -= new Model.TrackingSession.InformationHandler(UpdateInfoLabel);
                session.onTrackingDetected -= new Model.TrackingSession.TrackingHandler(UpdatePercent);
                session.OnPercentageReceived -= new Model.TrackingSession.TrackingPercentHandler(UpdateGUIPercent);
                TrackingStop.RunWorkerAsync();
                //session.Stop();
            }
           
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void UpdatePercent(object myObject, EventArgs myArgs)
        {
            ListItem state = new ListItem();
            alertList.Add(new Model.AlertListItem()
            {
                Temperature = "30" + "°C",
                Type = "Motion",
                Time = DateTime.Now.ToString("h:mm")
            });
            //AlertList.Items.Add("State Added");
        }

        private void UpdateGUIPercent(object myObject, Model.EventArguments.PercentEventArgs myArgs)
        {
            PercentLabel.Dispatcher.Invoke(new Action(() => PercentLabel.Text = "Percent: " + myArgs.Percentage.ToString()));
            //PercentLabel.Text = myArgs.Percentage.ToString();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow ls = new LoginWindow();
            ls.Show();
            this.Hide();
        }
        public void setUser(Model.ApplicationUser user_in)
        {
            user = user_in;
            UserName.Text = "User: " + user.firstName + " " + user.lastName;
        }
        private void UpdateInfoLabel(object myObject, Model.EventArguments.InformationArgs myArgs)
        {
            InfoLabel.Content = myArgs.InfoMessage;
        }
    }
}
