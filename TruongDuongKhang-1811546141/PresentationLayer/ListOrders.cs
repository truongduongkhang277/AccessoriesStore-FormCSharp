using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListOrders : Form
    {
        private int status = 0;

        public ListOrders(int tt)
        {
            InitializeComponent();

            this.loadDataToCombobox();

            this.loadDataToDgv();

            this.formatDgv();

            this.status = tt;
        }

        // load data to combobox
        private void loadDataToCombobox()
        {
            this.cbbDistrict.DataSource = new BusAddress().getData().Tables[0];
            this.cbbDistrict.DisplayMember = "District";
            this.cbbDistrict.ValueMember = "AddressId";
            this.cbbDistrict.SelectedIndex = -1;
        }

        private void loadDataToDgv()
        {
            string customerName = txtCustomerName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            int addressId = 0;
            if (cbbDistrict.SelectedIndex > -1)
            {
                addressId = int.Parse(this.cbbDistrict.SelectedValue.ToString());
            }

            this.dgvOrder.DataSource = new BusOrder().getData(status, customerName, phone, addressId).Tables[0];
        }

        private void formatDgv()
        {
            this.dgvOrder.Columns[0].Width = 125;
            this.dgvOrder.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvOrder.Columns[0].HeaderText = "Mã hóa đơn";

            this.dgvOrder.Columns[1].Width = 160;
            this.dgvOrder.Columns[1].HeaderText = "Tên khách hàng";

            this.dgvOrder.Columns[2].HeaderText = "Duyệt bởi";

            this.dgvOrder.Columns[3].Width = 140;
            this.dgvOrder.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvOrder.Columns[3].DefaultCellStyle.Format = "hh:mm dd/MM/yyyy";
            this.dgvOrder.Columns[3].HeaderText = "Ngày tạo";

            this.dgvOrder.Columns[4].Width = 145;
            this.dgvOrder.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvOrder.Columns[4].DefaultCellStyle.Format = "hh:mm dd/MM/yyyy";
            this.dgvOrder.Columns[4].HeaderText = "Ngày giao hàng";

            this.dgvOrder.Columns[5].Width = 225;
            this.dgvOrder.Columns[5].HeaderText = "Địa chỉ giao hàng";

            this.dgvOrder.Columns[6].Width = 115;
            this.dgvOrder.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvOrder.Columns[6].DefaultCellStyle.Format = "#,##0";
            this.dgvOrder.Columns[6].HeaderText = "Số tiền ";

            this.dgvOrder.Columns.Add(addButtonColumn());
            this.dgvOrder.Columns[7].HeaderText = "Action";
        }

        private DataGridViewImageColumn addButtonColumn()
        {
            DataGridViewImageColumn btn = new DataGridViewImageColumn();
            btn.Name = "action";
            btn.Image = Properties.Resources.shipped_32px;
            switch (this.status)
            {
                case 0:
                    btn.Image = Properties.Resources.receipt_declined_32px;
                    break;
                case 1:
                    btn.Image = Properties.Resources.receipt_declined_32px;
                    break;
                case 2:
                    btn.Image = Properties.Resources.add_receipt_32px;
                    break;
                case 3:
                    btn.Image = Properties.Resources.order_history_32px;
                    break;
                case 4:
                    btn.Image = Properties.Resources.receipt_approved_32px;
                    break;
                default:
                    btn.Image = Properties.Resources.File_Delivery_32px;
                    break;
            }
            return btn;
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == this.dgvOrder.Columns["action"].Index)
            {
                if(this.status < 4)
                {
                    // lấy mã hóa đơn
                    string orderId = this.dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString();
                    // cập nhật lại trạng thái đơn hàng
                    int result = new BusOrder().updateStatusOrder(orderId, this.status + 1);
                    // cập nhật lại ds
                    if(result > 0)
                    {
                        this.loadDataToDgv();
                    }

                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.loadDataToDgv();
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
