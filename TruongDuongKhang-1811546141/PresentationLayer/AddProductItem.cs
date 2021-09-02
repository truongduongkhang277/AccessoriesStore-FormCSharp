using System;
using System.Data;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddProductItem : Form
    {
        public AddProductItem()
        {
            InitializeComponent();
            this.updateDataSource("");
            this.formatDgv();
        }

        private void updateDataSource(string filterValue)
        {
            DataSet dsProduct = new BusProduct().getData(filterValue);
            this.dgvProduct.DataSource = dsProduct.Tables[0];
        }

        private void formatDgv()
        {
            // đặt kích thước cho các cột trong bảng
            this.dgvProduct.Columns[0].HeaderText = "Mã SP";
            this.dgvProduct.Columns[0].Width = 90;
            this.dgvProduct.Columns[1].HeaderText = "Tên SP";
            this.dgvProduct.Columns[1].Width = 275;
            this.dgvProduct.Columns[2].HeaderText = "NSX";
            this.dgvProduct.Columns[2].Width = 175;
            this.dgvProduct.Columns[3].HeaderText = "Số lượng";
            this.dgvProduct.Columns[3].Width = 135;
            this.dgvProduct.Columns[4].HeaderText = "Đơn giá";
            this.dgvProduct.Columns[4].Width = 135;
            this.dgvProduct.Columns[5].HeaderText = "Giảm giá";
            this.dgvProduct.Columns[5].Width = 135;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            updateDataSource(this.txtProductName.Text.Trim());
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnSelect.Enabled = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Order.ProductId = this.dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
            this.Dispose();
        }

        // khi nhấn nút thêm mới
        private void btnNew_Click(object sender, EventArgs e)
        {
            AddProduct product = new AddProduct();
            product.MdiParent = this.MdiParent;
            product.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
