using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        // khi tên loại sản phẩm được truyền dữ liệu
        private bool enableSave()
        {
            return (this.txtCategoryName.Text.Length > 0);
        }

        // khi có dữ liệu được nhập vào tên loại sản phẩm 
        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // đóng gói dữ liệu
            BusCategory busCategory = new BusCategory();
            busCategory.categoryInfo.CategoryName = this.txtCategoryName.Text.Trim();
            busCategory.categoryInfo.Description = this.txtDescription.Text.Trim();

            // gọi hàm từ busAddress để lưu dữ liệu vào database
            int result = busCategory.addCategory();
            if (result == 1)
            {
                MessageBox.Show("Thêm mới loại sản phẩm thành công !!");
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtCategoryName.Clear();
            this.txtDescription.Clear();
            this.txtCategoryName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
