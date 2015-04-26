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
                //Application.Run(new Form1());
                form.Show();
                //Close();
            }
            else {
                EmailTextBox.Text = "Incorrect Credentials";
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

        //public async Task<bool> VerifyUserNamePassword(string userName, string password)
        //{
        //    var usermanager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));
        //    return await usermanager.FindAsync(userName, password) != null;
        //}
    }
}
