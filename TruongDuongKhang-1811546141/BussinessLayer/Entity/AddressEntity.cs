using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class AddressEntity
    {
        // lưu trữ thông tin mã địa chỉ
        public int AddressId { get; set; }

        // huyện (quận) quản lý Phường (xã)
        public string District { get; set; }

        // tỉnh (thành phố) quản lý huyện (quận)
        public string City { get; set; }

        // những thông tin khác
        public string Description { get; set; }

        // default contructor:: initalize all of member in class
        public AddressEntity()
        {
            this.District = "";
            this.City = "";
            this.Description = "";
        }
    }
}
