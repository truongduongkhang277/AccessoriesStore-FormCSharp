using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class AddAddress : Form
    {
        public AddAddress()
        {
            InitializeComponent();
        }

        // khi tên quận (huyện) và tên tỉnh (thành phố) > 0 thì nút lưu có thể sử dụng
        private bool enableSave()
        {
            return (this.txtDistrict.Text.Length > 0 && this.txtCity.Text.Length > 0);
        }

        // khi có dữ liệu được nhập vào huyện 
        private void txtDistrict_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        // khi có dữ liệu được nhập vào tỉnh  
        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        // khi nhấn nút save
        private void btnSave_Click(object sender, EventArgs e)
        {
            // đóng gói dữ liệu
            BusAddress busAddress = new BusAddress();
            busAddress.addressInfo.District = this.txtDistrict.Text.Trim();
            busAddress.addressInfo.City = this.txtCity.Text.Trim();
            busAddress.addressInfo.Description = this.txtDescription.Text.Trim();

            // gọi hàm từ busAddress để lưu dữ liệu vào database
            int result = busAddress.addAddress();
            if (result == 1)
            {
                MessageBox.Show("Thêm mới quận (huyện) thành công !!");
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtDistrict.Clear();
            this.txtCity.Clear();
            this.txtDescription.Clear();
            this.txtDistrict.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
    }
}
