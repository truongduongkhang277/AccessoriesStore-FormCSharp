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
    class BusCategory
    {
        public CategoryEntity categoryInfo { get; set; }

        // default contructor
        public BusCategory()
        {
            this.categoryInfo = new CategoryEntity();
        }

        private string selectSql()
        {
            return string.Format("Select * from TblCategory");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblCategory ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "Insert Into TblCategory(CategoryName, Description) Values (N'{0}', N'{1}');",
                this.categoryInfo.CategoryName,
                this.categoryInfo.Description);
        }

        // trả về câu SQL update dữ liệu vào bảng TblCategory ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblCategory set CategoryName=N'{0}', Description=N'{1}' Where CategoryId={2} ;",
                this.categoryInfo.CategoryName,
                this.categoryInfo.Description,
                this.categoryInfo.CategoryId);
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblCategory ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblCategory where CategoryId={0}", this.categoryInfo.CategoryId);
        }

        // thêm thông tin địa chỉ vào database
        public int addCategory()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateCategory()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteCategory()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // lấy thông tin địa chỉ từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblCategory");
        }
    }
}
