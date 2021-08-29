using System;
using System.Data;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListCategory : Form
    {
        private DataSet ds;
        private DataViewManager dsView;

        public ListCategory()
        {
            InitializeComponent();
            dataBinding();
        }

        private void loadDataSet()
        {
            BusCategory busCategory = new BusCategory();
            ds = busCategory.getData();

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
            this.dgvCategory.DataSource = dsView;
            this.dgvCategory.DataMember = "TblCategory";

        }
        private void dataBinding()
        {
            loadDataSet();

            // đặt kích thước cho các cột trong bảng
            this.dgvCategory.Columns[0].HeaderText = "Mã";
            this.dgvCategory.Columns[0].Width = 100;
            this.dgvCategory.Columns[1].HeaderText = "Loại sản phẩm";
            this.dgvCategory.Columns[1].Width = 250;
            this.dgvCategory.Columns[2].HeaderText = "Chú thích";
            this.dgvCategory.Columns[2].Width = 400;

            // dòng được chọn
            this.dgvCategory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chuyển dữ liệu đến Textbox (txtDistrict, txtCity, txtDescription
            this.lblCategoryId.Text = dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.txtCategoryName.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.txtDescription.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.lblCategoryId.Text);
            // đóng gói dữ liệu
            BusCategory busCategory = new BusCategory();
            busCategory.categoryInfo.CategoryName = this.txtCategoryName.Text.Trim();
            busCategory.categoryInfo.Description = this.txtDescription.Text.Trim();
            busCategory.categoryInfo.CategoryId = id;

            // gọi hàm từ busRole để cập nhật dữ liệu vào database
            int result = busCategory.updateCategory();
            if (result == 1)
            {
                loadDataSet();
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.lblCategoryId.Text);
            BusCategory busCategory = new BusCategory();
            busCategory.categoryInfo.CategoryId = id;
            if (busCategory.deleteCategory() > 0)
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
