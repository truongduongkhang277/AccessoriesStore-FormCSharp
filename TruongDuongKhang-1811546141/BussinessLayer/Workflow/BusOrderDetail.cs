using System.Data;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;

namespace TruongDuongKhang_1811546141.BussinessLayer.Workflow
{
    class BusOrderDetail
    {
        public OrderDetailEntity orderDetailInfo { get; set; }

        // default contructor
        public BusOrderDetail()
        {
            this.orderDetailInfo = new OrderDetailEntity();
        }

        // contructor with paramecter
        public BusOrderDetail(OrderDetailEntity orderDetail)
        {
            this.orderDetailInfo = orderDetail;
        }

        private string selectSql()
        {
            return string.Format("Select OrderDetailId, OrderId, ProductId, Quantity, UnitPrice, DiscountPrice from TblAccount from TblOrderDetail");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblOrderDetail ( mssql server )
        public string insertSql()
        {
            return string.Format(
                "Insert Into TblOrderDetail(OrderId, ProductId, Quantity, UnitPrice, DiscountPrice) Values ('{0}', '{1}', {2}, {3}, {4});",
                this.orderDetailInfo.OrderId,
                this.orderDetailInfo.ProductId,
                this.orderDetailInfo.Quantity,
                this.orderDetailInfo.UnitPrice,
                this.orderDetailInfo.DiscountPrice);
        }

        // trả về câu SQL update dữ liệu vào bảng TblOrderDetail ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblOrderDetail set ProductId='{0}', Quantity={1}, UnitPrice={2}, DiscountPrice={3} Where OrderId='{4}' and ProductId='{0}' ;",
                this.orderDetailInfo.ProductId,
                this.orderDetailInfo.Quantity,
                this.orderDetailInfo.UnitPrice,
                this.orderDetailInfo.DiscountPrice,
                this.orderDetailInfo.OrderId);
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblOrderDetail ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblOrderDetail Where OrderId='{0}' and ProductId='{1}'",
                this.orderDetailInfo.OrderId, this.orderDetailInfo.ProductId);
        }

        // thêm thông tin địa chỉ vào database
        public int addOrderDetail()
        {
            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateOrderDetail()
        {
            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteOrderDetail()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // lấy thông tin địa chỉ từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblOrderDetail");
        }

    }
}
