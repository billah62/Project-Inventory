
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2025 20:59:56
-- Generated from EDMX file: C:\Users\user\Downloads\inventoryProject\inventoryProject\inventoryProject\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StockManage];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_product_product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product] DROP CONSTRAINT [FK_product_product];
GO
IF OBJECT_ID(N'[dbo].[FK_purchase_product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[purchase] DROP CONSTRAINT [FK_purchase_product];
GO
IF OBJECT_ID(N'[dbo].[FK_purchase_store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[purchase] DROP CONSTRAINT [FK_purchase_store];
GO
IF OBJECT_ID(N'[dbo].[FK_purchase_supplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[purchase] DROP CONSTRAINT [FK_purchase_supplier];
GO
IF OBJECT_ID(N'[dbo].[FK_Sale_customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sale] DROP CONSTRAINT [FK_Sale_customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Sale_product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sale] DROP CONSTRAINT [FK_Sale_product];
GO
IF OBJECT_ID(N'[dbo].[FK_Sale_store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sale] DROP CONSTRAINT [FK_Sale_store];
GO
IF OBJECT_ID(N'[dbo].[FK_stock_product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[stock] DROP CONSTRAINT [FK_stock_product];
GO
IF OBJECT_ID(N'[dbo].[FK_stock_store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[stock] DROP CONSTRAINT [FK_stock_store];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customer];
GO
IF OBJECT_ID(N'[dbo].[produc_type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[produc_type];
GO
IF OBJECT_ID(N'[dbo].[product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[product];
GO
IF OBJECT_ID(N'[dbo].[purchase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[purchase];
GO
IF OBJECT_ID(N'[dbo].[Sale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sale];
GO
IF OBJECT_ID(N'[dbo].[stock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[stock];
GO
IF OBJECT_ID(N'[dbo].[store]', 'U') IS NOT NULL
    DROP TABLE [dbo].[store];
GO
IF OBJECT_ID(N'[dbo].[supplier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[supplier];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'customers'
CREATE TABLE [dbo].[customers] (
    [customer_id] int IDENTITY(1,1) NOT NULL,
    [customer_name] varchar(50)  NULL,
    [mobile] varchar(50)  NULL,
    [address] varchar(50)  NULL
);
GO

-- Creating table 'produc_type'
CREATE TABLE [dbo].[produc_type] (
    [product_type_id] int IDENTITY(1,1) NOT NULL,
    [product_type_name] varchar(50)  NULL
);
GO

-- Creating table 'products'
CREATE TABLE [dbo].[products] (
    [product_id] int IDENTITY(1,1) NOT NULL,
    [product_name] varchar(50)  NULL,
    [product_type_id] int  NULL,
    [buying_price] decimal(18,8)  NULL,
    [selling_price] decimal(18,8)  NULL,
    [photo] varchar(500)  NULL
);
GO

-- Creating table 'purchases'
CREATE TABLE [dbo].[purchases] (
    [purchase_id] int IDENTITY(1,1) NOT NULL,
    [product_id] int  NULL,
    [supplier_id] int  NULL,
    [store_id] int  NULL,
    [purchase_date] datetime  NULL,
    [unit_price] decimal(18,8)  NULL,
    [quantity] int  NULL,
    [total_price] decimal(18,8)  NULL,
    [vat] decimal(18,8)  NULL,
    [grand_total_price] decimal(18,8)  NULL,
    [stock_status] varchar(50)  NULL,
    [memo_no] int  NULL,
    [coomments] varchar(50)  NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [sale_id] int IDENTITY(1,1) NOT NULL,
    [product_id] int  NULL,
    [customer_id] int  NULL,
    [store_id] int  NULL,
    [sale_date] datetime  NULL,
    [rate] decimal(18,2)  NULL,
    [quantity] int  NULL,
    [total_price] decimal(18,2)  NULL,
    [vat] decimal(18,2)  NULL,
    [discount] decimal(18,2)  NULL,
    [net_total_price] decimal(18,2)  NULL,
    [stock_status] varchar(50)  NULL,
    [memo_no] int  NULL,
    [coomments] varchar(50)  NULL
);
GO

-- Creating table 'stocks'
CREATE TABLE [dbo].[stocks] (
    [stock_id] int IDENTITY(1,1) NOT NULL,
    [product_id] int  NULL,
    [store_id] int  NULL,
    [quantity] int  NULL,
    [status] varchar(50)  NULL
);
GO

-- Creating table 'stores'
CREATE TABLE [dbo].[stores] (
    [store_id] int IDENTITY(1,1) NOT NULL,
    [store_no] int  NULL,
    [store_name] varchar(50)  NULL,
    [address] varchar(50)  NULL,
    [manager_name] varchar(50)  NULL
);
GO

-- Creating table 'suppliers'
CREATE TABLE [dbo].[suppliers] (
    [supplier_id] int IDENTITY(1,1) NOT NULL,
    [supplier_name] varchar(50)  NULL,
    [mobile] decimal(18,0)  NULL,
    [address] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [customer_id] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [PK_customers]
    PRIMARY KEY CLUSTERED ([customer_id] ASC);
GO

-- Creating primary key on [product_type_id] in table 'produc_type'
ALTER TABLE [dbo].[produc_type]
ADD CONSTRAINT [PK_produc_type]
    PRIMARY KEY CLUSTERED ([product_type_id] ASC);
GO

-- Creating primary key on [product_id] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [PK_products]
    PRIMARY KEY CLUSTERED ([product_id] ASC);
GO

-- Creating primary key on [purchase_id] in table 'purchases'
ALTER TABLE [dbo].[purchases]
ADD CONSTRAINT [PK_purchases]
    PRIMARY KEY CLUSTERED ([purchase_id] ASC);
GO

-- Creating primary key on [sale_id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([sale_id] ASC);
GO

-- Creating primary key on [stock_id] in table 'stocks'
ALTER TABLE [dbo].[stocks]
ADD CONSTRAINT [PK_stocks]
    PRIMARY KEY CLUSTERED ([stock_id] ASC);
GO

-- Creating primary key on [store_id] in table 'stores'
ALTER TABLE [dbo].[stores]
ADD CONSTRAINT [PK_stores]
    PRIMARY KEY CLUSTERED ([store_id] ASC);
GO

-- Creating primary key on [supplier_id] in table 'suppliers'
ALTER TABLE [dbo].[suppliers]
ADD CONSTRAINT [PK_suppliers]
    PRIMARY KEY CLUSTERED ([supplier_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [customer_id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_Sale_customer]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[customers]
        ([customer_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sale_customer'
CREATE INDEX [IX_FK_Sale_customer]
ON [dbo].[Sales]
    ([customer_id]);
GO

-- Creating foreign key on [product_type_id] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [FK_product_product]
    FOREIGN KEY ([product_type_id])
    REFERENCES [dbo].[produc_type]
        ([product_type_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_product_product'
CREATE INDEX [IX_FK_product_product]
ON [dbo].[products]
    ([product_type_id]);
GO

-- Creating foreign key on [product_id] in table 'purchases'
ALTER TABLE [dbo].[purchases]
ADD CONSTRAINT [FK_purchase_product]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[products]
        ([product_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_purchase_product'
CREATE INDEX [IX_FK_purchase_product]
ON [dbo].[purchases]
    ([product_id]);
GO

-- Creating foreign key on [product_id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_Sale_product]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[products]
        ([product_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sale_product'
CREATE INDEX [IX_FK_Sale_product]
ON [dbo].[Sales]
    ([product_id]);
GO

-- Creating foreign key on [product_id] in table 'stocks'
ALTER TABLE [dbo].[stocks]
ADD CONSTRAINT [FK_stock_product]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[products]
        ([product_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_stock_product'
CREATE INDEX [IX_FK_stock_product]
ON [dbo].[stocks]
    ([product_id]);
GO

-- Creating foreign key on [store_id] in table 'purchases'
ALTER TABLE [dbo].[purchases]
ADD CONSTRAINT [FK_purchase_store]
    FOREIGN KEY ([store_id])
    REFERENCES [dbo].[stores]
        ([store_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_purchase_store'
CREATE INDEX [IX_FK_purchase_store]
ON [dbo].[purchases]
    ([store_id]);
GO

-- Creating foreign key on [supplier_id] in table 'purchases'
ALTER TABLE [dbo].[purchases]
ADD CONSTRAINT [FK_purchase_supplier]
    FOREIGN KEY ([supplier_id])
    REFERENCES [dbo].[suppliers]
        ([supplier_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_purchase_supplier'
CREATE INDEX [IX_FK_purchase_supplier]
ON [dbo].[purchases]
    ([supplier_id]);
GO

-- Creating foreign key on [store_id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [FK_Sale_store]
    FOREIGN KEY ([store_id])
    REFERENCES [dbo].[stores]
        ([store_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sale_store'
CREATE INDEX [IX_FK_Sale_store]
ON [dbo].[Sales]
    ([store_id]);
GO

-- Creating foreign key on [store_id] in table 'stocks'
ALTER TABLE [dbo].[stocks]
ADD CONSTRAINT [FK_stock_store]
    FOREIGN KEY ([store_id])
    REFERENCES [dbo].[stores]
        ([store_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_stock_store'
CREATE INDEX [IX_FK_stock_store]
ON [dbo].[stocks]
    ([store_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------