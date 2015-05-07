using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace TestClient
{
    public partial class LoginForm : Form
    {
        private MainForm form = new MainForm();
        public LoginForm()
        {
            InitializeComponent();
        }

        public void SignIn(string userName, string password)
        {
            if (VerifyUserNamePassword(userName, password) == true)
            {
                ErrorLabel.Text = "Correct! Logging in";
                ErrorLabel.ForeColor = System.Drawing.Color.Green;
                //Application.Run(new Form1());
                form.Show();
                this.Hide();
                //Close();
            }
            else
            {
                ErrorLabel.Text = "Incorrect User Name or Password";
                PasswordTextBox.Clear();
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        public async void SignInAsync(string userName, string password)
        {
            if (!userName.Contains('@'))
            {
                //LoggingBox.Invoke((MethodInvoker)(() => LoggingBox.AppendText("MOTION DETECTED, EVENT TRIGGERED BY: " + sender.ToString() + " AT: " + DateTime.Now + Environment.NewLine)));
                ErrorLabel.Text = "Please enter a valid email";
                //ErrorLabel.Invoke((MethodInvoker)(() => ErrorLabel.Text = "Please Enter a valid email"), ErrorLabel.ForeColor = System.Drawing.Color.Red);
                PasswordTextBox.Clear();
                //PasswordTextBox.Invoke((MethodInvoker)(() => PasswordTextBox.Clear()));
                //ErrorLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                bool result = await VerifyUserNamePasswordAsync(userName, password);
                if (result == true)
                {
                    ErrorLabel.Text = "Correct! Logging in";
                    //ErrorLabel.Invoke((MethodInvoker)(() => ErrorLabel.Text = "Correct! Logging in"), ErrorLabel.ForeColor = System.Drawing.Color.Green);
                    ErrorLabel.ForeColor = System.Drawing.Color.Green;
                    //Application.Run(new Form1());
                    form.Show();
                    this.Hide();
                    //Close();
                }
                else
                {
                    ErrorLabel.Text = "Incorrect User Name or Password";
                    //ErrorLabel.Invoke((MethodInvoker)(() => ErrorLabel.Text = "Incorrect Username or Password"), ErrorLabel.ForeColor = System.Drawing.Color.Red);
                    PasswordTextBox.Clear();
                    //PasswordTextBox.Invoke((MethodInvoker)(() => PasswordTextBox.Clear())); 
                    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                }
            }


        }

        public bool VerifyUserNamePassword(string userName, string password)
        {
            //var usermanager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new Model.ApplicationDbContext()));
            //return usermanager.FindAsync(userName, password) != null;
            UserStore<Model.ApplicationUser> store = new UserStore<Model.ApplicationUser>(new Model.ApplicationDbContext());
            UserManager<Model.ApplicationUser> manager = new UserManager<Model.ApplicationUser>(store);
            var found = manager.FindAsync(userName, password);
            if (found.Result == null)
            {
                return false;
            }
            else
            {
                form.setUser(found.Result);
                return true;
            }
        }

        private void VerifyUser_Click(object sender, EventArgs e)
        {
            SignInAsync(EmailTextBox.Text, PasswordTextBox.Text);
            //LoginWorker.RunWorkerAsync();
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            // Reset label on click
            ErrorLabel.Text = "";
        }
        private void EmailTextBox_OnClick(object sender, EventArgs e)
        {
            // Reset label on click
            ErrorLabel.Text = "";
        }

        private void RegisterLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterLabel.LinkVisited = true;
            Process.Start("https://localhost:44320/");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public async Task<bool> VerifyUserNamePasswordAsync(string userName, string password)
        {
            UserStore<Model.ApplicationUser> store = new UserStore<Model.ApplicationUser>(new Model.ApplicationDbContext());
            UserManager<Model.ApplicationUser> manager = new UserManager<Model.ApplicationUser>(store);
            var found = await manager.FindAsync(userName, password);
            if (found == null)
            {
                return false;
            }
            else
            {
                form.setUser(found);
                return true;
            }
            //return await manager.FindAsync(userName, password) != null;
        }

        private void LoginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }
    }
}
