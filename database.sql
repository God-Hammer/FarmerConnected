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
    ProductName nvarchar(256)not null,
	MarketPriceId uniqueidentifier foreign key references MarketPrice(Id) NOT NULL,
	Price int not null,
	UpdateAt datetime,
	CreateAt datetime not null default getdate(),
);
Go