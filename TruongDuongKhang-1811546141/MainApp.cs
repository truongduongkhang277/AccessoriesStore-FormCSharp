using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.PresentationLayer;

namespace TruongDuongKhang_1811546141
{
    public partial class MainApp : RibbonForm
    {
        public MainApp()
        {
            InitializeComponent();
        }

        private void MainApp_Load(object sender, EventArgs e)
        {

        }

        private void ribBtnAddAccount_Click(object sender, EventArgs e)
        {
            Account account = new Account();
            account.MdiParent = this;
            account.Show();
        }

        private void ribBtnGroupList_Click(object sender, EventArgs e)
        {
            Role role = new Role();
            role.MdiParent = this;
            role.Show();
        }

        private void ribBtnAddress_Click(object sender, EventArgs e)
        {
            Address address = new Address();
            address.MdiParent = this;
            address.Show();
        }
        
    }
}
