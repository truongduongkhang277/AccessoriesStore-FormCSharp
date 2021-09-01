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
    public partial class ListAccount : Form
    {
        private DataSet ds;
        private DataViewManager dsView;
        private bool isActive;

        public ListAccount(bool isActive)
        {
            InitializeComponent();
            // gán giá trị cho isActive bằng giá trị được truyền vào khi gọi hàm tại main
            this.isActive = isActive;
            // khởi chạy truyền dữ liệu cho combobox để lọc dữ liệu
            loadDataToFilterCombobox();
            // khởi chạy truyền dữ liệu cho datagridview
            dataBinding(this.isActive, 0);
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

            // cbbRole
            DataSet dsRole = new BusRole().getData();
            this.cbbRole.DataSource = dsRole.Tables[0];
            this.cbbRole.DisplayMember = "RoleName";
            this.cbbRole.ValueMember = "RoleId";
            this.cbbRole.SelectedIndex = -1;

            // cbbFilterRole
            this.cbbFilterRole.DataSource = new BusRole().getData().Tables[0];
            this.cbbFilterRole.DisplayMember = "RoleName";
            this.cbbFilterRole.ValueMember = "RoleId";
            this.cbbFilterRole.SelectedIndex = -1;

            this.radStatus.Checked = isActive;
        }

        private void loadDataSet(bool isActive, int roleId)
        {
            BusAccount busAccount = new BusAccount();
            ds = busAccount.getData(isActive, roleId);

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
            this.dgvAccount.DataSource = dsView;
            this.dgvAccount.DataMember = "TblAccount";            
        }

        private void dataBinding(bool isActive, int roleId)
        {
            loadDataSet(isActive, roleId);

            // đặt kích thước cho các cột trong bảng
            this.dgvAccount.Columns[0].HeaderText = "Họ và tên";
            this.dgvAccount.Columns[0].Width = 225;
            this.dgvAccount.Columns[1].HeaderText = "Tên đăng nhập";
            this.dgvAccount.Columns[1].Width = 175;
            this.dgvAccount.Columns[2].HeaderText = "Ngày sinh";
            this.dgvAccount.Columns[2].Width = 110;
            this.dgvAccount.Columns[3].HeaderText = "Giới tính";
            this.dgvAccount.Columns[3].Width = 100;
            this.dgvAccount.Columns[4].HeaderText = "Số điện thoại";
            this.dgvAccount.Columns[4].Width = 150;
            this.dgvAccount.Columns[5].HeaderText = "Địa chỉ";
            this.dgvAccount.Columns[5].Width = 295;
            this.dgvAccount.Columns[6].HeaderText = "Trạng thái";
            this.dgvAccount.Columns[6].Width = 150;
        }

        // khi nhấn vào mỗi dòng trong bảng
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chuyển dữ liệu đến Textbox và combobox
            this.lblUsername.Text = dgvAccount.Rows[e.RowIndex].Cells[1].Value.ToString();

            AccountEntity accountEntity = new BusAccount().getInfo(this.lblUsername.Text);
            fillDataToForm(accountEntity);

            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
        }

        // truyền vào các textbox và combobox
        private void fillDataToForm(AccountEntity entity)
        {
            //thông tin tài khoản
            this.cbbRole.SelectedValue = entity.RoleId.ToString();
            this.radStatus.Checked = entity.Status;
            
            // thông tin người dùng
            this.txtFirstName.Text = entity.FirstName;
            this.txtLastName.Text = entity.LastName;
            this.txtDateOfBirth.Text = string.Format("{0:dd/MM/yyyy}", entity.DateOfBirth);
            this.radMale.Checked = entity.Sex;

            // địa chỉ - phương thức liên lạc
            this.txtAddress.Text = entity.Address;
            this.cbbAddress.SelectedValue = entity.AddressId.ToString();
            this.txtPhone.Text = entity.Phone;
            this.txtEmail.Text = entity.Email;
        }

        private void cbbFilterRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnFilter.Enabled = true;
        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
            this.radFemale.Checked = !this.radMale.Checked;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            int roleId = int.Parse(this.cbbFilterRole.SelectedValue.ToString());
            this.dataBinding(isActive, roleId);
            this.btnClear.PerformClick();
            this.btnUpdate.Enabled = this.btnDelete.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = this.lblUsername.Text;
            // đóng gói dữ liệu
            BusAccount busAccount = new BusAccount();
            busAccount.accountInfo.RoleId = int.Parse(this.cbbRole.SelectedValue.ToString());
            busAccount.accountInfo.Status = this.radStatus.Checked;
            // thông tin người dùng
            busAccount.accountInfo.FirstName = this.txtFirstName.Text.Trim();
            busAccount.accountInfo.LastName = this.txtLastName.Text.Trim();
            busAccount.accountInfo.DateOfBirth = DateTime.Parse(this.txtDateOfBirth.Text);
            busAccount.accountInfo.Sex = this.radMale.Checked;
            // địa chỉ - phương thức liên lạc
            busAccount.accountInfo.Address = this.txtAddress.Text.Trim();
            busAccount.accountInfo.AddressId = int.Parse(this.cbbAddress.SelectedValue.ToString());
            busAccount.accountInfo.Phone = this.txtPhone.Text.Trim();
            busAccount.accountInfo.Email = this.txtEmail.Text.Trim();
            busAccount.accountInfo.Username = username;

            // gọi hàm từ busRole để cập nhật dữ liệu vào database
            int result = busAccount.updateAccount();
            if (result == 1)
            {
                loadDataSet(isActive, 0);
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string username = this.lblUsername.Text;
            // đóng gói dữ liệu
            BusAccount busAccount = new BusAccount();
            busAccount.accountInfo.Username = username;
            if (busAccount.deleteAccount() > 0)
            {
                loadDataSet(isActive, 0);
                // gọi nút thêm mới dữ liệu khởi động
                this.btnClear.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lblUsername.Text = "";
            this.cbbRole.SelectedIndex = -1;

            this.txtFirstName.Clear();
            this.txtLastName.Clear();
            this.radMale.Checked = true;
            this.txtDateOfBirth.Clear();

            this.txtAddress.Clear();
            this.cbbAddress.SelectedIndex = -1;
            this.txtPhone.Clear();
            this.txtEmail.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
