/* =========================================================
   DATABASE: ContractManagement
   Project: Quản lý Hợp đồng/Hồ sơ
   ========================================================= */

-- 1. Tạo Database
IF DB_ID('ContractManagement') IS NULL
BEGIN
    CREATE DATABASE ContractManagement;
END
GO

USE ContractManagement;
GO


/* =========================================================
   2. XÓA BẢNG CŨ NẾU ĐÃ TỒN TẠI
   ========================================================= */

IF OBJECT_ID('dbo.Documents', 'U') IS NOT NULL
    DROP TABLE dbo.Documents;

IF OBJECT_ID('dbo.Contracts', 'U') IS NOT NULL
    DROP TABLE dbo.Contracts;

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE dbo.Users;

IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL
    DROP TABLE dbo.Roles;
GO


/* =========================================================
   3. BẢNG ROLES
   ========================================================= */

CREATE TABLE Roles
(
    RoleId INT IDENTITY(1,1) NOT NULL,
    RoleName NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Roles
        PRIMARY KEY (RoleId),

    CONSTRAINT UQ_Roles_RoleName
        UNIQUE (RoleName)
);
GO


/* =========================================================
   4. BẢNG USERS
   ========================================================= */

CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL,
    RoleId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_Users
        PRIMARY KEY (UserId),

    CONSTRAINT UQ_Users_Username
        UNIQUE (Username),

    CONSTRAINT UQ_Users_Email
        UNIQUE (Email),

    CONSTRAINT FK_Users_Roles
        FOREIGN KEY (RoleId)
        REFERENCES Roles(RoleId)
);
GO


/* =========================================================
   5. BẢNG CONTRACTS
   ========================================================= */

CREATE TABLE Contracts
(
    ContractId INT IDENTITY(1,1) NOT NULL,
    ContractCode NVARCHAR(50) NOT NULL,
    ContractName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedBy INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT PK_Contracts
        PRIMARY KEY (ContractId),

    CONSTRAINT UQ_Contracts_ContractCode
        UNIQUE (ContractCode),

    CONSTRAINT FK_Contracts_Users
        FOREIGN KEY (CreatedBy)
        REFERENCES Users(UserId)
);
GO


/* =========================================================
   6. BẢNG DOCUMENTS
   ========================================================= */

CREATE TABLE Documents
(
    DocumentId INT IDENTITY(1,1) NOT NULL,
    ContractId INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    FileType NVARCHAR(50) NULL,
    FileSize BIGINT NULL,
    UploadedBy INT NOT NULL,
    UploadedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT PK_Documents
        PRIMARY KEY (DocumentId),

    CONSTRAINT FK_Documents_Contracts
        FOREIGN KEY (ContractId)
        REFERENCES Contracts(ContractId),

    CONSTRAINT FK_Documents_Users
        FOREIGN KEY (UploadedBy)
        REFERENCES Users(UserId)
);
GO


/* =========================================================
   7. THÊM DỮ LIỆU ROLE MẪU
   ========================================================= */

INSERT INTO Roles (RoleName)
VALUES
(N'Admin'),
(N'Nhân viên'),
(N'Quản lý');
GO


/* =========================================================
   8. THÊM USER MẪU
   ========================================================= */

INSERT INTO Users
(
    Username,
    PasswordHash,
    FullName,
    Email,
    RoleId
)
VALUES
(
    N'admin',
    N'123456',
    N'Quản trị viên',
    N'admin@example.com',
    1
),
(
    N'nhanvien01',
    N'123456',
    N'Nguyễn Văn A',
    N'nhanvien01@example.com',
    2
),
(
    N'quanly01',
    N'123456',
    N'Trần Văn B',
    N'quanly01@example.com',
    3
);
GO


/* =========================================================
   9. THÊM HỢP ĐỒNG MẪU
   ========================================================= */

INSERT INTO Contracts
(
    ContractCode,
    ContractName,
    Description,
    StartDate,
    EndDate,
    Status,
    CreatedBy
)
VALUES
(
    N'HD001',
    N'Hợp đồng cung cấp dịch vụ CNTT',
    N'Hợp đồng dịch vụ công nghệ thông tin',
    '2026-09-01',
    '2027-09-01',
    N'Đang hiệu lực',
    1
),
(
    N'HD002',
    N'Hợp đồng bảo trì hệ thống',
    N'Hợp đồng bảo trì hệ thống phần mềm',
    '2026-09-15',
    '2027-09-15',
    N'Đang hiệu lực',
    2
);
GO


/* =========================================================
   10. THÊM DOCUMENT MẪU
   ========================================================= */

INSERT INTO Documents
(
    ContractId,
    FileName,
    FilePath,
    FileType,
    FileSize,
    UploadedBy
)
VALUES
(
    1,
    N'hopdong_HD001.pdf',
    N'/documents/hopdong_HD001.pdf',
    N'application/pdf',
    102400,
    1
),
(
    1,
    N'phuluc_HD001.pdf',
    N'/documents/phuluc_HD001.pdf',
    N'application/pdf',
    204800,
    2
),
(
    2,
    N'hopdong_HD002.pdf',
    N'/documents/hopdong_HD002.pdf',
    N'application/pdf',
    153600,
    3
);
GO


/* =========================================================
   11. KIỂM TRA DỮ LIỆU
   ========================================================= */

SELECT * FROM Roles;
SELECT * FROM Users;
SELECT * FROM Contracts;
SELECT * FROM Documents;
GO