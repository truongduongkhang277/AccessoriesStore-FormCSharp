using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace TruongDuongKhang_1811546141
{
    public partial class Login : Form
    {
        public Login()
        {
            Thread t = new Thread(new ThreadStart(Splash));
            t.Start();
            InitializeComponent();
            // loading data
            string str = string.Empty;
            for(int i = 0; i < 100000; i++)
            {
                // init data
                str += i.ToString();
            }
            //Complete
            t.Abort();
        }

        void Splash()
        {
            SplashScreen.SplashForm frm = new SplashScreen.SplashForm();
            frm.AppName = "Trương Dương Khang";
            frm.Icon = Properties.Resources.smartphones;
            frm.ShowIcon = true;
            frm.ShowInTaskbar = true;
            Application.Run(frm);
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
