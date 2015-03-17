using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
                label1.Invoke((MethodInvoker)(() => label1.Text = DataClass.Slow().ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
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
                    string temp = Convert.ToString(lines[4]);
                    int noAlerts = Convert.ToInt32(lines[5]);

                    TrackingState ts = new TrackingState(id, userid, time, place, temp, noAlerts);
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
