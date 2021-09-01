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
using TruongDuongKhang_1811546141.Lib;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class Order : Form
    {
        public static string CustomerId = "";
        public static string ProductId = "";

        public Order()
        {
            InitializeComponent();
        }

        // khi trạng thái giới tính nam thay đổi thì chuyển sang giới tính nữ được chọn
        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
            this.radFemale.Checked = !this.radMale.Checked;
        }

        // mở lại trạng thái của các nút khác 
        private void enableSave(bool isActive)
        {
            this.btnNew.Enabled = isActive;
            this.btnSelectCustomer.Enabled = !isActive;
            this.btnSelectProduct.Enabled = !isActive;
            this.btnPayment.Enabled = !isActive;
            this.btnPrint.Enabled = !isActive;
        }

        // khi nhấn vào ds sản phẩm order thì sẽ xóa dòng đó
        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == this.dgvOrder.Columns["action"].Index)
            {
                this.dgvOrder.Rows.Remove(this.dgvOrder.Rows[e.RowIndex]);
            }
        }

        // khi chọn nút chọn khách hàng
        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            new AddCustomerInfo().ShowDialog(this);
            /*AddCustomerInfo productItem = new AddCustomerInfo();
            customerInfo.customerInfo = this.MdiParent;
            customerInfo.Show();*/
            if(CustomerId.Length > 0)
            {
                this.lblCustomerId.Text = CustomerId;
                // lấy sản phẩm dựa vào phương thức getInfo trong BusCustomer
                CustomerEntity customerEntity = new BusCustomer().getInfoCustomer(CustomerId);
                this.txtCustomer.Text = this.txtCustomerName.Text = customerEntity.CustomerName;
                this.txtPhone.Text = customerEntity.Phone;
                this.txtEmail.Text = customerEntity.Email;
                this.txtDateOfBirth.Text = string.Format("{0:dd/MM/yyyy}", customerEntity.DateOfBirth);
                this.radMale.Checked = customerEntity.Sex;
                this.txtAddress.Text = this.txtDeliveryAddress.Text = customerEntity.Address;
            }
            CustomerId = "";
        }

        // khi nhấn nút thêm mới
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.lblCustomerId.Text = "";
            this.txtAccount.Text = SecurityObject.accInfo.FirstName + " " + SecurityObject.accInfo.LastName;
            this.txtOrderDate.Text = string.Format("{0:hh:mm dd/MM/yyyy}", DateTime.Now);
            this.txtDepartureDate.Text = string.Format("{0:hh:mm dd/MM/yyyy}", DateTime.Now.AddHours(24));
            this.radStatus.Checked = false;
            this.txtOrderId.Clear();
            this.txtCustomer.Clear();
            this.txtCustomerName.Clear();
            this.radMale.Checked = true;
            this.txtPhone.Clear();
            this.txtEmail.Clear();
            this.txtDateOfBirth.Clear();
            this.txtAddress.Clear();
            this.txtDeliveryAddress.Clear();
            this.dgvOrder.Rows.Clear();
            this.txtOrderId.Focus();
            this.enableSave(false);
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            new AddProductItem().ShowDialog(this);
            /*AddProductItem productItem = new AddProductItem();
            productItem.MdiParent = this.MdiParent;
            productItem.Show();
            */
            if (ProductId.Length > 0)
            {
                // lấy sản phẩm dựa vào phương thức getInfo trong BusProduct
                ProductEntity productEntity = new BusProduct().getInfo(ProductId);
                if (!this.isExistProduct(ProductId))
                {
                    this.addNewRow(productEntity);
                }
            }
        }

        // hàm trả về true nếu như đã có sản phẩm trong danh sách mua hàng -> cập nhật lại số lượng và tổng tiền trong ds
        private bool isExistProduct(string ProductId)
        {
            bool result = false;

            foreach(DataGridViewRow i in this.dgvOrder.Rows)
            {
                if (i.Cells["pId"].Value.ToString().Equals(ProductId))
                {
                    // tăng số lượng sản phẩm
                    int quantity = int.Parse(i.Cells["qty"].Value.ToString());
                    i.Cells["qty"].Value = ++quantity;

                    // tính lại tổng tiền phải trả
                    int price = int.Parse(i.Cells["unitPrice"].Value.ToString());
                    int discount = int.Parse(i.Cells["discount"].Value.ToString());
                    int total = (quantity * price) - (discount * quantity);
                    i.Cells["total"].Value = total;

                    // trả về true nếu tìm thấy sản phẩm
                    result = true;
                    break;
                     
                }
            }

            return result;
        }

        private void calculateOrder()
        {
            long amount = 0, reduced = 0;
            foreach (DataGridViewRow i in this.dgvOrder.Rows)
            {
                int quantity = int.Parse(i.Cells["qty"].Value.ToString());
                int price = int.Parse(i.Cells["unitPrice"].Value.ToString());
                int discount = int.Parse(i.Cells["discount"].Value.ToString());

                amount += (quantity * price);
                reduced += (discount * quantity);
            }
            this.txtAmount.Text = string.Format("{0:#,##0 }", amount);
            this.txtDiscount.Text = string.Format("{0:#,##0 }", reduced);
            this.txtTotal.Text = string.Format("{0:#,##0 }", amount - reduced);
        }

        // khi có cột mới được thêm
        private void dgvOrder_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.calculateOrder();
        }

        // khi có cột xóa
        private void dgvOrder_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.calculateOrder();
        }

        // khi giá trị dòng bị thay đổi
        private void dgvOrder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.calculateOrder();
        }

        // thêm mới dòng dữ liệu
        private void addNewRow(ProductEntity entity)
        {
            DataGridViewRow rows = new DataGridViewRow();
            // tạo ô thứ 1
            DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
            c1.Value = entity.ProductId;
            rows.Cells.Add(c1);
            // tạo ô thứ 2
            DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
            c2.Value = entity.ProductName;
            rows.Cells.Add(c2);
            // tạo ô thứ 3
            DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
            c3.Value = entity.Manufactur;
            rows.Cells.Add(c3);
            // tạo ô thứ 4
            DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
            c4.Value = 1;
            rows.Cells.Add(c4);
            // tạo ô thứ 5
            DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
            c5.Value = entity.UnitPrice;
            rows.Cells.Add(c5);
            // tạo ô thứ 6
            DataGridViewTextBoxCell c6 = new DataGridViewTextBoxCell();
            c6.Value = (entity.UnitPrice * entity.Discount) / 100;
            rows.Cells.Add(c6);
            // tạo ô thứ 7
            DataGridViewTextBoxCell c7 = new DataGridViewTextBoxCell();
            c7.Value = entity.UnitPrice - (entity.UnitPrice * entity.Discount) / 100;
            rows.Cells.Add(c7);

            // thêm dòng dữ liệu được chọn vào dgvOrder
            this.dgvOrder.Rows.Add(rows);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            // đóng gói dữ liệu 
            OrderEntity orderEntity = new OrderEntity();
            orderEntity.OrderId = this.txtOrderId.Text.Trim();
            orderEntity.CustomerId = this.lblCustomerId.Text.Trim();
            orderEntity.Account = SecurityObject.accInfo.Username;
            orderEntity.OrderDate = DateTime.Now;
            orderEntity.DepartureDate = DateTime.Now.AddHours(24);
            orderEntity.Status = 4;
            orderEntity.DeliveryAddress = this.txtDeliveryAddress.Text.Trim();

            // chuyển dgvOrder đến List<OrderDetail>
            List<OrderDetailEntity> list = new List<OrderDetailEntity>();
            foreach (DataGridViewRow row in this.dgvOrder.Rows)
            {
                OrderDetailEntity orderDetail = new OrderDetailEntity();

                orderDetail.OrderId = orderEntity.OrderId;
                orderDetail.ProductId = row.Cells["pId"].Value.ToString();
                orderDetail.Quantity = int.Parse(row.Cells["qty"].Value.ToString());
                orderDetail.UnitPrice = int.Parse(row.Cells["unitPrice"].Value.ToString());
                orderDetail.DiscountPrice = int.Parse(row.Cells["discount"].Value.ToString());

                list.Add(orderDetail);
            }

            // khởi chạy transaction
            bool result = new BusOrder().createCompleteOrder(orderEntity, list);

            // thông báo
            if (result)
            {
                MessageBox.Show("Tạo mới chi tiết đơn hàng thành công !");
                this.btnNew.PerformClick();
            }
            else
            {
                MessageBox.Show("Xảy ra lỗi, không thể tạo chi tiết đơn hàng !!!");
            }
            
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
