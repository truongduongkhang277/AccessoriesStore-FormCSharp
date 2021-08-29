using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class OrderEntity
    {
        // lưu trữ thông tin mã đơn hàng
        public string OrderId { get; set; }

        // lưu trữ thông tin mã loại tài khoản
        public string Account { get; set; }

        // lưu trữ thông tin mã loại khách hàng
        public string CustomerId { get; set; }

        // lưu trữ thông tin ngày đặt hàng
        public DateTime OrderDate { get; set; }

        // lưu trữ thông tin trạng thái đơn hàng
        public bool Status { get; set; }

        // lưu trữ thông tin ngày giao hàng
        public DateTime DepartureDate { get; set; }

        // lưu trữ thông tin đại chỉ giao hàng
        public string DeliveryAddress { get; set; }

        // lưu trữ thông tin khác
        public string Description { get; set; }

        // default contructor:: initalize all of member in class
        public OrderEntity()
        {
            this.Account = "";
            this.CustomerId = "";
            this.OrderDate = new DateTime(1900, 1, 1);
            this.DepartureDate = new DateTime(1900, 1, 1);
            this.DeliveryAddress = "";
            this.Description = "";
        }
    }
}
