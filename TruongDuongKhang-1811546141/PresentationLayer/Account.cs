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
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
            // lấy dữ liệu truyền vào các combobox
            // cbbAddress
            DataSet dsAddress = new BusAddress().getData();
            this.cbbAddress.DataSource = dsAddress.Tables[0];
            this.cbbAddress.DisplayMember = "District";
            this.cbbAddress.ValueMember = "AddressId";
            this.cbbAddress.SelectedIndex = -1;

            // cbbRole
            DataSet dsRole = new BusRole().getData();
            this.cbbRole.DataSource = dsRole.Tables[0];
            this.cbbRole.DisplayMember = "RoleName";
            this.cbbRole.ValueMember = "RoleId";
            this.cbbRole.SelectedIndex = -1;

        }

        // kiểm tra tính hợp lệ của dữ liệu ngày sinh được nhập vào 
        private void txtDateOfBirth_Leave(object sender, EventArgs e)
        {
            if (this.txtDateOfBirth.Text.Trim().Length >0 && !ValidationByRegex.checkDate(this.txtDateOfBirth.Text))
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

        // kiểm tra tính hợp lệ của dữ liệu tên đăng nhập được nhập vào
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (this.txtUsername.Text.Trim().Length > 0 && !ValidationByRegex.checkUsernameRegex(this.txtUsername.Text))
            {
                this.ErrorMessage.Show("Tài khoản phải có ít nhất 1 chữ, 1 số, nhiều hơn 8 kí tự và không quá 20!!", this.txtUsername, 0, -70, 5000);
            }
        }


        // Khi có dữ liệu truyền vào form
        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
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

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void cbbAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void cbbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        // lấy dữ liệu được nhập từ form
        private AccountEntity dataFromUI()
        {
            AccountEntity accountEntity = new AccountEntity();

            //thông tin tài khoản
            accountEntity.Username = this.txtUsername.Text.Trim();
            accountEntity.Password = new Encryption().SHA512_Hashing( this.txtPassword.Text.Trim());
            accountEntity.RoleId = int.Parse(this.cbbRole.SelectedValue.ToString());
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

        // khi có dữ liệu về tên tài khoản, mật khẩu, loại tài khoản, họ và tên, ngày sinh, số nhà, địa chỉ và điện thoại thì có thể lưu 
        private bool enableSave()
        {
            return (
                //thông tin tài khoản
                this.txtUsername.Text.Trim().Length > 0 &&
                this.txtPassword.Text.Trim().Length > 0 &&
                this.cbbRole.SelectedIndex >= 0 &&

                //thông tin người dùng
                this.txtFirstName.Text.Trim().Length > 0 &&
                this.txtLastName.Text.Trim().Length > 0 &&
                this.txtDateOfBirth.Text.Trim().Length > 0 &&

                // địa chỉ - phương thức liên lạc
                this.txtAddress.Text.Trim().Length > 0 &&
                this.cbbAddress.SelectedIndex >= 0 &&
                this.txtPhone.Text.Trim().Length > 0 &&
                this.txtEmail.Text.Trim().Length > 0
                );
        }

        // khi nhấn nút thêm
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.txtUsername.Clear();
            this.txtPassword.Clear();
            this.txtConfirmPassword.Clear();
            this.cbbRole.SelectedIndex = -1;

            this.txtFirstName.Clear();
            this.txtLastName.Clear();
            this.radMale.Checked = true;
            this.txtDateOfBirth.Clear();

            this.txtAddress.Clear();
            this.cbbAddress.SelectedIndex = -1;
            this.txtPhone.Clear();
            this.txtEmail.Clear();

            this.txtFirstName.Focus();
        }

        // khi nhấn nút lưu
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(this.txtPassword.Text.Trim().Equals(this.txtConfirmPassword.Text.Trim()))
            {

                BusAccount busAccount = new BusAccount();
                busAccount.accountInfo = dataFromUI();

                // gọi hàm addAccount từ busAccount để lưu dữ liệu vào database
                int result = busAccount.addAccount();
                if (result == 1)
                {
                    MessageBox.Show(string.Format("Thêm mới thành công tài khoản {0} cho thành viên {1} {2}", 
                        busAccount.accountInfo.Username, busAccount.accountInfo.FirstName , busAccount.accountInfo.LastName));
                    
                    // gọi nút thêm mới dữ liệu khởi động
                    this.btnNew.PerformClick();
                } else
                {
                    MessageBox.Show("Thêm mới thất bại");
                }

            } else
            {
                this.ErrorMessage.Show("Mật khẩu không khớp !! Vui lòng thử lại !!", this.txtPassword, 0, -70, 5000);
                this.txtPassword.Clear();
                this.txtConfirmPassword.Clear();
                this.txtPassword.Focus();
            }
        }

        // khi nhấn nút in
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        // khi nhấn nút thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
