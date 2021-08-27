using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class Address : Form
    {
        private DataSet ds;
        private DataViewManager dsView;

        public Address()
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
            this.dgvAddress.Columns[3].Width = 170;

            // dòng được chọn
            this.dgvAddress.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // tạo nút delete vào dòng trong bảng
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Xóa địa chỉ";
            btnDelete.Text = "Xóa";
            btnDelete.UseColumnTextForButtonValue = true;

            //thêm nút xóa vào bảng
            this.dgvAddress.Columns.Add(btnDelete);
            this.dgvAddress.CellClick += dgvAddress_CellClick;
        }

        private void dgvAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // khi bấm vào btnDelete thì mới xóa, còn không là binding data
            if (e.ColumnIndex == this.dgvAddress.Columns["Xóa địa chỉ"].Index)
            {
                int id = int.Parse(this.dgvAddress.Rows[e.RowIndex].Cells[1].Value.ToString());
                BusAddress busAddress = new BusAddress();
                busAddress.addressInfo = dataFromUI();
                busAddress.addressInfo.AddressId = id;
                if (busAddress.deleteAddress() > 0)
                {
                    loadDataSet();
                    // gọi nút thêm mới dữ liệu khởi động
                    this.btnNew.PerformClick();
                }
            } else
            {
                // chuyển dữ liệu đến Textbox (txtDistrict, txtCity, txtDescription
                this.txtDistrict.Text = dgvAddress.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.txtCity.Text = dgvAddress.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.txtDescription.Text = dgvAddress.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        // khi tên quận (huyện) và tên tỉnh (thành phố) > 0 thì nút lưu có thể sử dụng
        private bool enableSave()
        {
            return (this.txtDistrict.Text.Length > 0 && this.txtCity.Text.Length > 0);
        }

        private AddressEntity dataFromUI()
        {
            AddressEntity addressEntity = new AddressEntity();
            addressEntity.District = this.txtDistrict.Text.Trim();
            addressEntity.City = this.txtCity.Text.Trim();
            addressEntity.Description = this.txtDescription.Text.Trim();
            return addressEntity;
        }

        // khi có dữ liệu được nhập vào ấp        
        private void txtHamlet_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
        }

        // khi có dữ liệu được nhập vào xã  
        private void txtWard_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = enableSave();
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

        // khi nhấn nút new
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.txtDistrict.Clear();
            this.txtCity.Clear();
            this.txtDescription.Clear();
            this.txtDistrict.Focus();
        }

        // khi nhấn nút save
        private void btnSave_Click(object sender, EventArgs e)
        {
            // đóng gói dữ liệu
            BusAddress busAddress = new BusAddress();
            busAddress.addressInfo = dataFromUI();

            // gọi hàm từ busAddress để lưu dữ liệu vào database
            int result = busAddress.addAddress();
            if(result == 1)
            {
                loadDataSet();
            }
            // gọi nút thêm mới dữ liệu khởi động
            this.btnNew.PerformClick();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
