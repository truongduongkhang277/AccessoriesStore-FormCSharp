using System;
using System.Data;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListAddress : Form
    {
        private DataSet ds;
        private DataViewManager dsView;

        public ListAddress()
        {
            InitializeComponent();
            dataBinding();
        }

        private void loadDataSet()
        {
            BusAddress busAddress = new BusAddress();
            ds = busAddress.getData();

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
            this.dgvAddress.DataSource = dsView;
            this.dgvAddress.DataMember = "TblAddress";

        }

        private void dataBinding()
        {
            loadDataSet();

            // đặt kích thước cho các cột trong bảng
            this.dgvAddress.Columns[0].HeaderText = "Mã";
            this.dgvAddress.Columns[0].Width = 70;
            this.dgvAddress.Columns[1].HeaderText = "Quận (Huyện)";
            this.dgvAddress.Columns[1].Width = 200;
            this.dgvAddress.Columns[2].HeaderText = "TP (Tỉnh)";
            this.dgvAddress.Columns[2].Width = 200;
            this.dgvAddress.Columns[3].HeaderText = "Chú thích";
            this.dgvAddress.Columns[3].Width = 270;

            // dòng được chọn
            this.dgvAddress.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dgvAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chuyển dữ liệu đến Textbox (txtDistrict, txtCity, txtDescription
            this.lblAddressId.Text = dgvAddress.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.txtDistrict.Text = dgvAddress.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.txtCity.Text = dgvAddress.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.txtDescription.Text = dgvAddress.Rows[e.RowIndex].Cells[3].Value.ToString();
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.lblAddressId.Text);
            // đóng gói dữ liệu
            BusAddress busAddress = new BusAddress();
            busAddress.addressInfo.District = this.txtDistrict.Text.Trim();
            busAddress.addressInfo.City = this.txtCity.Text.Trim();
            busAddress.addressInfo.Description = this.txtDescription.Text.Trim();
            busAddress.addressInfo.AddressId = id;

            // gọi hàm từ busAddress để cập nhật dữ liệu vào database
            int result = busAddress.updateAddress();
            if (result == 1)
            {
                loadDataSet();
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnClear.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.lblAddressId.Text);
            BusAddress busAddress = new BusAddress();
            busAddress.addressInfo.AddressId = id;
            if (busAddress.deleteAddress() > 0)
            {
                loadDataSet();
                // gọi nút thêm mới dữ liệu khởi động
                this.btnClear.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AddAddress address = new AddAddress();
            address.MdiParent = this.MdiParent;
            address.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintAddressList print = new PrintAddressList();
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
