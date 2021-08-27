using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class OrderDetailEntity
    {
      public string OrderId { get; set; }
      public string Account { get; set; }
      public string CustomerId { get; set; }
      public string OrderDate { get; set; }
      public string Status { get; set; }
      public string DepartureDate { get; set; }
      public string DeliveryAddress { get; set; }
      public string Description { get; set; }
    }
}
