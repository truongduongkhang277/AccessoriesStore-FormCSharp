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
    class BusAccount
    {
        public AccountEntity accountInfo { get; set; }

        // default contructor
        public BusAccount()
        {
            this.accountInfo = new AccountEntity();
        }

        private string selectSql()
        {
            return string.Format("Select * from TblAccount");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblAccount ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblAccount( Username, Password, RoleId, FirstName, LastName, DateOfBirth, Sex, Phone, Email,Address, AddressId, Status, Description )" +
                                     " Values (N'{0}', N'{1}', {2}, N'{3}', N'{4}', '{5}', {6}, N'{7}', N'{8}', N'{9}', {10}, {11}, N'{12}');",
                this.accountInfo.Username,
                this.accountInfo.Password,
                this.accountInfo.RoleId,
                this.accountInfo.FirstName,
                this.accountInfo.LastName,
                string.Format("{0:yyyy/MM/dd}", this.accountInfo.DateOfBirth),
                (this.accountInfo.Sex ? 1 : 0),
                this.accountInfo.Phone,
                this.accountInfo.Email,
                this.accountInfo.Address,
                this.accountInfo.AddressId,
                (this.accountInfo.Status ? 1 : 0),
                this.accountInfo.Description); ;
        }

        // trả về câu SQL update dữ liệu vào bảng TblAccount ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblAccount set Password=N'{0}', RoleId={1}, FirstName=N'{2}', LastName=N'{3}', DateOfBirth='{4}', " +
            " Sex={5}, Phone=N'{6}', Email=N'{7}',Address=N'{8}', AddressId={9}, Status={10}, Description=N'{11}' " +
                "Where Username='{12}' ;",
                this.accountInfo.Password,
                this.accountInfo.RoleId,
                this.accountInfo.FirstName,
                this.accountInfo.LastName,
                string.Format("{0:yyyy/MM/dd}", this.accountInfo.DateOfBirth),
                (this.accountInfo.Sex ? 1 : 0),
                this.accountInfo.Phone,
                this.accountInfo.Email,
                this.accountInfo.Address,
                this.accountInfo.AddressId,
                (this.accountInfo.Status ? 1 : 0),
                this.accountInfo.Description,
                this.accountInfo.Username); ;
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblAccount ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblAccount where AccountId='{0}'", this.accountInfo.Username);
        }

        // thêm thông tin địa chỉ vào database
        public int addAccount()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateAccount()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteAccount()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // lấy thông tin địa chỉ từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblAccount");
        }
    }
}
