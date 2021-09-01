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
using TruongDuongKhang_1811546141.BussinessLayer.Entity;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListCustomer : Form
    {
        private DataSet ds;
        private DataViewManager dsView;

        public ListCustomer()
        {
            InitializeComponent();
            // khởi chạy truyền dữ liệu cho combobox để lọc dữ liệu
            loadDataToFilterCombobox();
            // khởi chạy truyền dữ liệu cho datagridview
            dataBinding();
        }

        // truyền dữ liệu cho combobox
        private void loadDataToFilterCombobox()
        {
            // cbbAddress
            DataSet dsAddress = new BusAddress().getData();
            this.cbbAddress.DataSource = dsAddress.Tables[0];
            this.cbbAddress.DisplayMember = "District";
            this.cbbAddress.ValueMember = "AddressId";
            this.cbbAddress.SelectedIndex = -1;
        }

        private void loadDataSet()
        {
            BusCustomer busCustomer = new BusCustomer();
            ds = busCustomer.getData();

            // đặt hiển thị mặc định
            if (ds != null)
            {
                dsView = ds.DefaultViewManager;
            }
            else
            {
                dsView = null;
            }
            // chuyển dữ liệu đến dataGridView
            this.dgvCustomer.DataSource = dsView;
            this.dgvCustomer.DataMember = "TblCustomer";
        }

        private void dataBinding()
        {
            loadDataSet();

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

        // khi nhấn vào mỗi dòng trong bảng
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chuyển dữ liệu đến Textbox và combobox
            this.lblCustomerId.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();

            CustomerEntity customerEntity = new BusCustomer().getInfo(this.lblCustomerId.Text);
            fillDataToForm(customerEntity);

            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
        }

        // truyền vào các textbox và combobox
        private void fillDataToForm(CustomerEntity entity)
        {
            // thông tin khách hàng
            this.txtCustomerName.Text = entity.CustomerName;
            this.txtDateOfBirth.Text = string.Format("{0:dd/MM/yyyy}", entity.DateOfBirth);
            this.radMale.Checked = entity.Sex;

            // địa chỉ - phương thức liên lạc
            this.txtAddress.Text = entity.Address;
            this.cbbAddress.SelectedValue = entity.AddressId.ToString();
            this.txtPhone.Text = entity.Phone;
            this.txtEmail.Text = entity.Email;
        }

        // gọi đến form thêm mới người dùng
        private void btnClear_Click(object sender, EventArgs e)
        {
            new AddCustomer().Show();
            this.Dispose();
        }

        // chọn người dùng đển mua hàng
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(this.lblCustomerId.Text.Trim().Length > 0)
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerId = this.lblCustomerId.Text;
            // đóng gói dữ liệu
            BusCustomer busCustomer = new BusCustomer();
            // thông tin khách hàng
            busCustomer.customerInfo.CustomerName = this.txtCustomerName.Text.Trim();
            busCustomer.customerInfo.DateOfBirth = DateTime.Parse(this.txtDateOfBirth.Text);
            busCustomer.customerInfo.Sex = this.radMale.Checked;
            // địa chỉ - phương thức liên lạc
            busCustomer.customerInfo.Address = this.txtAddress.Text.Trim();
            busCustomer.customerInfo.AddressId = int.Parse(this.cbbAddress.SelectedValue.ToString());
            busCustomer.customerInfo.Phone = this.txtPhone.Text.Trim();
            busCustomer.customerInfo.Email = this.txtEmail.Text.Trim();
            busCustomer.customerInfo.CustomerId = customerId;

            // gọi hàm update từ busCustomer
            int result = busCustomer.updateCustomer();
            if (result == 1)
            {
                loadDataSet();
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string customerId = this.lblCustomerId.Text;
            // đóng gói dữ liệu
            BusCustomer busCustomer = new BusCustomer();
            busCustomer.customerInfo.CustomerId = customerId;
            if (busCustomer.deleteCustomer() > 0)
            {
                loadDataSet();
                // gọi nút thêm mới dữ liệu khởi động
                this.btnClear.PerformClick();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
