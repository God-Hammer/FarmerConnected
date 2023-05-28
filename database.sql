Use master
Go
CREATE DATABASE FarmerConnect
Go
USE FarmerConnect

CREATE TABLE [Admin] (
    Id uniqueidentifier primary key NOT NULL,
    Username nvarchar(256) NOT NULL,
    Password nvarchar(256) NOT NULL,
	AvatarUrl nvarchar(max),
	CreateAt datetime not null default getdate(),
);
Go
CREATE TABLE Customer (
    Id uniqueidentifier primary key NOT NULL,
	Email nvarchar(256) not null,
    Password nvarchar(256) NOT NULL,
	Name nvarchar(256) not null,
	Phone nvarchar(256),
	Address nvarchar(max),
	AvatarUrl nvarchar(max),
	IsActive bit,
	VerifyToken nvarchar(256) not null,
	VerifyTime datetime,
	CreateAt datetime not null default getdate(),
);
Go

CREATE TABLE Category (
    Id uniqueidentifier primary key NOT NULL,
    Name nvarchar(256),
	Description nvarchar(max),
	CreateAt datetime not null default getdate(),
);
Go

INSERT INTO Category (Id, Name, Description)
VALUES
(NEWID(), N'Nông nghiệp', N'Danh mục sản phẩm nông nghiệp');
Go



CREATE TABLE Product (
    Id uniqueidentifier primary key NOT NULL,
    Name nvarchar(256) not null,
	Price int not null,
	Quantity int not null,
	Description nvarchar(max),
	CustomerId uniqueidentifier foreign key references Customer(Id) NOT NULL,
	CategoryId uniqueidentifier foreign key references Category(Id) NOT NULL,
	Status nvarchar(256) not null,
	CreateAt datetime not null default getdate(),
	UpdateAt datetime,
);
Go

INSERT INTO Product (Id, Name, Price, Quantity, Description, CustomerId, CategoryId, Status)
VALUES
(NEWID(), N'Lúa mì', 10000, 50, N'Mô tả về lúa mì', '11aba470-9336-468f-89eb-fe6684ee2235', '9a9c8df9-35fd-4a1e-90de-6c2f7ad2294f', N'Đang bán'),
(NEWID(), N'Hạt giống rau', 5000, 100, N'Mô tả về hạt giống rau', '11aba470-9336-468f-89eb-fe6684ee2235', '9a9c8df9-35fd-4a1e-90de-6c2f7ad2294f', N'Đang bán'),
(NEWID(), N'Phân bón hữu cơ', 20000, 30, N'Mô tả về phân bón hữu cơ', '11aba470-9336-468f-89eb-fe6684ee2235', '9a9c8df9-35fd-4a1e-90de-6c2f7ad2294f', N'Đang bán');
Go

CREATE TABLE ProductImage (
    ProductId uniqueidentifier foreign key references Product(Id) NOT NULL,
    Type nvarchar(256),
	Url nvarchar(256),
	CreateAt datetime not null default getdate(),
);

CREATE TABLE MarketPrice (
    Id uniqueidentifier primary key NOT NULL,
    ProductName nvarchar(256)not null,
	Price int not null,
	UpdateAt datetime,
	CreateAt datetime not null default getdate(),
);
Go

CREATE TABLE Post(
    Id uniqueidentifier primary key NOT NULL,
    Title nvarchar(256)not null,
	MarketPriceId uniqueidentifier foreign key references MarketPrice(Id) NOT NULL,
	Description nvarchar(max) not null,
	UpdateAt datetime,
	CreateAt datetime not null default getdate(),
);
Go