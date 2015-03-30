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
        private System.Timers.Timer delay;
        private bool started = false;
        private int frameCounter = 0;

        private Model.TrackingSession trackedState;
        public Form1()
        {
            InitializeComponent();
            trackedState = new Model.TrackingSession();
            delay = new System.Timers.Timer();
            delay.Interval = 10;
            delay.Enabled = true;
            trackedState.onTrackingDetected += new Model.TrackingSession.TrackingHandler(UpdateGUI);
        }

        private void UpdateGUI(object sender, EventArgs e)
        {
            textBox1.Invoke((MethodInvoker)(() => textBox1.Text = "MOTION DETECTED, EVENT TRIGGERED BY: " + sender.ToString() + "AT: " + DateTime.Now)); 
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            trackedState.startTracking();
          //System.Threading.Thread.Sleep(5000);
            //TrackingState state = trackedState.getLatestState();
            //label1.Invoke((MethodInvoker)(() => label1.Text = "Detection State: " + state.getTime().ToString()));
                //System.Threading.Thread.Sleep(100)
        }

        private void button1_Click(object sender, EventArgs e)
        {
          started = true;
          if (started == true) { 
               //backgroundWorker1.RunWorkerAsync();
              backgroundWorker1 = new BackgroundWorker();
              backgroundWorker1.RunWorkerAsync();
                //delay.Start();
          }
         
         } 
 

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                TrackingState st = RESTConsume.getTrackingState(2147483647);
                textBox1.AppendText(Convert.ToString(st.getId()) + "\n");
                textBox1.AppendText(Convert.ToString(st.getUserId()) + "\n");
                textBox1.AppendText(Convert.ToString(st.getTime()) + "\n");
                textBox1.AppendText(Convert.ToString(st.getPlace()) + "\n");
                textBox1.AppendText(Convert.ToString(st.getTemp()) + "\n");
                textBox1.AppendText(Convert.ToString(st.getNoAlerts()) + "\n");

            }
            catch (Exception ex) {
                textBox1.AppendText("NETWORK ERROR!" + "\n");
                textBox1.AppendText(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                var lines = this.textBox1.Text.Split('\n').ToList();
                if (lines == null)
                {
                    textBox1.Text = "Please enter valid info!";
                }
                else
                {
                    int id = Convert.ToInt32(lines[0]);
                    int userid = Convert.ToInt32(lines[1]);
                    string time = Convert.ToString(lines[2]);
                    string place = Convert.ToString(lines[3]);
                    double temp = Convert.ToDouble(lines[4]);
                    int noAlerts = Convert.ToInt32(lines[5]);

                    TrackingState ts = new TrackingState(userid, time, place, temp, noAlerts);
                    if (RESTConsume.AddState(ts) == 1)
                    {
                        label1.Text = "State POSTED successfully";
                    }
                    else
                    {
                        label1.Text = "Error POSTING ";
                    }
                }  
            
            } catch (Exception ex) {
                textBox1.Text = ex.Message;
            }
        }
    }
}
