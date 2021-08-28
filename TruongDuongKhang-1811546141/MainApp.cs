﻿using System;
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
            AddAccount account = new AddAccount();
            account.MdiParent = this;
            account.Show();
        }

        private void ribBtnAccountActive_Click(object sender, EventArgs e)
        {
            ListAccount listAccount = new ListAccount(true);
            listAccount.MdiParent = this;
            listAccount.Show();
        }

        private void ribBtnAccountInactive_Click(object sender, EventArgs e)
        {
            ListAccount listAccount = new ListAccount(false);
            listAccount.MdiParent = this;
            listAccount.Show();
        }

        private void ribBtnRole_Click(object sender, EventArgs e)
        {
            ListRole listRole = new ListRole();
            listRole.MdiParent = this;
            listRole.Show();
        }
        private void ribBtnAddRole_Click(object sender, EventArgs e)
        {
            AddRole addRole = new AddRole();
            addRole.MdiParent = this;
            addRole.Show();
        }

        private void ribBtnAddress_Click(object sender, EventArgs e)
        {
            ListAddress listAddress = new ListAddress();
            listAddress.MdiParent = this;
            listAddress.Show();
        }


        private void ribBtnAddAddress_Click(object sender, EventArgs e)
        {
            AddAddress addAddress = new AddAddress();
            addAddress.MdiParent = this;
            addAddress.Show();
        }

        private void ribBtnCategory_Click(object sender, EventArgs e)
        {
            ListCategory listCategory = new ListCategory();
            listCategory.MdiParent = this;
            listCategory.Show();
        }

        private void ribBtnAddCategory_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.MdiParent = this;
            addCategory.Show();
        }

        private void ribBtnAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.MdiParent = this;
            addProduct.Show();
        }

        private void ribBtnProductActive_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnProductInactive_Click(object sender, EventArgs e)
        {

        }

        private void ribBtnCustomer_Click(object sender, EventArgs e)
        {
            ListCustomer listCustomer = new ListCustomer();
            listCustomer.MdiParent = this;
            listCustomer.Show();
        }

        private void ribBtnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.MdiParent = this;
            addCustomer.Show();
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


    }
}
