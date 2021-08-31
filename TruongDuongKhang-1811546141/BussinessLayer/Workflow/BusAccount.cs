using System.Data;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;
using System.Data.SqlClient;

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

        // lấy dữ liệu về họ và tên, tên đăng nhập, ngày sinh, giới tính, điện thoại, địa chỉ của tài khoản
        private string selectSql(bool isActive, int roleId)
        {
            return string.Format("Select " +
                            "FirstName + ' '  +LastName as FullName, " +
                            "Username, " +
                            "FORMAT(DateOfBirth, 'dd/MM/yyyy') as DateOfBirth, " +
                            "iif(Sex=1,'Nam', N'Nữ') as Sex, " +
                            "Phone, " +
                            "Address + ',' +ad.District + ',' + ad.City as Address, " +
                            "iif(Status=1,N'Đã kích hoạt', N'Chưa kích hoạt') as Status " +
                            "from TblAccount acc inner join TblAddress ad " +
                            "on (acc.AddressId = ad.AddressId) " +
                            "where Status = " + (isActive ? "1" : "0") + (roleId > 0 ? " And acc.RoleId = " + roleId.ToString() : "") + " Order by FullName");
        }

        // lấy dữ liệu về họ và tên, tên đăng nhập, ngày sinh, giới tính, điện thoại, địa chỉ của tài khoản
        private string selectSql()
        {
            return string.Format("Select " +
                            "FirstName + ' '  +LastName as FullName, " +
                            "Username, " +
                            "FORMAT(DateOfBirth, 'dd/MM/yyyy') as DateOfBirth, " +
                            "iif(Sex=1,'Nam', N'Nữ') as Sex, " +
                            "Phone, " +
                            "Address + ',' +ad.District + ',' + ad.City as Address, " +
                            "iif(Status=1,N'Đã kích hoạt', N'Chưa kích hoạt') as Status " +
                            "from TblAccount acc inner join TblAddress ad " +
                            "on (acc.AddressId = ad.AddressId) Order by FullName");
        }

        // lấy dữ liệu về họ và tên, tên đăng nhập, ngày sinh, giới tính, điện thoại, địa chỉ, mã loại tài khoản, mã địa chỉ ứng với tên đăng nhập
        private string getInfoSql(string username)
        {
            return string.Format("Select Username, Password, RoleId, FirstName, LastName, DateOfBirth, Sex, Phone, Email, Address, AddressId, Description from TblAccount where Username = '" + username + "'");
        }



        // trả về câu SQL insert dữ liệu vào bảng TblAccount ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblAccount( Username, Password, RoleId, FirstName, LastName, DateOfBirth, Sex, Phone, Email, Address, AddressId, Status, Description )" +
                                     " Values (N'{0}', N'{1}', {2}, N'{3}', N'{4}', {5}, {6}, N'{7}', N'{8}', N'{9}', {10}, {11}, N'{12}');",
                this.accountInfo.Username,
                this.accountInfo.Password,
                this.accountInfo.RoleId,
                this.accountInfo.FirstName,
                this.accountInfo.LastName,
                string.Format("{0:dd/MM/yyyy}", this.accountInfo.DateOfBirth),
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
                "Update TblAccount set RoleId={0}, FirstName=N'{1}', LastName=N'{2}', DateOfBirth={3}, " +
            " Sex={4}, Phone=N'{5}', Email=N'{6}',Address=N'{7}', AddressId={8}, Status={9}, Description=N'{10}' " +
                "Where Username='{11}' ;",
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
            return string.Format("Delete TblAccount where Username='{0}'", this.accountInfo.Username);
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

        // đọc thông tin tài khoản từ database theo tên đăng nhập và trả về AccountEntity object cho nơi gọi
        // username: tên tài khoản muốn lấy dữ liệu
        public AccountEntity getInfo(string username)
        {
            AccountEntity accountEntity = new AccountEntity();

            SqlDataReader reader = new DaoMsSqlServer().getDataReader(getInfoSql(username));
            while (reader.Read())
            {
                accountEntity.Username  = reader.GetString(0);
                accountEntity.Password  = reader.GetString(1);
                accountEntity.RoleId    = reader.GetInt32(2);
                accountEntity.FirstName = reader.GetString(3);
                accountEntity.LastName  = reader.GetString(4);
                accountEntity.DateOfBirth = reader.GetDateTime(5);
                accountEntity.Sex       = reader.GetBoolean(6);
                accountEntity.Phone     = reader.GetString(7);
                accountEntity.Email     = reader.GetString(8);
                accountEntity.Address   = reader.GetString(9);
                accountEntity.AddressId = reader.GetInt32(10);
                accountEntity.Description = reader.GetString(11);
            }

            return accountEntity;
        }

        // lấy thông tin tài khoản từ database và trả về dataset object cho nơi gọi

        // dựa vào trạng thái isActive để chọn ds : true - đã kích hoạt, false - chưa kích hoạt
        // dựa vào giá trị roleId để lọc [nếu là 0 thì không lọc ]
        public DataSet getData(bool isActive, int roleId)
        {
            return new DaoMsSqlServer().getData(selectSql(isActive, roleId), "TblAccount");
        }

        // lấy thông tin tài khoản từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblAccount");
        }
    }
}
