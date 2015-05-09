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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel;
using System.Diagnostics;

namespace ClientWPF
{
    public partial class LoginWindow
    {
        private BackgroundWorker signInWorker = new BackgroundWorker();
        private MainWindow window = new MainWindow();
        public LoginWindow()
        {
            window.Hide();
            InitializeComponent();
            signInWorker.DoWork += signInWorker_DoWork;
        }

        void signInWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => SignInAsync(EmailBox.Text, PasswordBox.Password)));      
        }

        private void Sign_In(object sender, RoutedEventArgs e)
        {
            signInWorker.RunWorkerAsync();       
        }

        public async void SignInAsync(string userName, string password)
        {
            if (!userName.Contains('@'))
            {   
                ErrorLabel.Dispatcher.Invoke(new Action(() => ErrorLabel.Content = "Please enter a valid email"));
                PasswordBox.Dispatcher.Invoke(new Action(() => PasswordBox.Clear()));
            }
            else
            {              
                bool result = await VerifyUserNamePasswordAsync(userName, password);
                if (result == true)
                {
                    ErrorLabel.Dispatcher.Invoke(new Action(() => ErrorLabel.Content = "Correct! Logging in"));
                    ErrorLabel.Dispatcher.Invoke(new Action(() => ErrorLabel.Foreground = Brushes.Green));
                    ErrorLabel.Foreground = Brushes.Green;
                    window.Dispatcher.Invoke(new Action(() => window.Show()));
                    this.Dispatcher.Invoke(new Action(() => this.Hide()));
                }
                else
                {
                    ErrorLabel.Dispatcher.Invoke(new Action(() =>  ErrorLabel.Content = "Incorrect User Name or Password"));
                    PasswordBox.Dispatcher.Invoke(new Action(() => PasswordBox.Clear()));
                }
            }
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
                window.setUser(found);
                return true;
            }
            //return await manager.FindAsync(userName, password) != null;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://192.168.192.3:44320/");
        }

    }
}
