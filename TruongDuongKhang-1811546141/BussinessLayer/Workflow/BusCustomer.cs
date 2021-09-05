using System.Data;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;
using System.Data.SqlClient;

namespace TruongDuongKhang_1811546141.BussinessLayer.Workflow
{
    class BusCustomer
    {
        public CustomerEntity customerInfo { get; set; }

        // default contructor
        public BusCustomer()
        {
            this.customerInfo = new CustomerEntity();
        }

        // lấy dữ liệu về mã khách hàng, họ và tên, ngày sinh, giới tính, điện thoại, địa chỉ của khách hàng
        private string selectSql(string likename)
        {
            return string.Format("Select " +
                            "CustomerId, " +
                            "CustomerName, " +
                            "FORMAT(DateOfBirth, 'dd/MM/yyyy') as DateOfBirth, " +
                            "iif(Sex=1,'Nam', N'Nữ') as Sex, " +
                            "Phone, " +
                            "Address + ',' +ad.District + ',' + ad.City as Address " +
                            "from TblCustomer cus inner join TblAddress ad " +
                            "on (cus.AddressId = ad.AddressId) " + 
                            (likename.Trim().Length > 0 ? " And CustomerName like N'%" + likename.Trim() + "%'" : "") +
                            " Order by CustomerName");
        }

        // lấy dữ liệu về mã khách hàng, họ và tên, ngày sinh, giới tính, điện thoại, địa chỉ của khách hàng
        private string selectSql()
        {
            return string.Format("Select " +
                            "CustomerId, " +
                            "CustomerName, " +
                            "FORMAT(DateOfBirth, 'dd/MM/yyyy') as DateOfBirth, " +
                            "iif(Sex=1,'Nam', N'Nữ') as Sex, " +
                            "Phone, " +
                            "Address + ',' +ad.District + ',' + ad.City as Address " +
                            "from TblCustomer cus inner join TblAddress ad " +
                            "on (cus.AddressId = ad.AddressId) " + " Order by CustomerName");
        }

        // lấy dữ liệu về mã khách hàng, họ và tên, ngày sinh, giới tính, điện thoại, địa chỉ, mã địa chỉ ứng với mã khách hàng,
        private string getInfoSql(string customerId)
        {
            return string.Format("Select CustomerId, CustomerName, DateOfBirth, Sex, Phone, Email, Address, AddressId from TblCustomer where CustomerId = '" + customerId + "'");
        }

        // lấy dữ liệu về mã khách hàng, họ và tên, ngày sinh, giới tính, điện thoại, địa chỉ, mã địa chỉ ứng với mã khách hàng,
        private string getInfoCustomerSql(string customerId)
        {
            return string.Format("Select CustomerId, CustomerName, DateOfBirth, Sex, Phone, Email,Address + ',' +ad.District + ',' + ad.City as Address from TblCustomer cus inner join TblAddress ad on (cus.AddressId = ad.AddressId) where CustomerId = '" + customerId + "'");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblCustomer ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblCustomer( CustomerId, CustomerName, DateOfBirth, Sex, Phone, Email, Address, AddressId, Description )" +
                                     " Values (N'{0}', N'{1}', '{2}', {3}, '{4}', N'{5}', N'{6}', {7}, N'{8}');",
                this.customerInfo.CustomerId,
                this.customerInfo.CustomerName,
                string.Format("{0:dd/MM/yyyy}", this.customerInfo.DateOfBirth),
                (this.customerInfo.Sex ? 1 : 0),
                this.customerInfo.Phone,
                this.customerInfo.Email,
                this.customerInfo.Address,
                this.customerInfo.AddressId,
                this.customerInfo.Description); ;
        }

        // trả về câu SQL update dữ liệu vào bảng TblCustomer ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblCustomer set CustomerName=N'{0}', DateOfBirth='{1}', Sex={2}, Phone=N'{3}', Email=N'{4}',Address=N'{5}', AddressId={6}, Description=N'{7}' Where CustomerId='{8}' ;",
                this.customerInfo.CustomerName,
                string.Format("{0:dd/MM/yyyy}", this.customerInfo.DateOfBirth),
                (this.customerInfo.Sex ? 1 : 0),
                this.customerInfo.Phone,
                this.customerInfo.Email,
                this.customerInfo.Address,
                this.customerInfo.AddressId,
                this.customerInfo.Description,
                this.customerInfo.CustomerId); ;
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblCustomer ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblCustomer where CustomerId='{0}'", this.customerInfo.CustomerId);
        }

        // thêm thông tin địa chỉ vào database
        public int addCustomer()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateCustomer()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteCustomer()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // đọc thông tin khách hàng từ database theo mã khách hàng và trả về CustomerEntity object cho nơi gọi
        // customerId: mã khách hàng muốn lấy dữ liệu
        public CustomerEntity getInfo(string customerId)
        {
            CustomerEntity customerEntity = new CustomerEntity();
            SqlDataReader reader = new DaoMsSqlServer().getDataReader(getInfoSql(customerId));
            while (reader.Read())
            {
                customerEntity.CustomerId   = reader.GetString(0);
                customerEntity.CustomerName = reader.GetString(1);
                customerEntity.DateOfBirth  = reader.GetDateTime(2);
                customerEntity.Sex          = reader.GetBoolean(3);
                customerEntity.Phone        = reader.GetString(4);
                customerEntity.Email        = reader.GetString(5);
                customerEntity.Address      = reader.GetString(6);
                customerEntity.AddressId    = reader.GetInt32(7);
            }

            return customerEntity;
        }

        // đọc thông tin khách hàng từ database theo mã khách hàng và trả về CustomerEntity object cho nơi gọi
        // customerId: mã khách hàng muốn lấy dữ liệu
        public CustomerEntity getInfoCustomer(string customerId)
        {
            CustomerEntity customerEntity = new CustomerEntity();
            SqlDataReader reader = new DaoMsSqlServer().getDataReader(getInfoCustomerSql(customerId));
            while (reader.Read())
            {
                customerEntity.CustomerId = reader.GetString(0);
                customerEntity.CustomerName = reader.GetString(1);
                customerEntity.DateOfBirth = reader.GetDateTime(2);
                customerEntity.Sex = reader.GetBoolean(3);
                customerEntity.Phone = reader.GetString(4);
                customerEntity.Email = reader.GetString(5);
                customerEntity.Address = reader.GetString(6);
            }

            return customerEntity;
        }

        // lấy thông tin khách hàng từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblCustomer");
        }

        // lấy thông tin khách hàng từ database và trả về dataset object cho nơi gọi
        public DataSet getData(string likename)
        {
            return new DaoMsSqlServer().getData(selectSql(likename), "TblCustomer");
        }
    }
}
