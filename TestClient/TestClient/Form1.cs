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

namespace TestClient
{
    public partial class Form1 : Form
    {
        //private LoginForm login = new LoginForm();
        private System.Timers.Timer delay;
        private bool started = false;
        private static Model.ApplicationUser user;
        public void setUser(Model.ApplicationUser user_in) {
            user = user_in;
            UserName.Text = user.firstName + " " + user.lastName;
        }


        private Model.TrackingSession trackedState;
        public Form1()
        {
            InitializeComponent();
            trackedState = new Model.TrackingSession();
            delay = new System.Timers.Timer();
            delay.Interval = 10;
            delay.Enabled = true;
            trackedState.onTrackingDetected += new Model.TrackingSession.TrackingHandler(UpdateGUI);
            trackedState.onTempGUI += new Model.TrackingSession.TemperatureGUIHandler(UpdateTemp);
            trackedState.onTempDetected += new Model.TrackingSession.TemperatureHandler(UpdateGUITemp);
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void UpdateGUI(object sender, EventArgs e)
        {
            MotionLabel.Invoke((MethodInvoker)(() => MotionLabel.Text = "MOTION DETECTED, EVENT TRIGGERED BY: " + sender.ToString() + "AT: " + DateTime.Now));
        }

        private void UpdateTemp(object myObject, Model.TemperatureEventArgs myArgs)
        {
            string temp = myArgs.temp + "°C";
            MotionLabel.Invoke((MethodInvoker)(() => TempLabel.Text = temp));
        }
        private void UpdateGUITemp(object myObject, Model.TemperatureEventArgs myArgs)
        {
            MotionLabel.Invoke((MethodInvoker)(() => MotionLabel.Text = "WARNING! HIGH TEMPERATURE!: " + myArgs.temp));
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            trackedState.startTracking();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start") {
                button1.Text = "Stop";
                backgroundWorker1.RunWorkerAsync();
            }
            else if (button1.Text == "Stop") {
                button1.Text = "Start";
                trackedState.Stop();
            }

            //started = true;
           // if (started == true)
           // {
                //backgroundWorker1 = new BackgroundWorker();
                //backgroundWorker1.RunWorkerAsync();
                //trackedState.startSession();
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MotionLabel.Text = ex.Message;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MotionLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
