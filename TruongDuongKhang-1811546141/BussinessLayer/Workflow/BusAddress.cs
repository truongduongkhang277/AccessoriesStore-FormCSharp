using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;

namespace TruongDuongKhang_1811546141.BussinessLayer.Workflow
{
    class BusAddress
    {
        public AddressEntity addressInfo { get; set; }

        // default contructor
        public BusAddress()
        {
            this.addressInfo = new AddressEntity();
        }

        private string selectSql()
        {
            return string.Format("Select * from TblAddress");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblAddress ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblAddress( District, City, Description) Values (N'{0}', N'{1}', N'{2}');", 
                this.addressInfo.District, 
                this.addressInfo.City, 
                this.addressInfo.Description);
        }

        // trả về câu SQL update dữ liệu vào bảng TblAddress ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblAddress set District=N'{0}', City=N'{1}', Description=N'{2}' " +
                "Where AddressId={3} ;", 
                this.addressInfo.District, 
                this.addressInfo.City, 
                this.addressInfo.Description, 
                this.addressInfo.AddressId); 
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblAddress ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblAddress where AddressId={0}", this.addressInfo.AddressId);
        }

        // thêm thông tin địa chỉ vào database
        public int addAddress()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateAddress()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteAddress()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // lấy thông tin địa chỉ từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblAddress");
        }
    }
}
