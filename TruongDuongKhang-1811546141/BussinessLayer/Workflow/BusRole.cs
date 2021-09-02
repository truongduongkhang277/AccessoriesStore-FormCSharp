using System.Data;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;

namespace TruongDuongKhang_1811546141.BussinessLayer.Workflow
{
    class BusRole
    {
        public RoleEntity roleInfo { get; set; }

        // default contructor
        public BusRole()
        {
            this.roleInfo = new RoleEntity();
        }

        private string selectSql()
        {
            return string.Format("Select r.RoleId, r.RoleName, r.Description, (select count(*) from TblAccount where RoleId = r.RoleId) as 'NumOfAcc' from TblRole r");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblRole ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblRole(RoleName, Description) Values (N'{0}', N'{1}');",
                this.roleInfo.RoleName,
                this.roleInfo.Description);
        }

        // trả về câu SQL update dữ liệu vào bảng TblRole ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblRole set RoleName=N'{0}', Description=N'{1}' Where RoleId={2} ;",
                this.roleInfo.RoleName,
                this.roleInfo.Description,
                this.roleInfo.RoleId);
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblRole ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblRole where RoleId={0}", this.roleInfo.RoleId);
        }

        // thêm thông tin địa chỉ vào database
        public int addRole()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateRole()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteRole()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // lấy thông tin địa chỉ từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblRole");
        }
    }
}
