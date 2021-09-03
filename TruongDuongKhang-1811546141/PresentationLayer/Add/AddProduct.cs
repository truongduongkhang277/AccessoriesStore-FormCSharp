using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.Lib;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddProduct : Form
    {
        private ProductEntity productEntity;

        public AddProduct()
        {
            InitializeComponent();
            this.productEntity = new ProductEntity(new Bitmap(this.picImage.Width, this.picImage.Height));
            loadDataToCombobox();
        }

        // lấy dữ liệu truyền vào các combobox
        private void loadDataToCombobox()
        {
            // cbbCategory
            DataSet dsCategory = new BusCategory().getData();
            this.cbbCategory.DataSource = dsCategory.Tables[0];
            this.cbbCategory.DisplayMember = "CategoryName";
            this.cbbCategory.ValueMember = "CategoryId";
            this.cbbCategory.SelectedIndex = -1;

            DataSet dsAccount = new BusAccount().getData();
            this.cbbAccount.DataSource = dsAccount.Tables[0];
            this.cbbAccount.DisplayMember = "Username";
            this.cbbAccount.ValueMember   = "Username";
            this.cbbAccount.SelectedIndex = 1;

            this.txtEnteredDate.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);
        }

        private void txtProductId_TextChanged(object sender, EventArgs e)
        {
            //this.btnSave.Enabled = enableSave();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           //this.btnSave.Enabled = enableSave();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            //this.btnSave.Enabled = enableSave();
        }

        private void txtManufactur_TextChanged(object sender, EventArgs e)
        {
            //this.btnSave.Enabled = enableSave();
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

        // khi nhấn vào picture box 
        private void picImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file(*.png)|*.png|All Files(*.*)|*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                this.productEntity.Image = Image.FromFile(ofd.FileName);
                this.picImage.Image = this.productEntity.Image;
            }
        }

        // lấy dữ liệu được nhập từ form
        private ProductEntity getDataFromUI()
        {
            // thông tin sản phẩm
            productEntity.ProductId = this.txtProductId.Text.Trim();
            productEntity.ProductName = this.txtProductName.Text.Trim();
            productEntity.CategoryId = int.Parse(this.cbbCategory.SelectedValue.ToString());
            productEntity.EnteredDate = DateTime.ParseExact(this.txtEnteredDate.Text, "dd/MM/yyyy hh:mm:ss", null);        
            productEntity.Manufactur = this.txtManufactur.Text.Trim();
            productEntity.Quantity = int.Parse(this.txtQuantity.Text.Trim());
            productEntity.Account = this.cbbAccount.SelectedValue.ToString();
            productEntity.UnitPrice = int.Parse(this.txtUnitPrice.Text.Trim());
            productEntity.Discount = int.Parse(this.txtDiscount.Text.Trim());
            productEntity.Description = this.txtDescription.Text.Trim();

            return productEntity;
        }
        // khi có dữ liệu về mã sản phẩm, tên sản phẩm, nhà cung cấp, loại sản phẩm, số lượng và đơn giá thì có thể lưu 
        private bool enableSave()
        {
            return (

                //thông tin sản phẩm
                this.txtProductId.Text.Trim().Length > 0 &&
                this.txtProductName.Text.Trim().Length > 0 &&
                this.txtManufactur.Text.Trim().Length > 0 &&
                this.txtQuantity.Text.Trim().Length > 0 &&
                this.cbbCategory.SelectedIndex >= 0 &&
                this.txtUnitPrice.Text.Trim().Length > 0 
                );
        }

        // khi nhấn nút lưu
        private void btnSave_Click(object sender, EventArgs e)
        {
            BusProduct busProduct = new BusProduct();
            busProduct.productInfo = getDataFromUI();

            // gọi hàm addProduct từ busProduct để lưu dữ liệu vào database
            int result = busProduct.addProduct();
            if (result == 1)
            {
                MessageBox.Show(string.Format("Thêm mới thành công sản phẩm {0} !", busProduct.productInfo.ProductName));

                // gọi nút thêm mới dữ liệu khởi động
                this.btnClear.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtProductId.Clear();
            this.txtProductName.Clear();
            this.cbbCategory.SelectedIndex = -1;
            this.picImage.Image = Properties.Resources.noImage;
            this.txtManufactur.Clear();
            this.txtQuantity.Clear();
            this.txtEnteredDate.Text = DateTime.Now.ToString();
            this.txtUnitPrice.Clear();
            this.txtDiscount.Clear();
            this.txtDescription.Clear();
            this.txtProductId.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
