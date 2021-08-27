﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace TruongDuongKhang_1811546141.DataAccessLayer
{
    class DaoMsSqlServer
    {
        private string stringConnect;

        // default contructor: lấy chuỗi kết nối từ cài đặt của ứng dụng (Properties - Setting - stringConnect)
        public DaoMsSqlServer()
        {
            this.stringConnect = Properties.Settings.Default.stringConnect;
            Console.WriteLine(Properties.Settings.Default.stringConnect);
        }

        // trả về cho nơi gọi 1 SqlConnection Object đã kết nối
        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(this.stringConnect);
            connection.Open();
            return connection;
        }

        // Hàm cho phép thực thi các lệnh DML thuộc dạng insert - update - delete
        // query: câu truy vấn T-SQL cần thực thi trên MSSQL Server 
        public int executeNonQuery(string query)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand(query, getConnection());
            result = cmd.ExecuteNonQuery();
            return result;
        }

        // lấy dữ liệu từ database và trả về dataset object cho nơi gọi
        // query: câu truy vấn T-SQL cần thực thi trên MSSQL Server 
        // table: tên table dùng để mapping cho dataset

        public DataSet getData(string query, string table)
        {
            // tạo dataset
            DataSet ds = new DataSet();

            // tạo data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(query, getConnection());

            // fill data to dataset
            adapter.TableMappings.Add("Table", table);
            adapter.Fill(ds);

            return ds;
        }
    }
}