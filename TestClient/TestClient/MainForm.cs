using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Timers.Timer;
using System.Timers;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace TestClient
{
    public partial class MainForm : Form
    {
        //private LoginForm login = new LoginForm();
        private bool started = false;
        private static Model.ApplicationUser user;
        public void setUser(Model.ApplicationUser user_in) {
            user = user_in;
            UserName.Text = "User: " + user.firstName + " " + user.lastName;
        }

        private Model.TrackingSession trackedState = new Model.TrackingSession();

        public MainForm()
        {
            InitializeComponent();
            CheckNetwork();
            trackedState = new Model.TrackingSession();
            
            trackedState.onTrackingDetected += new Model.TrackingSession.TrackingHandler(UpdateGUI);
            trackedState.onTempGUI += new Model.TrackingSession.TemperatureGUIHandler(UpdateTemp);
            trackedState.onTempDetected += new Model.TrackingSession.TemperatureHandler(UpdateGUITemp);
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        public void CheckNetwork()
        {
            try
            {
                byte[] toByte;
                using (WebClient client = new WebClient())
                {
                    LoggingBox.AppendText("Checking Connection" + Environment.NewLine);
                    toByte = client.DownloadData((new Uri("https://localhost:44301/TrackingService.svc/Network/Test/")));
                }
                Stream strm = new MemoryStream(toByte);
                DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(int));
                int objects = (int)obj.ReadObject(strm);
                if(objects == 1) {
                    LoggingBox.AppendText("Connection Successful" + Environment.NewLine);
                } else {
                    LoggingBox.AppendText("Connection Failed" + Environment.NewLine);
                }
                //session = (Models.TrackingModel.Session) objects;
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void UpdateGUI(object sender, EventArgs e)
        {
            try
            {
                //LoggingBox.Invoke((MethodInvoker)(() => LoggingBox.Text = "MOTION DETECTED, EVENT TRIGGERED BY: " + sender.ToString() + "AT: " + DateTime.Now));
                LoggingBox.Invoke((MethodInvoker)(() => LoggingBox.AppendText(Environment.NewLine + "MOTION DETECTED, EVENT TRIGGERED BY: " + sender.ToString() + " AT: " + DateTime.Now)));
            }
            catch (Exception)
            {               
               // throw;
            }
        }

        private void UpdateTemp(object myObject, Model.TemperatureEventArgs myArgs)
        {
            string temp = myArgs.temp + "°C";
            TempLabel.Invoke((MethodInvoker)(() => TempLabel.Text = temp));
        }
        private void UpdateGUITemp(object myObject, Model.TemperatureEventArgs myArgs)
        {
          LoggingBox.Invoke((MethodInvoker)(() => LoggingBox.AppendText(Environment.NewLine + "WARNING! HIGH TEMPERATURE!: " + myArgs.temp)));
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            trackedState.startTracking();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start" && started == false) {
                started = true;
                trackedState.SetUser(user);
                button1.Text = "Stop";
                LoggingBox.Text = "Starting Tracking Service" + Environment.NewLine;
                backgroundWorker1.RunWorkerAsync();
            }
            else if (button1.Text == "Stop" && started == true) {
                started = false;
                button1.Text = "Start";
                LoggingBox.AppendText("Tracking Stopped" + Environment.NewLine);
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void SignOutButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (started == true) {
                    backgroundWorker2.RunWorkerAsync();
                }

                LoginForm ls = new LoginForm();
                ls.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                LoggingBox.Text = ex.Message;
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            trackedState.Stop();
        }
    }
}
