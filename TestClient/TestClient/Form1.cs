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
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void UpdateGUI(object sender, EventArgs e)
        {
            MotionLabel.Invoke((MethodInvoker)(() => MotionLabel.Text = "MOTION DETECTED, EVENT TRIGGERED BY: " + sender.ToString() + "AT: " + DateTime.Now));
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //label1.Text = "Started";
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
                // Stop Session
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
