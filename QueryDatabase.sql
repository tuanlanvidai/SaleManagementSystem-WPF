CREATE DATABASE SaleCallCenter
GO
USE SaleCallCenter
GO

-- Bảng danh mục vai trò người dùng
CREATE TABLE Tbl_UserRoles (
    role_id INT IDENTITY(1,1) CONSTRAINT pk_user_roles PRIMARY KEY,
    role_name NVARCHAR(50) CONSTRAINT ck_default_role_name DEFAULT 'untitle'
)
GO

INSERT INTO Tbl_UserRoles (role_name) VALUES ('Admin'), ('Manager'), ('Warehouse Staff')
GO

-- Bảng người dùng
CREATE TABLE Tbl_Users (
    user_id INT IDENTITY(1,1) CONSTRAINT pk_users PRIMARY KEY,
    username NVARCHAR(100) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    role_id INT CONSTRAINT fk_users_role FOREIGN KEY REFERENCES Tbl_UserRoles(role_id),
    status INT CONSTRAINT ck_default_user_status DEFAULT 1
)
GO

INSERT INTO Tbl_Users (username, password, role_id, status) VALUES
('admin', 'admin', 1, 1),
('manager', 'manager', 2, 1),
('staff', 'staff', 3, 1)
GO

-- Bảng khách hàng
CREATE TABLE Tbl_Customers (
    customer_id INT IDENTITY(1,1) CONSTRAINT pk_customers PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    phone NVARCHAR(15),
    email NVARCHAR(100),
    address NVARCHAR(255),
    needs NVARCHAR(255)
)
GO

INSERT INTO Tbl_Customers (name, phone, email, address, needs) VALUES
('Lan Truong', '123456789', 'john@example.com', '123 Main St', 'Electronics'),
('Will Smith', '987654321', 'jane@example.com', '456 Elm St', 'Home Appliances'),
('Edison', '567890123', 'bob@example.com', '789 Oak St', 'Office Supplies')
GO

-- Bảng danh mục trạng thái đơn hàng
CREATE TABLE Tbl_OrderStatus (
    status_id INT IDENTITY(1,1) CONSTRAINT pk_order_status PRIMARY KEY,
    status_name NVARCHAR(50) NOT NULL
)
GO

INSERT INTO Tbl_OrderStatus (status_name) VALUES
('Delivered'), ('Pending'), ('Cancelled'), ('Awaiting Stock')
GO

-- Bảng đơn hàng
CREATE TABLE Tbl_Orders (
    order_id INT IDENTITY(1,1) CONSTRAINT pk_orders PRIMARY KEY,
    customer_id INT CONSTRAINT fk_orders_customer FOREIGN KEY REFERENCES Tbl_Customers(customer_id),
    created_by INT CONSTRAINT fk_orders_user FOREIGN KEY REFERENCES Tbl_Users(user_id),
    status_id INT CONSTRAINT fk_orders_status FOREIGN KEY REFERENCES Tbl_OrderStatus(status_id),
    created_date DATETIME CONSTRAINT ck_default_created_date DEFAULT GETDATE(),
    total_amount DECIMAL(18,2) CONSTRAINT ck_default_total_amount DEFAULT 0
)
GO

INSERT INTO Tbl_Orders (customer_id, created_by, status_id, created_date, total_amount) VALUES
(1, 2, 2, '2025-02-08', 200.00),
(2, 2, 1, '2025-02-07', 150.00),
(3, 2, 2, '2025-02-08', 500.00)
GO

-- Bảng danh mục sản phẩm
CREATE TABLE Tbl_ProductCategories (
    category_id INT IDENTITY(1,1) CONSTRAINT pk_product_categories PRIMARY KEY,
    category_name NVARCHAR(100) CONSTRAINT ck_default_category_name DEFAULT 'untitle'
)
GO

INSERT INTO Tbl_ProductCategories (category_name) VALUES
('Electronics'), ('Home Appliances'), ('Office Supplies')
GO

-- Bảng sản phẩm
CREATE TABLE Tbl_Products (
    product_id INT IDENTITY(1,1) CONSTRAINT pk_products PRIMARY KEY,
    product_name NVARCHAR(100) CONSTRAINT ck_default_product_name DEFAULT 'untitle',
    category_id INT CONSTRAINT fk_products_category FOREIGN KEY REFERENCES Tbl_ProductCategories(category_id),
    stock INT CONSTRAINT ck_default_stock DEFAULT 0,
    unit_price DECIMAL(18,2) CONSTRAINT ck_default_unit_price DEFAULT 0
)
GO

INSERT INTO Tbl_Products (product_name, category_id, stock, unit_price) VALUES
('Laptop', 1, 10, 23000000),
('Microwave', 2, 15, 5000000),
('Printer', 3, 5, 2700000)
GO

-- Bảng chi tiết đơn hàng
CREATE TABLE Tbl_OrderDetails (
    detail_id INT IDENTITY(1,1) CONSTRAINT pk_order_details PRIMARY KEY,
    order_id INT CONSTRAINT fk_order_details_order FOREIGN KEY REFERENCES Tbl_Orders(order_id),
    product_id INT CONSTRAINT fk_order_details_product FOREIGN KEY REFERENCES Tbl_Products(product_id),
    quantity INT NOT NULL,
    unit_price DECIMAL(18,2) NOT NULL
)
GO

INSERT INTO Tbl_OrderDetails (order_id, product_id, quantity, unit_price) VALUES
(1, 1, 2, 23000000),
(1, 2, 1, 5000000),
(2, 2, 3, 5000000),
(3, 3, 1, 2700000)
GO

-- Bảng danh mục loại thống kê
CREATE TABLE Tbl_StatisticTypes (
    type_id INT IDENTITY(1,1) CONSTRAINT pk_statistic_types PRIMARY KEY,
    type_name NVARCHAR(100) NOT NULL
)
GO

INSERT INTO Tbl_StatisticTypes (type_name) VALUES
('Revenue'), ('Product Sales'), ('Employee Sales'), ('Stock Report'), ('Total Orders')
GO

-- Bảng thống kê
CREATE TABLE Tbl_Statistics (
    statistic_id INT IDENTITY(1,1) CONSTRAINT pk_statistics PRIMARY KEY,
    type_id INT CONSTRAINT fk_statistics_type FOREIGN KEY REFERENCES Tbl_StatisticTypes(type_id),
    statistic_target NVARCHAR(100) NULL,
    target_id INT NULL,
    time_period NVARCHAR(20),
    start_date DATE,
    end_date DATE,
    metric_name NVARCHAR(100),
    value DECIMAL(18,2),
    last_updated DATETIME CONSTRAINT ck_default_last_updated DEFAULT GETDATE()
)
GO

-- Trigger cập nhật doanh thu
CREATE TRIGGER trg_UpdateMonthlyRevenue
ON Tbl_Orders
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @monthStart DATE = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
    DECLARE @monthEnd DATE = EOMONTH(GETDATE());
    DECLARE @revenue DECIMAL(18, 2);

    -- Tính tổng doanh thu
    SELECT @revenue = SUM(total_amount)
    FROM Tbl_Orders
    WHERE status_id = 1 AND created_date BETWEEN @monthStart AND @monthEnd;

    -- Kiểm tra xem bản ghi đã tồn tại chưa
    IF EXISTS (
        SELECT 1 FROM Tbl_Statistics 
        WHERE type_id = 1 AND time_period = 'Monthly' AND start_date = @monthStart
    )
    BEGIN
        -- Cập nhật giá trị nếu bản ghi đã tồn tại
        UPDATE Tbl_Statistics
        SET value = @revenue, last_updated = GETDATE()
        WHERE type_id = 1 AND time_period = 'Monthly' AND start_date = @monthStart;
    END
    ELSE
    BEGIN
        -- Thêm bản ghi mới nếu chưa tồn tại
        INSERT INTO Tbl_Statistics (type_id, time_period, start_date, end_date, metric_name, value, last_updated)
        VALUES (1, 'Monthly', @monthStart, @monthEnd, 'Monthly Revenue', @revenue, GETDATE());
    END
END
GO

-- Trigger cập nhật số lượng sản phẩm bán ra
CREATE TRIGGER trg_UpdateProductSales
ON Tbl_OrderDetails
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @monthStart DATE = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
    DECLARE @monthEnd DATE = EOMONTH(GETDATE());

    -- Xử lý từng sản phẩm trong `inserted`
    MERGE Tbl_Statistics AS target
    USING (
        SELECT 
            2 AS type_id,
            'Product' AS statistic_target,
            product_id AS target_id,
            'Monthly' AS time_period,
            @monthStart AS start_date,
            @monthEnd AS end_date,
            'Monthly Product Sales' AS metric_name,
            SUM(quantity) AS value
        FROM inserted
        GROUP BY product_id
    ) AS source
    ON target.type_id = source.type_id 
       AND target.statistic_target = source.statistic_target 
       AND target.target_id = source.target_id 
       AND target.time_period = source.time_period 
       AND target.start_date = source.start_date
    WHEN MATCHED THEN
        UPDATE SET value = target.value + source.value, last_updated = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (type_id, statistic_target, target_id, time_period, start_date, end_date, metric_name, value, last_updated)
        VALUES (source.type_id, source.statistic_target, source.target_id, source.time_period, source.start_date, source.end_date, source.metric_name, source.value, GETDATE());
END
GO

-- Trigger cập nhật số lượng đơn hàng mỗi nhân viên
CREATE TRIGGER trg_UpdateEmployeeOrderCount
ON Tbl_Orders
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @monthStart DATE = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
    DECLARE @monthEnd DATE = EOMONTH(GETDATE());

    -- Xử lý từng nhân viên trong `inserted`
    MERGE Tbl_Statistics AS target
    USING (
        SELECT 
            3 AS type_id,
            'Employee' AS statistic_target,
            created_by AS target_id,
            'Monthly' AS time_period,
            @monthStart AS start_date,
            @monthEnd AS end_date,
            'Monthly Employee Order Count' AS metric_name,
            COUNT(*) AS value
        FROM inserted
        GROUP BY created_by
    ) AS source
    ON target.type_id = source.type_id 
       AND target.statistic_target = source.statistic_target 
       AND target.target_id = source.target_id 
       AND target.time_period = source.time_period 
       AND target.start_date = source.start_date
    WHEN MATCHED THEN
        UPDATE SET value = target.value + source.value, last_updated = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (type_id, statistic_target, target_id, time_period, start_date, end_date, metric_name, value, last_updated)
        VALUES (source.type_id, source.statistic_target, source.target_id, source.time_period, source.start_date, source.end_date, source.metric_name, source.value, GETDATE());
END
GO