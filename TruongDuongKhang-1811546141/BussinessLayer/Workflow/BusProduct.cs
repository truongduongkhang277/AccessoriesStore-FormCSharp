using System.Data;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.DataAccessLayer;
using System.Data.SqlClient;
using TruongDuongKhang_1811546141.Lib;
using System;

namespace TruongDuongKhang_1811546141.BussinessLayer.Workflow
{
    class BusProduct
    {
        public ProductEntity productInfo { get; set; }

        // default contructor
        public BusProduct()
        {
            this.productInfo = new ProductEntity();
        }

        // contructor with one parameter
        public BusProduct(ProductEntity entity)
        {
            this.productInfo = entity;
        }

        // lấy dữ liệu về mã SP, mã loại SP, tên SP, nhà sản xuất, số lượng, ngày nhập hàng, tài khoản phê duyệt, giá SP, giảm giá, chú thích, hình của SP
        private string selectSql(bool isActive, int cateId)
        {
            return string.Format("Select ProductId, ProductName, Manufactur, Quantity, UnitPrice, Discount, Description " +
                            "from TblProduct prod where Status = " + (isActive ? "1" : "0") + (cateId > 0 ? " And prod.CategoryId = " + cateId.ToString() : "") + " Order by ProductName");
        }

        // lấy dữ liệu về mã SP, mã loại SP, tên SP, nhà sản xuất, số lượng, ngày nhập hàng, tài khoản phê duyệt, giá SP, giảm giá, chú thích, hình của SP
        private string selectSql(string likename)
        {
            return string.Format("Select ProductId, ProductName, Manufactur, Quantity, UnitPrice, Discount, cast(UnitPrice * Discount / 100 as int) DiscountPrice, Description  " +
                            "from TblProduct where Status = 1 " + (likename.Trim().Length > 0 ? " And ProductName like N'%" + likename.Trim() + "%'" : "") +
                            " Order by ProductId");
        }

        // lấy dữ liệu về mã SP, mã loại SP, tên SP, nhà sản xuất, số lượng, ngày nhập hàng, tài khoản phê duyệt, giá SP,
        // giảm giá, chú thích, hình của SP
        private string selectSql()
        {
            return string.Format("Select ProductId, ProductName, CategoryId, Image, Manufactur, FORMAT(EnteredDate, 'dd/MM/yyyy') as EnteredDate, " +
                            "Account, iif(Status=1,N'Đã phê duyệt', N'Chưa phê duyệt') as Status, Quantity, UnitPrice, Discount, Description " +
                            "from TblProduct " + " Order by ProductId");
        }

        // lấy dữ liệu về mã SP, mã loại SP, tên SP, nhà sản xuất, số lượng, ngày nhập hàng, tài khoản phê duyệt, giá SP,
        // giảm giá, chú thích, hình ứng với mã SP
        private string getInfoSql(string productId)
        {
            return string.Format("Select " +
                "ProductId, ProductName, CategoryId, Image, Manufactur, EnteredDate, Account, Status, Quantity, UnitPrice, Discount, Description " +
                "from TblProduct where ProductId = '" + productId + "'");
        }

        // trả về câu SQL insert dữ liệu vào bảng TblProduct ( mssql server )
        private string insertSql()
        {
            return string.Format(
                "set dateformat dmy;" +
                "Insert Into TblProduct" +
                "(ProductId, ProductName, CategoryId, Image, Manufactur, EnteredDate, Account, Status, Quantity, UnitPrice, Discount, Description )" +
                " Values (N'{0}', N'{1}', {2}, '{3}', N'{4}', '{5}', N'{6}', {7}, {8}, {9}, {10}, N'{11}');",
                this.productInfo.ProductId,
                this.productInfo.ProductName,
                this.productInfo.CategoryId.ToString(),
                Tools.imageToString(this.productInfo.Image),
                this.productInfo.Manufactur,
                string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now),
                this.productInfo.Account,
                "0",
                this.productInfo.Quantity.ToString(),
                this.productInfo.UnitPrice.ToString(),
                this.productInfo.Discount.ToString(),
                this.productInfo.Description); ;
        }

        // trả về câu SQL update dữ liệu vào bảng TblProduct ( mssql server )
        private string updateSql()
        {
            return string.Format(
                "Update TblProduct set ProductName =N'{0}', CategoryId ={1}, Manufactur =N'{2}', Status ={3}, " +
                                       "Quantity ={4}, UnitPrice ={5}, Discount ={6}, Description =N'{7}' Where ProductId=N'{8}' ;",
                this.productInfo.ProductName,
                this.productInfo.CategoryId,
                this.productInfo.Manufactur,
                (this.productInfo.Status ? 1 : 0),
                this.productInfo.Quantity,
                this.productInfo.UnitPrice,
                this.productInfo.Discount,
                this.productInfo.Description,
                this.productInfo.ProductId); ;
        }

        // trả về câu SQL xóa dữ liệu vào bảng TblProduct ( mssql server )
        private string deleteSql()
        {
            return string.Format("Delete TblProduct where ProductId='{0}'", this.productInfo.ProductId);
        }

        // thêm thông tin địa chỉ vào database
        public int addProduct()
        {

            return new DaoMsSqlServer().executeNonQuery(insertSql());
        }

        // cập nhật thông tin địa chỉ vào database
        public int updateProduct()
        {

            return new DaoMsSqlServer().executeNonQuery(updateSql());
        }

        // xóa thông tin địa chỉ vào database
        public int deleteProduct()
        {
            return new DaoMsSqlServer().executeNonQuery(deleteSql());
        }

        // đọc thông tin khách hàng từ database theo mã khách hàng và trả về ProductEntity object cho nơi gọi
        // ProductId: mã khách hàng muốn lấy dữ liệu
        public ProductEntity getInfo(string productId)
        {
            ProductEntity ProductEntity = new ProductEntity();
            SqlDataReader reader = new DaoMsSqlServer().getDataReader(getInfoSql(productId));
            while (reader.Read())
            {
                ProductEntity.ProductId     = reader.GetString(0);
                ProductEntity.ProductName   = reader.GetString(1);
                ProductEntity.CategoryId    = reader.GetInt32(2);
                ProductEntity.Image         = Tools.stringToImage(reader.GetString(3));
                ProductEntity.Manufactur    = reader.GetString(4);
                ProductEntity.EnteredDate   = reader.GetDateTime(5);
                ProductEntity.Account       = reader.GetString(6);
                ProductEntity.Status        = reader.GetBoolean(7);
                ProductEntity.Quantity      = reader.GetByte(8);
                ProductEntity.UnitPrice     = (int)reader.GetDouble(9);
                ProductEntity.Discount      = (int)reader.GetDouble(10);
                ProductEntity.Description   = reader.GetString(11);
            }

            return ProductEntity;
        }

        // lấy thông tin khách hàng từ database và trả về dataset object cho nơi gọi
        public DataSet getData()
        {
            return new DaoMsSqlServer().getData(selectSql(), "TblProduct");
        }

        // lấy thông tin sản phẩm từ database và trả về dataset object cho nơi gọi
        // dựa vào trạng thái isActive để chọn ds : true - đã kích hoạt, false - chưa kích hoạt
        // dựa vào giá trị cateId để lọc [nếu là 0 thì không lọc ]
        public DataSet getData(bool isActive, int cateId)
        {
            return new DaoMsSqlServer().getData(selectSql(isActive, cateId), "TblProduct");
        }

        // lấy thông tin sản phẩm từ database và trả về dataset object cho nơi gọi
        // dựa vào tên sản phẩm truyền vào để chọn ds
        public DataSet getData(string likename)
        {
            return new DaoMsSqlServer().getData(selectSql(likename), "TblProduct");
        }
    }
}
