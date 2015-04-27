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
        private Form1 form = new Form1();
        public LoginForm()
        {
            InitializeComponent();
        }

        public void SignIn(string userName, string password) {
            if (VerifyUserNamePassword(userName, password) == true)
            {
                ErrorLabel.Text = "Correct! Logging in";
                ErrorLabel.ForeColor = System.Drawing.Color.Green;
                //Application.Run(new Form1());
                form.Show();
                this.Hide();
                //Close();
            }
            else {
                ErrorLabel.Text = "Incorrect User Name or Password";
                PasswordTextBox.Clear();
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        public bool VerifyUserNamePassword(string userName, string password)
        {
            //var usermanager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new Model.ApplicationDbContext()));
            //return usermanager.FindAsync(userName, password) != null;
               UserStore<Model.ApplicationUser> store = new UserStore<Model.ApplicationUser>(new Model.ApplicationDbContext());
               UserManager<Model.ApplicationUser> manager = new UserManager<Model.ApplicationUser>(store);
               var found =  manager.FindAsync(userName, password);
               if (found.Result == null)
               {
                   return false;
               }
               else {
                   form.setUser(found.Result);
                   return true;
               }
        }

        private void VerifyUser_Click(object sender, EventArgs e)
        {
            SignIn(EmailTextBox.Text, PasswordTextBox.Text);
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterLabel.LinkVisited = true;
            Process.Start("https://localhost:44320/");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //public async Task<bool> VerifyUserNamePassword(string userName, string password)
        //{
        //    var usermanager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));
        //    return await usermanager.FindAsync(userName, password) != null;
        //}
    }
}
