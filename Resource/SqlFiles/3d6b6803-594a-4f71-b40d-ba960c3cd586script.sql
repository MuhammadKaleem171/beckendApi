USE [master]
GO
CREATE DATABASE [Ecomerece]
GO
use [Ecomerece]
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] NULL,
	[First_Name] [varchar](20) NULL,
	[Last_Name] [varchar](20) NULL,
	[Email] [varchar](30) NULL,
) ON [PRIMARY]
insert into Customer (CustomerID,First_Name,Last_Name,Email) values(1,'malik','kaleem','malik@gmail.com')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[PaymentID] [int] NOT NULL,
	[order_Date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [varchar](30) NULL,
	[Product_Description] [varchar](60) NULL,
	[Price] [float] NULL,
	[Quantity] [int] NULL
) ON [PRIMARY]
GO
insert into Product(Product_Name,Product_Description,Price,Quantity)values('iphone','iphone xl',34444,3)
GO
insert into Product(Product_Name,Product_Description,Price,Quantity)values('iphone','iphone XL PRO',40000,3)
GO
insert into Product(Product_Name,Product_Description,Price,Quantity)values('SUMSUNG','j PRIME ',20000,2)
GO
insert into Product(Product_Name,Product_Description,Price,Quantity)values('sumsung','galaxy g5 ',10000,10)
GO
insert into Customer (CustomerID,First_Name,Last_Name,Email) values(1,'malik','kaleem','malik@gmail.com')
GO
insert into Customer (CustomerID,First_Name,Last_Name,Email) values(1,'hassan','mirza','malik@gmail.com')
GO
insert into Customer (CustomerID,First_Name,Last_Name,Email) values(1,'malik','saad','malik@gmail.com')

