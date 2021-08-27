using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class RoleEntity
    {

        // lưu trữ thông tin mã loại tài khoản
        public int RoleId { get; set; }

        // lưu trữ thông tin tên loại tài khoản
        public string RoleName { get; set; }

        // những thông tin khác
        public string Description { get; set; }

        // default contructor:: initalize all of member in class
        public RoleEntity()
        {
            this.RoleName = "";
            this.Description = "";
        }

    }
}
