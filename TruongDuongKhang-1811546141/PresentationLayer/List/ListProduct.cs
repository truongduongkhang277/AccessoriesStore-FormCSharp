using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.Lib;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListProduct : Form
    {
        private DataSet ds;
        private DataViewManager dsView;
        private bool isActive;
        private ProductEntity productEntity;
        private BusProduct busProduct;

        public ListProduct(bool isActive)
        {
            InitializeComponent();
            // gán giá trị cho isActive bằng giá trị được truyền vào khi gọi hàm tại main
            this.isActive = isActive;
            // khởi chạy truyền dữ liệu cho combobox để lọc dữ liệu
            loadDataToFilterCombobox();
            // khởi chạy truyền dữ liệu cho datagridview
            dataBinding(this.isActive, 0);
            this.productEntity = new ProductEntity(new Bitmap(this.picImage.Width, this.picImage.Height));
            this.busProduct = new BusProduct(productEntity);
        }

        // truyền dữ liệu cho combobox
        private void loadDataToFilterCombobox()
        {
            // cbbCategory
            DataSet dsCategory = new BusCategory().getData();
            this.cbbCategory.DataSource = dsCategory.Tables[0];
            this.cbbCategory.DisplayMember = "CategoryName";
            this.cbbCategory.ValueMember = "CategoryId";
            this.cbbCategory.SelectedIndex = -1;

            // cbbFilterCate
            this.cbbFilterCate.DataSource = new BusCategory().getData().Tables[0];
            this.cbbFilterCate.DisplayMember = "CategoryName";
            this.cbbFilterCate.ValueMember = "CategoryId";
            this.cbbFilterCate.SelectedIndex = -1;

            this.radStatus.Checked = isActive;
        }

        private void loadDataSet(bool isActive, int cateId)
        {
            BusProduct busProduct = new BusProduct();
            ds = busProduct.getData(isActive, cateId);

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
            this.dgvProduct.DataSource = dsView;
            this.dgvProduct.DataMember = "TblProduct";
        }

        private void dataBinding(bool isActive, int cateId)
        {
            loadDataSet(isActive, cateId);

            // đặt kích thước cho các cột trong bảng
            this.dgvProduct.Columns[0].HeaderText = "Mã SP";
            this.dgvProduct.Columns[0].Width = 75;
            this.dgvProduct.Columns[1].HeaderText = "Tên SP";
            this.dgvProduct.Columns[1].Width = 200;
            this.dgvProduct.Columns[2].HeaderText = "NSX";
            this.dgvProduct.Columns[2].Width = 125;
            this.dgvProduct.Columns[3].HeaderText = "Số lượng";
            this.dgvProduct.Columns[3].Width = 150;
            this.dgvProduct.Columns[4].HeaderText = "Đơn giá";
            this.dgvProduct.Columns[4].Width = 150;
            this.dgvProduct.Columns[5].HeaderText = "Giảm giá";
            this.dgvProduct.Columns[5].Width = 150;
            this.dgvProduct.Columns[6].HeaderText = "Chú thích";
            this.dgvProduct.Columns[6].Width = 150;
        }

        // khi nhấn vào mỗi dòng trong bảng
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chuyển dữ liệu đến Textbox và combobox
            this.lblProductId.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();

            ProductEntity productEntity = new BusProduct().getInfo(this.lblProductId.Text);
            fillDataToForm(productEntity);

            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;

            this.cbbFilterCate.SelectedIndex = -1;
        }

        // truyền vào các textbox và combobox
        private void fillDataToForm(ProductEntity entity)
        {
            //thông tin sản phẩm
            this.txtProductName.Text        = entity.ProductName;
            this.cbbCategory.SelectedValue  = entity.CategoryId.ToString();
            this.txtEnteredDate.Text        = string.Format("{0:dd/MM/yyyy hh:mm:ss}", entity.EnteredDate);
            this.txtManufactur.Text         = entity.Manufactur;
            this.txtAccount.Text            = entity.Account;
            this.radStatus.Checked          = entity.Status;
            this.txtQuantity.Text           = entity.Quantity.ToString();
            this.txtUnitPrice.Text          = entity.UnitPrice.ToString();
            this.txtDiscount.Text           = entity.Discount.ToString();
            this.txtDescription.Text        = entity.Description;
            this.picImage.Image             = entity.Image;
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (this.txtQuantity.Text.Trim().Length > 0 && !ValidationByRegex.IsNumeric(this.txtQuantity.Text))
            {
                this.ErrorMessage.Show("Dữ liệu số lượng không đúng !!", this.txtQuantity, 0, -70, 5000);
            }
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            if (this.txtUnitPrice.Text.Trim().Length > 0 && !ValidationByRegex.IsNumeric(this.txtUnitPrice.Text))
            {
                this.ErrorMessage.Show("Dữ liệu giá sản phẩm không đúng !!", this.txtUnitPrice, 0, -70, 5000);
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (this.txtDiscount.Text.Trim().Length > 0 && !ValidationByRegex.IsNumeric(this.txtDiscount.Text))
            {
                this.ErrorMessage.Show("Dữ liệu giảm giá không đúng !!", this.txtDiscount, 0, -70, 5000);
            }
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file(*.png)|*.png|All Files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.productEntity.Image = Image.FromFile(ofd.FileName);
                this.picImage.Image = this.productEntity.Image;
            }
        }

        private void cbbFilterCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnFilter.Enabled = true;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            int cateId = int.Parse(this.cbbFilterCate.SelectedValue.ToString());
            this.dataBinding(isActive, cateId);
            this.btnNew.PerformClick();
            this.btnUpdate.Enabled = this.btnDelete.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string productId = this.lblProductId.Text;
            // đóng gói dữ liệu
            this.busProduct = new BusProduct();
            busProduct.productInfo.ProductName = this.txtProductName.Text.Trim();
            busProduct.productInfo.CategoryId = int.Parse(this.cbbCategory.SelectedValue.ToString());
            busProduct.productInfo.Manufactur = this.txtManufactur.Text.Trim();
            busProduct.productInfo.Status = this.radStatus.Checked;
            busProduct.productInfo.Quantity = int.Parse(this.txtQuantity.Text.Trim());
            busProduct.productInfo.UnitPrice = int.Parse(this.txtUnitPrice.Text.Trim());
            busProduct.productInfo.Discount = int.Parse(this.txtDiscount.Text.Trim());
            busProduct.productInfo.Description = this.txtDescription.Text.Trim();
            busProduct.productInfo.ProductId = productId;

            // gọi hàm từ busRole để cập nhật dữ liệu vào database
            int result = busProduct.updateProduct();
            if (result == 1)
            {
                loadDataSet(isActive, 0);
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnNew.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string productId = this.lblProductId.Text;
            // đóng gói dữ liệu
            BusProduct busProduct = new BusProduct();
            busProduct.productInfo.ProductId = productId;
            if (busProduct.deleteProduct() > 0)
            {
                loadDataSet(isActive, 0);
                // gọi nút thêm mới dữ liệu khởi động
                this.btnNew.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            new AddProduct().Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintProductList print = new PrintProductList(isActive, 0);
            print.MdiParent = this.MdiParent;
            //print.Location = new Point(0, 0);
            print.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
