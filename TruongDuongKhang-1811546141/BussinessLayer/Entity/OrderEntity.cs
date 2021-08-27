using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class OrderEntity
    {
      public string OrderDetailId { get; set; }
      public string OrderId { get; set; }
      public string ProductId { get; set; }
      public string Quantity { get; set; }
      public string UnitPrice { get; set; }
      public string DiscountPrice { get; set; }
    }
}
