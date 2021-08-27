using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddRole : Form
    {
        public AddRole()
        {
            InitializeComponent();
        }

        // khi tên loại tài khoản được truyền dữ liệu
        private bool enableSave()
        {
            return (this.txtRoleName.Text.Length > 0);
        }

        // khi có dữ liệu được nhập vào tên loại tài khoản      
        private void txtRoleName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // đóng gói dữ liệu
            BusRole busRole = new BusRole();
            busRole.roleInfo.RoleName = this.txtRoleName.Text.Trim();
            busRole.roleInfo.Description = this.txtDescription.Text.Trim();

            // gọi hàm từ busAddress để lưu dữ liệu vào database
            int result = busRole.addRole();
            if (result == 1)
            {
                MessageBox.Show("Thêm mới loại tài khoản thành công !!");
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtRoleName.Clear();
            this.txtDescription.Clear();
            this.txtRoleName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
