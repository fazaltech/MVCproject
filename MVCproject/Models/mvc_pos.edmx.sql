
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/16/2020 14:20:10
-- Generated from EDMX file: D:\MVCproject\MVCproject\Models\mvc_pos.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [mvc_pos];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblcustomer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblcustomer];
GO
IF OBJECT_ID(N'[dbo].[tblinvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblinvoice];
GO
IF OBJECT_ID(N'[dbo].[tblproduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblproduct];
GO
IF OBJECT_ID(N'[dbo].[tblproductcategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblproductcategory];
GO
IF OBJECT_ID(N'[dbo].[tblproductunit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblproductunit];
GO
IF OBJECT_ID(N'[dbo].[tblpurchaseorder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblpurchaseorder];
GO
IF OBJECT_ID(N'[dbo].[tblreceiveproduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblreceiveproduct];
GO
IF OBJECT_ID(N'[dbo].[tblsales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblsales];
GO
IF OBJECT_ID(N'[dbo].[tblsupplier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblsupplier];
GO
IF OBJECT_ID(N'[dbo].[tbluser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbluser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblcustomers'
CREATE TABLE [dbo].[tblcustomers] (
    [int] int  NOT NULL,
    [customer_id] decimal(18,0)  NOT NULL,
    [customer_code] varchar(50)  NULL,
    [customer_name] varchar(50)  NULL,
    [contact] varchar(50)  NULL,
    [address] varchar(50)  NULL,
    [flag] int  NULL
);
GO

-- Creating table 'tblinvoices'
CREATE TABLE [dbo].[tblinvoices] (
    [id] int  NOT NULL,
    [invoice_id] decimal(18,0)  NOT NULL,
    [customer_id] decimal(18,0)  NULL,
    [payment_type] decimal(18,0)  NULL,
    [total_amount] decimal(18,0)  NULL,
    [amount_tendered] decimal(18,0)  NULL,
    [bank_account_name] varchar(50)  NULL,
    [bank_account_number] varchar(50)  NULL,
    [date_recorded] varchar(50)  NULL,
    [user_id] decimal(18,0)  NULL,
    [flag] int  NULL
);
GO

-- Creating table 'tblproducts'
CREATE TABLE [dbo].[tblproducts] (
    [id] int IDENTITY(1,1) NOT NULL,
    [product_id] decimal(18,0)  NULL,
    [product_code] varchar(50)  NULL,
    [product_name] varchar(50)  NULL,
    [unit_id] decimal(18,0)  NULL,
    [category_id] decimal(18,0)  NULL,
    [unit_in_stock] decimal(18,0)  NULL,
    [unit_price] decimal(18,0)  NULL,
    [discount_percentage] decimal(18,0)  NULL,
    [recorder_level] decimal(18,0)  NULL,
    [user_id] decimal(18,0)  NULL
);
GO

-- Creating table 'tblproductcategories'
CREATE TABLE [dbo].[tblproductcategories] (
    [id] int IDENTITY(1,1) NOT NULL,
    [category_id] decimal(18,0)  NULL,
    [category_name] varchar(50)  NULL
);
GO

-- Creating table 'tblproductunits'
CREATE TABLE [dbo].[tblproductunits] (
    [id] int IDENTITY(1,1) NOT NULL,
    [unit_id] decimal(18,0)  NOT NULL,
    [unit_name] varchar(50)  NULL
);
GO

-- Creating table 'tblpurchaseorders'
CREATE TABLE [dbo].[tblpurchaseorders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [purchase_order_id] decimal(18,0)  NULL,
    [product_id] decimal(18,0)  NULL,
    [quantity] decimal(18,0)  NULL,
    [unit_price] decimal(18,0)  NULL,
    [sub_total] decimal(18,0)  NULL,
    [supplier_id] decimal(18,0)  NULL,
    [order_date] varchar(50)  NULL,
    [user_id] decimal(18,0)  NULL,
    [order_status] int  NULL,
    [flag] int  NULL
);
GO

-- Creating table 'tblreceiveproducts'
CREATE TABLE [dbo].[tblreceiveproducts] (
    [int] int IDENTITY(1,1) NOT NULL,
    [receive_product_id] decimal(18,0)  NULL,
    [product_id] decimal(18,0)  NULL,
    [quantity] decimal(18,0)  NULL,
    [unit_price] decimal(18,0)  NULL,
    [sub_total] decimal(18,0)  NULL,
    [supplier_id] decimal(18,0)  NULL,
    [received_date] varchar(50)  NULL,
    [user_id] decimal(18,0)  NULL,
    [flag] int  NULL
);
GO

-- Creating table 'tblsales'
CREATE TABLE [dbo].[tblsales] (
    [id] int IDENTITY(1,1) NOT NULL,
    [sale_id] decimal(18,0)  NULL,
    [invoice_id] decimal(18,0)  NULL,
    [product_id] decimal(18,0)  NULL,
    [quantity] decimal(18,0)  NULL,
    [unit_price] decimal(18,0)  NULL,
    [sub_total] decimal(18,0)  NULL
);
GO

-- Creating table 'tblsuppliers'
CREATE TABLE [dbo].[tblsuppliers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [supplier_int] decimal(18,0)  NULL,
    [supplier_code] varchar(50)  NULL,
    [supplier_name] varchar(50)  NULL,
    [supplier_contact] varchar(50)  NULL,
    [supplier_address] varchar(50)  NULL,
    [supplier_email] varbinary(50)  NULL,
    [flag] int  NULL
);
GO

-- Creating table 'tblusers'
CREATE TABLE [dbo].[tblusers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_id] decimal(18,0)  NULL,
    [user_name] varchar(50)  NULL,
    [password] varchar(50)  NULL,
    [fullname] varchar(50)  NULL,
    [designation] varchar(50)  NULL,
    [role] varchar(50)  NULL,
    [account_type] int  NULL,
    [flag] varchar(50)  NULL,
    [email_id] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [int] in table 'tblcustomers'
ALTER TABLE [dbo].[tblcustomers]
ADD CONSTRAINT [PK_tblcustomers]
    PRIMARY KEY CLUSTERED ([int] ASC);
GO

-- Creating primary key on [id] in table 'tblinvoices'
ALTER TABLE [dbo].[tblinvoices]
ADD CONSTRAINT [PK_tblinvoices]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblproducts'
ALTER TABLE [dbo].[tblproducts]
ADD CONSTRAINT [PK_tblproducts]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblproductcategories'
ALTER TABLE [dbo].[tblproductcategories]
ADD CONSTRAINT [PK_tblproductcategories]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblproductunits'
ALTER TABLE [dbo].[tblproductunits]
ADD CONSTRAINT [PK_tblproductunits]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblpurchaseorders'
ALTER TABLE [dbo].[tblpurchaseorders]
ADD CONSTRAINT [PK_tblpurchaseorders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [int] in table 'tblreceiveproducts'
ALTER TABLE [dbo].[tblreceiveproducts]
ADD CONSTRAINT [PK_tblreceiveproducts]
    PRIMARY KEY CLUSTERED ([int] ASC);
GO

-- Creating primary key on [id] in table 'tblsales'
ALTER TABLE [dbo].[tblsales]
ADD CONSTRAINT [PK_tblsales]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblsuppliers'
ALTER TABLE [dbo].[tblsuppliers]
ADD CONSTRAINT [PK_tblsuppliers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tblusers'
ALTER TABLE [dbo].[tblusers]
ADD CONSTRAINT [PK_tblusers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------