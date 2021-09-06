using System;
using System.Drawing;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.PresentationLayer.Add;
using TruongDuongKhang_1811546141.PresentationLayer;
using TruongDuongKhang_1811546141.Lib;

namespace TruongDuongKhang_1811546141
{
    public partial class MainApp : RibbonForm
    {
        public MainApp()
        {
            InitializeComponent();
        }

        public void enableControl(int maLTK)
        {
            switch (maLTK)
            {
                case 1: //admin
                    ribTabDirectory.Enabled = true;
                    ribTabProduct.Enabled = true;
                    ribTabCustomer.Enabled = true;
                    ribTabOrder.Enabled = true;
                    ribTabStatistical.Enabled = true;
                    break;
                case 2: //Nhân viên bán hàng                
                    ribTabDirectory.Enabled = false;
                    ribTabProduct.Enabled = false;
                    ribTabCustomer.Enabled = true;
                    ribTabOrder.Enabled = true;
                    ribTabStatistical.Enabled = false;
                    break;
                case 3: //Nhân viên quản lý kho               
                    ribTabDirectory.Enabled = false;
                    ribTabProduct.Enabled = true;
                    ribTabCustomer.Enabled = false;
                    ribTabOrder.Enabled = false;
                    ribTabStatistical.Enabled = true;
                    break;
                default:
                    ribTabDirectory.Enabled = false;
                    ribTabProduct.Enabled = false;
                    ribTabCustomer.Enabled = false;
                    ribTabOrder.Enabled = false;
                    ribTabStatistical.Enabled = false;
                    break;
            }
        }

        private void MainApp_Load(object sender, EventArgs e)
        {
            enableControl(SecurityObject.accInfo.RoleId);
        }

        public void displayForm(Form form)
        {
            form.MdiParent = this;
            form.Location = new Point(0, 0);
            form.Show();
        }

        private void ribBtnAddAccount_Click(object sender, EventArgs e)
        {
            displayForm(new AddAccount());
        }

        private void ribBtnAccountActive_Click(object sender, EventArgs e)
        {
            displayForm(new ListAccount(true));
        }

        private void ribBtnAccountInactive_Click(object sender, EventArgs e)
        {
            displayForm(new ListAccount(false));
        }

        private void ribBtnRole_Click(object sender, EventArgs e)
        {
            displayForm(new ListRole());
        }
        private void ribBtnAddRole_Click(object sender, EventArgs e)
        {
            displayForm(new AddRole());
        }

        private void ribBtnAddress_Click(object sender, EventArgs e)
        {
            displayForm(new ListAddress());
        }


        private void ribBtnAddAddress_Click(object sender, EventArgs e)
        {
            displayForm(new AddAddress());
        }

        private void ribBtnCategory_Click(object sender, EventArgs e)
        {
            displayForm(new ListCategory());
        }

        private void ribBtnAddCategory_Click(object sender, EventArgs e)
        {
            displayForm(new AddCategory());
        }

        private void ribBtnAddProduct_Click(object sender, EventArgs e)
        {
            displayForm(new AddProduct());
        }

        private void ribBtnProductActive_Click(object sender, EventArgs e)
        {
            displayForm(new ListProduct(true));
        }

        private void ribBtnProductInactive_Click(object sender, EventArgs e)
        {
            displayForm(new ListProduct(false));
        }

        private void ribBtnCustomer_Click(object sender, EventArgs e)
        {
            displayForm(new ListCustomer());
        }

        private void ribBtnAddCustomer_Click(object sender, EventArgs e)
        {
            displayForm(new AddCustomer());
        }

        private void ribBtnDeclined_Click(object sender, EventArgs e)
        {
            displayForm(new ListOrders(-1));
        }

        private void ribBtnOrderList_Click(object sender, EventArgs e)
        {
            displayForm(new ListOrders(0));
        }

        private void ribBtnWaiting_Click(object sender, EventArgs e)
        {
            displayForm(new ListOrders(1));
        }

        private void ribBtnOrderApproved_Click(object sender, EventArgs e)
        {
            displayForm(new ListOrders(2));
        }

        private void ribBtnAreShipping_Click(object sender, EventArgs e)
        {
            displayForm(new ListOrders(3));
        }

        private void ribBtnSuccessfully_Click(object sender, EventArgs e)
        {
            displayForm(new ListOrders(4));
        }

        private void ribBtnCreateOrder_Click(object sender, EventArgs e)
        {
            displayForm(new Order());
        }

        private void ribBtnRevenue_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnStatistical_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnBestSellProduct_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnReputableManufacturer_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnSlowSellProduct_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnUpdate_Click(object sender, EventArgs e)
        {
            displayForm(new UpdateInfoAccount());
        }

        private void ribBtnChangePassword_Click(object sender, EventArgs e)
        {
            displayForm(new ChangePassword());
        }

        private void ribBtnLogout_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
        }

        private void ribBtnInfo_Click(object sender, EventArgs e)
        {
            displayForm(new InfoForm());
        }

    }
}
