using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    class ProductEntity
    {
        // lưu trữ thông tin mã sản phẩm
        public string ProductId { get; set; }

        // lưu trữ thông tin tên sản phẩm
        public string ProductName { get; set; }

        // lưu trữ thông tin mã loại sản phẩm
        public int CategoryId { get; set; }

        // lưu trữ thông tin ảnh sản phẩm
        public Image Image { get; set; }

        // lưu trữ thông tin nhà cung cấp
        public string Manufactur { get; set; }

        // lưu trữ thông tin ngày nhập hàng
        public DateTime EnteredDate { get; set; }

        // lưu trữ thông tin tài khoản phê duyệt
        public string Account { get; set; }

        // lưu trữ thông tin trạng thái sản phẩm
        public bool Status { get; set; }

        // lưu trữ thông tin số lượng sản phẩm
        public int Quantity { get; set; }

        // lưu trữ thông tin giá bán
        public int UnitPrice { get; set; }

        // lưu trữ thông tin giảm giá
        public int Discount { get; set; }

        // lưu trữ thông tin khác
        public string Description { get; set; }

        // default contructor:: initalize all of member in class
        public ProductEntity()
        {
            this.ProductId = "";
            this.ProductName = "";
            this.Image = new Bitmap(225, 300);
            this.Manufactur = "";
            this.EnteredDate = new DateTime(1900, 1, 1);
            this.Account = "";
            this.Description = "";
        }

        // contructor with one parameter
        public ProductEntity(Bitmap image)
        {
            this.ProductId = "";
            this.ProductName = "";
            this.Image = image;
            this.Manufactur = "";
            this.EnteredDate = new DateTime(1900, 1, 1);
            this.Account = "";
            this.Description = "";
        }
    }
}
