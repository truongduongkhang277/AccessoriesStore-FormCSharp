using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.BussinessLayer.Entity
{
    public class AccountEntity
    {
        // lưu trữ thông tin tên đăng nhập
        public string Username  { get; set; }

        // lưu trữ thông tin mật khẩu
        public string  Password { get; set;}

        // lưu trữ thông tin mã loại tài khoản
        public int RoleId { get; set;}

        // lưu trữ thông tin tên đệm và tên
        public string FirstName { get; set;}

        // lưu trữ thông tin họ
        public string LastName { get; set;}

        // lưu trữ thông tin ngày sinh
        public DateTime DateOfBirth { get; set;}

        // lưu trữ thông tin giới tính
        public bool Sex { get; set;}

        // lưu trữ thông tin điện thoại
        public string Phone { get; set;}

        // lưu trữ thông tin địa chỉ email
        public string Email { get; set; }

        // lưu trữ thông tin địa chỉ nơi ở
        public string Address { get; set; }

        // lưu trữ thông tin mã địa chỉ (xã, phường - quận, huyện - tỉnh, thành phố )
        public int AddressId { get; set;}

        // lưu trữ thông tin trạng thái
        public bool Status { get; set;}

        // lưu trữ thông tin khác
        public string Description { get; set;}

        public AccountEntity()
        {
            this.Username = "";
            this.Password = "";
            this.FirstName = "";
            this.LastName = "";
            this.DateOfBirth = new DateTime(1900, 1, 1);
            this.Phone = "";
            this.Email = "";
            this.Address = "";
            this.Description = "";

        }
    }
}
