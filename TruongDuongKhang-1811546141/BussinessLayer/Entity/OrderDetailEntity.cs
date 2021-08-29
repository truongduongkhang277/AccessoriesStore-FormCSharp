using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class OrderDetailEntity
    {
        // lưu trữ thông tin mã chi tiết đơn hàng
        public string OrderDetailId { get; set; }

        // lưu trữ thông tin mã đơn hàng
        public string OrderId { get; set; }

        // lưu trữ thông tin mã sản phẩm
        public string ProductId { get; set; }

        // lưu trữ thông tin số lượng sản phẩm
        public int Quantity { get; set; }

        // lưu trữ thông tin giá sản phẩm
        public float UnitPrice { get; set; }

        // lưu trữ thông tin giá giảm
        public float DiscountPrice { get; set; }

        // default contructor:: initalize all of member in class
        public OrderDetailEntity()
        {
            this.OrderId = "";
            this.ProductId = "";
        }
    }
}
