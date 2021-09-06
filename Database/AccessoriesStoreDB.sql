USE [master]
CREATE DATABASE [AccessoriesStoreDB]
CONTAINMENT = NONE
ON PRIMARY
( NAME = N'AccessoriesStoreDB', FILENAME = N'D:\Project\Database\AccessoriesStoreDB.mdf' , SIZE = 3136KB ,
MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
LOG ON
( NAME = N'AccessoriesStoreDB_log', FILENAME = N'D:\Project\Database\AccessoriesStoreDB_log.ldf' , SIZE = 784KB ,
MAXSIZE = 2048GB , FILEGROWTH = 10%)

USE [AccessoriesStoreDB]

CREATE TABLE [dbo].[TblAddress]
(
	[AddressId]	INT NOT NULL IDENTITY(1, 1),
	[District]		[nvarchar](100) NULL,
	[City]			[nvarchar](100) NULL,
	[Description]	[nvarchar](255) NULL,
	 PRIMARY KEY CLUSTERED ([AddressId] ASC) 
)
/*ALTER TABLE [AccessoriesStoreDB].[dbo].[TblAddress] drop COLUMN [Ward];
delete column in table */

select * from TblAddress;

Insert Into [dbo].[TblAddress]( District, City) Values ( N'Vĩnh Lợi', N'Bạc Liêu');
Insert Into [dbo].[TblAddress]( District, City) Values ( N'Hòa Bình', N'Bạc Liêu');
Insert Into [dbo].[TblAddress]( District, City) Values ( N'TP Bạc Liêu', N'Bạc Liêu');
Insert Into [dbo].[TblAddress]( District, City) Values ( N'Bình Thạnh', N'Hồ Chí Minh');
Insert Into [dbo].[TblAddress]( District, City) Values ( N'Thủ Đức', N'Hồ Chí Minh');
Insert Into [dbo].[TblAddress]( District, City) Values ( N'Gò Vấp', N'Hồ Chí Minh');
Insert Into [dbo].[TblAddress]( District, City) Values ( N'12', N'Hồ Chí Minh');

CREATE TABLE [dbo].[TblCategory]
(
	[CategoryId]	INT NOT NULL IDENTITY(1, 1),
	[CategoryName]	[nvarchar](100) NULL,
	[Description]	[nvarchar](255) NULL,
	 PRIMARY KEY CLUSTERED ([CategoryId] ASC) 
)

CREATE TABLE [dbo].[TblRole]
(
	[RoleId]	INT NOT NULL IDENTITY(1, 1),
	[RoleName]	[nvarchar](100) NULL,
	[Description]	[nvarchar](255) NULL,
	 PRIMARY KEY CLUSTERED ([RoleId] ASC) 
)

select * from TblRole;

Insert Into [dbo].[TblRole]( RoleName) Values ( N'Admin');
Insert Into [dbo].[TblRole]( RoleName) Values ( N'Thu ngân');
Insert Into [dbo].[TblRole]( RoleName) Values ( N'Giao hàng');
Insert Into [dbo].[TblRole]( RoleName) Values ( N'Nhân viên');

CREATE TABLE [dbo].[TblAccount]
(
	[Username]		[nvarchar](50) NOT NULL,
	[Password]		[nvarchar](150) NULL,
	[RoleId]		int not null,
	[FirstName]		[nvarchar](150) NULL,
	[LastName]		[nvarchar](50) NULL,
	[DateOfBirth]	datetime null,
	[Sex]			bit default 1,
	[Phone]			[varchar](20) NULL,
	[Email]			[nvarchar](150) NULL,
	[Address]		[nchar](150) NULL,
	[AddressId]		int not null,
	[Status]		bit default 0,
	[Description]	[nvarchar](255) NULL,
	 PRIMARY KEY CLUSTERED ([Username] ASC) 
)
/* nâng chiều dài mật khẩu lên 200*/
ALTER TABLE [AccessoriesStoreDB].[dbo].[TblAccount]
ALTER COLUMN [Password] [nvarchar](200) NULL;

ALTER TABLE [dbo].[TblAccount] WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[TblRole] ([RoleId])
GO

ALTER TABLE [dbo].[TblAccount] WITH CHECK ADD FOREIGN KEY([AddressId])
REFERENCES [dbo].[TblAddress] ([AddressId])
GO

CREATE TABLE [dbo].[TblCustomer]
(
	[CustomerId]	[nvarchar](10) NOT NULL,
	[CustomerName]	[nvarchar](250) NULL,
	[Phone]			[nvarchar](20) NULL,
	[Email]			[nvarchar](200) NULL,
	[AddressId]		int not null,
	[DateOfBirth]	datetime null,
	[Sex]			bit default 1,
	[Description]	[nvarchar](255) NULL,
	[Address]		[nvarchar](200) NULL,
	 PRIMARY KEY CLUSTERED ([CustomerId]) 
)
ALTER TABLE [dbo].[TblCustomer] WITH CHECK ADD FOREIGN KEY([AddressId])
REFERENCES [dbo].[TblAddress] ([AddressId])
GO

CREATE TABLE [dbo].[TblProduct]
(
	[ProductId]		[nvarchar](10) NOT NULL,
	[ProductName]	[nvarchar](200) NULL,
	[CategoryId]	INT NOT NULL,
	[Image]			[nvarchar](max) NULL,
	[Manufactur]	[nvarchar](200) NULL,
	[EnteredDate]	[datetime] default current_timestamp,
	[Account]		[nvarchar](50) NOT NULL,
	[Status]		bit default 0,
	[Quantity]		TINYINT default 0,
	[UnitPrice]		float default 0,
	[Discount]		float default 0 check ([Discount] >= 0 and [Discount] <= 100),
	[Description]	[nvarchar](255) NULL,
	 PRIMARY KEY CLUSTERED ([ProductId]) 
)

ALTER TABLE [dbo].[TblProduct] WITH CHECK ADD FOREIGN KEY([Account])
REFERENCES [dbo].[TblAccount] ([Username])
GO

ALTER TABLE [dbo].[TblProduct] WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[TblCategory] ([CategoryId])
GO

CREATE TABLE [dbo].[TblOrder]
(
	[OrderId]		[nvarchar](50) NOT NULL,
	[Account]		[nvarchar](50) NOT NULL,
	[CustomerId]	[nvarchar](10) NULL,
	[OrderDate]		[datetime] NULL,
	[Status]		bit default 1,
	[DepartureDate]	[datetime] NULL,
	[DeliveryAddress] [nvarchar](255) NULL,
	[Description]	[nvarchar](255) NULL,
	 PRIMARY KEY CLUSTERED ([OrderId]) 
)
/* sửa kiểu dữ liệu lại là int thay vì bit
ALTER TABLE [AccessoriesStoreDB].[dbo].[TblOrder]
ALTER COLUMN [Status] int;
-- xóa dữ liệu mặc định của cột status
ALTER TABLE [AccessoriesStoreDB].[dbo].[TblOrder]
DROP CONSTRAINT df_TblOrder_Status;
-- đặt dữ liệu mặc định của cột status là 0
ALTER TABLE [AccessoriesStoreDB].[dbo].[TblOrder]
ADD CONSTRAINT df_TblOrder_Status
DEFAULT 0 FOR [Status];
*/
ALTER TABLE [dbo].[TblOrder] WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[TblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[TblOrder] WITH CHECK ADD FOREIGN KEY([Account])
REFERENCES [dbo].[TblAccount] ([Username])
GO

CREATE TABLE [dbo].[TblOrderDetail]
(
	[OrderDetailId]	INT NOT NULL IDENTITY(1, 1),
	[OrderId]		[nvarchar](50) NOT NULL,
	[ProductId]		[nvarchar](10) NOT NULL,
	[Quantity]		TINYINT NULL,
	[UnitPrice]		float NULL,
	[DiscountPrice]	float NULL,
	 PRIMARY KEY CLUSTERED ([OrderDetailId] ASC) 
)
ALTER TABLE [dbo].[TblOrderDetail] WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[TblOrder] ([OrderId])
GO
ALTER TABLE [dbo].[TblOrderDetail] WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[TblProduct] ([ProductId])
GO

-- Xây dựng hàm phát sinh mã khách hàng có dạng "KH0001" theo thứ tự tăng dần
Create function fn_CreateMaKH()
	returns nvarchar(10)
begin
		
		declare @MaKHOld varchar(10), @MaKHNew nvarchar(10)
		select Top 1 @MaKHOld=CustomerId from TblCustomer order by CustomerId Desc
		Return 'KH' + format(right(@MaKHOld,4)+1,'000#')
end
Go

select dbo.fn_CreateMaKH()