using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class CustomerEntity
    {

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Sex { get; set; }

        public string Description { get; set; }

        public CustomerEntity()
        {

        }

    }
}
