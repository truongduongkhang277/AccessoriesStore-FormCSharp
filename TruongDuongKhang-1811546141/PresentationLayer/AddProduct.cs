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
using System.IO;

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
            // cbbAddress
            DataSet dsCategory = new BusCategory().getData();
            this.cbbCategory.DataSource = dsCategory.Tables[0];
            this.cbbCategory.DisplayMember = "CategoryName";
            this.cbbCategory.ValueMember = "CategoryId";
            this.cbbCategory.SelectedIndex = -1;
        }

        private void txtProductId_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtManufactur_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
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
            if(new BusProduct(this.productEntity).addProduct() == 1)
            {
                MessageBox.Show("Thêm mới sản phẩm thành công !!");
                btnClear.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtProductId.Clear();
            this.txtProductName.Clear();
            this.cbbCategory.SelectedIndex = -1;
            this.picImage.Image = new Bitmap(this.picImage.Width, this.picImage.Height);
            this.txtManufactur.Clear();
            this.txtQuantity.Clear();
            this.txtEnteredDate.Clear();
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
