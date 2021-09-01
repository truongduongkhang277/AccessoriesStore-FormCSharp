using System;
using System.Data;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListRole : Form
    {
        private DataSet ds;
        private DataViewManager dsView;

        public ListRole()
        {
            InitializeComponent();
            dataBinding();
        }

        private void loadDataSet()
        {
            BusRole busRole = new BusRole();
            ds = busRole.getData();

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
            this.dgvRole.DataSource = dsView;
            this.dgvRole.DataMember = "TblRole";

        }

        private void dataBinding()
        {
            loadDataSet();

            // đặt kích thước cho các cột trong bảng
            this.dgvRole.Columns[0].HeaderText = "Mã";
            this.dgvRole.Columns[0].Width = 100;
            this.dgvRole.Columns[1].HeaderText = "Loại tài khoản";
            this.dgvRole.Columns[1].Width = 250;
            this.dgvRole.Columns[2].HeaderText = "Chú thích";
            this.dgvRole.Columns[2].Width = 300;

            // dòng được chọn
            this.dgvRole.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dgvRole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chuyển dữ liệu đến Textbox (txtDistrict, txtCity, txtDescription
            this.lblRoleId.Text = dgvRole.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.txtRoleName.Text = dgvRole.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.txtDescription.Text = dgvRole.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.lblRoleId.Text);
            // đóng gói dữ liệu
            BusRole busRole = new BusRole();
            busRole.roleInfo.RoleName = this.txtRoleName.Text.Trim();
            busRole.roleInfo.Description = this.txtDescription.Text.Trim();
            busRole.roleInfo.RoleId = id;

            // gọi hàm từ busRole để cập nhật dữ liệu vào database
            int result = busRole.updateRole();
            if (result == 1)
            {
                loadDataSet();
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.lblRoleId.Text);
            BusRole busRole = new BusRole();
            busRole.roleInfo.RoleId = id;
            if (busRole.deleteRole() > 0)
            {
                loadDataSet();
                // gọi nút thêm mới dữ liệu khởi động
                this.btnClear.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
