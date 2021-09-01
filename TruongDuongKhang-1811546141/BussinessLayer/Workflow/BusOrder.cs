using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;

namespace TruongDuongKhang_1811546141.BussinessLayer.Workflow
{
    class BusOrder
    {
        public OrderEntity orderInfo { get; set; }

        // default contructor
        public BusOrder()
        {
            this.orderInfo = new OrderEntity();
        }

        // contructor with paramecter
        public BusOrder(OrderEntity entity)
        {
            this.orderInfo = entity;
        }

        private string selectSql()
        {
            return string.Format("Select OrderId, CustomerId, Account, OrderDate, Status, DepartureDate, DeliveryAddress, Description from TblAccount from TblOrder");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblOrder ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblOrder(OrderId, CustomerId, Account, OrderDate, Status, DepartureDate, DeliveryAddress, Description) " +
                "Values ('{0}', '{1}', N'{2}', '{3}', {4}, '{5}', N'{6}', N'{7}');",
                this.orderInfo.OrderId,
                this.orderInfo.CustomerId,
                this.orderInfo.Account,
                string.Format("{0:dd/MM/yyyy hh:mm:ss}", this.orderInfo.OrderDate), 
                this.orderInfo.Status,
                string.Format("{0:dd/MM/yyyy hh:mm:ss}", this.orderInfo.DepartureDate), 
                this.orderInfo.DeliveryAddress,
                this.orderInfo.Description);
        }

        // trả về câu SQL update dữ liệu vào bảng TblOrder ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblOrder set CustomerId='{0}', Account={1}, OrderDate={2}, Status={3}, DepartureDate={4}, DeliveryAddress={5}, Description={6} " +
                "Where OrderId='{7}';",
                this.orderInfo.CustomerId,
                this.orderInfo.Account,
                string.Format("{0:dd/MM/yyyy hh:mm:ss}", this.orderInfo.OrderDate),
                this.orderInfo.Status,
                string.Format("{0:dd/MM/yyyy hh:mm:ss}", this.orderInfo.DepartureDate),
                this.orderInfo.DeliveryAddress,
                this.orderInfo.Description,
                this.orderInfo.OrderId);
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblOrder ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblOrder Where OrderId='{0}'", this.orderInfo.OrderId);
        }

        // thêm thông tin địa chỉ vào database
        public int addOrder()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateOrder()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteOrder()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // hoàn thành tạo hóa đơn
        // orderEntity: orderEntity object
        // orderDetails: list of product items
        public bool createCompleteOrder(OrderEntity orderEntity, List<OrderDetailEntity> orderDetails)
        {
            bool result = false;

            // tạo ds để lưu các câu Sql Statement
            List<string> statements = new List<string>();

            // chuyển kiểu dữ liệu của orderEntity object sang SQL command, sau đó thêm vào ds vừa tạo
            statements.Add(new BusOrder(orderEntity).insertSql());

            // lặp và sản phẩm của ds sản phẩm để tạo SQL command
            foreach(OrderDetailEntity orderDetail in orderDetails)
            {
                statements.Add(new BusOrderDetail(orderDetail).insertSql());
            }

            //khởi chạy transaction trong file DaoMsSqlSServer
            result = new DaoMsSqlServer().ExecuteTransaction(statements);

            return result;
        }

        // lấy thông tin địa chỉ từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblOrder");
        }
    }
}
