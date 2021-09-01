using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.Lib;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
            loadDataToCombobox();
        }
        // lấy dữ liệu truyền vào các combobox
        private void loadDataToCombobox()
        {
            // cbbAddress
            DataSet dsAddress = new BusAddress().getData();
            this.cbbAddress.DataSource = dsAddress.Tables[0];
            this.cbbAddress.DisplayMember = "District";
            this.cbbAddress.ValueMember = "AddressId";
            this.cbbAddress.SelectedIndex = -1;
        }

        // kiểm tra tính hợp lệ của dữ liệu ngày sinh được nhập vào 
        private void txtDateOfBirth_Leave(object sender, EventArgs e)
        {
            if (this.txtDateOfBirth.Text.Trim().Length > 0 && !ValidationByRegex.checkDate(this.txtDateOfBirth.Text))
            {
                this.ErrorMessage.Show("Dữ liệu ngày sinh không đúng !!", this.txtDateOfBirth, 0, -70, 5000);
            }
        }

        // kiểm tra tính hợp lệ của dữ liệu số điện thoại được nhập vào
        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (this.txtPhone.Text.Replace(".", "").Trim().Length > 0 && !ValidationByRegex.checkPhone(this.txtPhone.Text.Replace(".", "").Trim()))
            {
                this.ErrorMessage.Show("Số điện thoại không hợp lệ !!", this.txtPhone, 0, -70, 5000);
            }
        }

        // kiểm tra tính hợp lệ của dữ liệu email được nhập vào
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (this.txtEmail.Text.Trim().Length > 0 && !ValidationByRegex.checkMail(this.txtEmail.Text))
            {
                this.ErrorMessage.Show("Địa chỉ email không hợp lệ !!", this.txtEmail, 0, -70, 5000);
            }
        }

        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void cbbAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
            this.radFemale.Checked = !this.radMale.Checked;
        }

        // lấy dữ liệu được nhập từ form
        private CustomerEntity getDataFromUI()
        {
            CustomerEntity customerEntity = new CustomerEntity();

            // thông tin khách hàng
            customerEntity.CustomerId = this.txtCustomerId.Text.Trim();
            customerEntity.CustomerName = this.txtCustomerName.Text.Trim();
            customerEntity.DateOfBirth = DateTime.Parse(this.txtDateOfBirth.Text);
            customerEntity.Sex = this.radMale.Checked;
            // địa chỉ - phương thức liên lạc
            customerEntity.Address = this.txtAddress.Text.Trim();
            customerEntity.AddressId = int.Parse(this.cbbAddress.SelectedValue.ToString());
            customerEntity.Phone = this.txtPhone.Text.Trim();
            customerEntity.Email = this.txtEmail.Text.Trim();

            return customerEntity;
        }

        // khi có dữ liệu về mã khách hàng, họ và tên, ngày sinh, số nhà, địa chỉ và điện thoại thì có thể lưu 
        private bool enableSave()
        {
            return (

                //thông tin khách hàng
                this.txtCustomerId.Text.Trim().Length > 0 &&
                this.txtCustomerName.Text.Trim().Length > 0 &&
                this.txtDateOfBirth.Text.Trim().Length > 0 &&

                // địa chỉ - phương thức liên lạc
                this.txtAddress.Text.Trim().Length > 0 &&
                this.cbbAddress.SelectedIndex >= 0 &&
                this.txtPhone.Text.Trim().Length > 0 &&
                this.txtEmail.Text.Trim().Length > 0
                );
        }

        // khi nhấn nút lưu
        private void btnSave_Click(object sender, EventArgs e)
        {
            BusCustomer busCustomer = new BusCustomer();
            busCustomer.customerInfo = getDataFromUI();

            // gọi hàm addCustomer từ busCustomer để lưu dữ liệu vào database
            int result = busCustomer.addCustomer();
            if (result == 1)
            {
                MessageBox.Show(string.Format("Thêm mới thành công khách hàng {0} !", busCustomer.customerInfo.CustomerName));

                // gọi nút thêm mới dữ liệu khởi động
                this.btnClear.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại");
            }         
        }

        // khi nhấn tạo mới
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtCustomerId.Clear();
            this.txtCustomerName.Clear();
            this.radMale.Checked = true;
            this.txtDateOfBirth.Clear();

            this.txtAddress.Clear();
            this.cbbAddress.SelectedIndex = -1;
            this.txtPhone.Clear();
            this.txtEmail.Clear();

            this.txtCustomerName.Focus();
        }

        // khi nhấn nút thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
    }
}
