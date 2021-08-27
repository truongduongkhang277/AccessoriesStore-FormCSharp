using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class CategoryEntity
    {

        // lưu trữ thông tin mã loại sản phẩm
        public int CategoryId { get; set; }

        // lưu trữ thông tin tên loại sản phẩm
        public string CategoryName { get; set; }

        // những thông tin khác
        public string Description { get; set; }

        // default contructor:: initalize all of member in class
        public CategoryEntity()
        {
            this.CategoryName = "";
            this.Description = "";
        }
    }
}
