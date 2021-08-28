using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class CustomerEntity
    {
        // lưu trữ thông tin mã khách hàng
        public string CustomerId { get; set; }

        // lưu trữ thông tin họ và tên khách hàng
        public string CustomerName { get; set; }

        // lưu trữ thông tin số điện thoại khách hàng
        public string Phone { get; set; }

        // lưu trữ thông tin địa chỉ email khách hàng
        public string Email { get; set; }

        // lưu trữ thông tin địa chỉ khách hàng
        public string Address { get; set; }

        // lưu trữ thông tin địa chỉ nhà khách hàng
        public int AddressId { get; set; }

        // lưu trữ thông tin ngày sinh khách hàng
        public DateTime DateOfBirth { get; set; }

        // lưu trữ thông tin giới tính khách hàng
        public bool Sex { get; set; }

        // lưu trữ thông tin khác về khách hàng
        public string Description { get; set; }

        // Default contructor
        public CustomerEntity()
        {
            this.CustomerName = "";
            this.DateOfBirth = new DateTime(1900, 1, 1);
            this.Phone = "";
            this.Email = "";
            this.Address = "";
            this.Description = "";
        }

    }
}
