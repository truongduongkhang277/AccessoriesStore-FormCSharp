using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;
using TruongDuongKhang_1811546141.Lib;

namespace TruongDuongKhang_1811546141.PresentationLayer.Add
{
    public partial class UpdateInfoAccount : Form
    {
        public UpdateInfoAccount()
        {
            InitializeComponent();
            loadData();
        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
            this.radFemale.Checked = !this.radMale.Checked;
        }

        private void loadData()
        {
            // cbbAddress
            DataSet dsAddress = new BusAddress().getData();
            this.cbbAddress.DataSource = dsAddress.Tables[0];
            this.cbbAddress.DisplayMember = "District";
            this.cbbAddress.ValueMember = "AddressId";
            this.cbbAddress.SelectedIndex = -1;

            // thông tin người dùng
            this.txtFirstName.Text = SecurityObject.accInfo.FirstName;
            this.txtLastName.Text = SecurityObject.accInfo.LastName;
            this.txtDateOfBirth.Text = string.Format("{0:dd/MM/yyyy}", SecurityObject.accInfo.DateOfBirth);
            this.radMale.Checked = SecurityObject.accInfo.Sex;

            // địa chỉ - phương thức liên lạc
            this.txtAddress.Text = SecurityObject.accInfo.Address;
            this.cbbAddress.SelectedValue = SecurityObject.accInfo.AddressId.ToString();
            this.txtPhone.Text = SecurityObject.accInfo.Phone;
            this.txtEmail.Text = SecurityObject.accInfo.Email;
        }

        // lấy dữ liệu được nhập từ form
        private AccountEntity getDataFromUI()
        {
            AccountEntity accountEntity = new AccountEntity();

            //thông tin tài khoản
            accountEntity.Username = SecurityObject.accInfo.Username;
            accountEntity.RoleId = SecurityObject.accInfo.RoleId;
            accountEntity.Status = SecurityObject.accInfo.Status;

            // thông tin người dùng
            accountEntity.FirstName = this.txtFirstName.Text.Trim();
            accountEntity.LastName = this.txtLastName.Text.Trim();
            accountEntity.DateOfBirth = DateTime.Parse(this.txtDateOfBirth.Text);
            accountEntity.Sex = this.radMale.Checked;
            // địa chỉ - phương thức liên lạc
            accountEntity.Address = this.txtAddress.Text.Trim();
            accountEntity.AddressId = int.Parse(this.cbbAddress.SelectedValue.ToString());
            accountEntity.Phone = this.txtPhone.Text.Trim();
            accountEntity.Email = this.txtEmail.Text.Trim();

            return accountEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BusAccount busAccount = new BusAccount();
            busAccount.accountInfo = getDataFromUI();

            // gọi hàm addAccount từ busAccount để lưu dữ liệu vào database
            int result = busAccount.updateAccount();
            if (result == 1)
            {
                MessageBox.Show(string.Format("Cập nhật thành công tài khoản {0} cho thành viên {1} {2}",
                    busAccount.accountInfo.Username, busAccount.accountInfo.FirstName, busAccount.accountInfo.LastName));
                loadData();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại");
            }            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
