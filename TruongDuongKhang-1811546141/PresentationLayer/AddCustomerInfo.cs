using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddCustomerInfo : Form
    {
        public AddCustomerInfo()
        {
            InitializeComponent();
            this.updateDataSource("");
            this.formatDgv();
        }

        private void updateDataSource(string filterValue)
        {
            this.dgvCustomer.DataSource = new BusCustomer().getData(filterValue).Tables[0];
        }

        private void formatDgv()
        {
            // đặt kích thước cho các cột trong bảng
            this.dgvCustomer.Columns[0].HeaderText = "Mã khách hàng";
            this.dgvCustomer.Columns[0].Width = 175;
            this.dgvCustomer.Columns[1].HeaderText = "Họ và tên";
            this.dgvCustomer.Columns[1].Width = 225;
            this.dgvCustomer.Columns[2].HeaderText = "Ngày sinh";
            this.dgvCustomer.Columns[2].Width = 110;
            this.dgvCustomer.Columns[3].HeaderText = "Giới tính";
            this.dgvCustomer.Columns[3].Width = 100;
            this.dgvCustomer.Columns[4].HeaderText = "Số điện thoại";
            this.dgvCustomer.Columns[4].Width = 150;
            this.dgvCustomer.Columns[5].HeaderText = "Địa chỉ";
            this.dgvCustomer.Columns[5].Width = 295;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            updateDataSource(this.txtCustomerName.Text.Trim());
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnSelect.Enabled = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Order.CustomerId = this.dgvCustomer.SelectedRows[0].Cells[0].Value.ToString();
            this.Dispose();
        }

        // khi nhấn nút thêm mới
        private void btnNew_Click(object sender, EventArgs e)
        {
            AddCustomer customer = new AddCustomer();
            customer.MdiParent = this.MdiParent;
            customer.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
