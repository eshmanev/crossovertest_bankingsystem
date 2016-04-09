USE master
GO

CREATE DATABASE BankingSystemDB
GO
ALTER DATABASE BankingSystemDB SET COMPATIBILITY_LEVEL = 120
GO
CREATE LOGIN [BankingUser] WITH PASSWORD='test', DEFAULT_DATABASE=[BankingSystemDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE BankingSystemDB
GO

CREATE USER [BankingUser] FOR LOGIN [BankingUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [BankingUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [BankingUser]
GO

CREATE TABLE [dbo].[Accounts]
(
	[Id] [int] NOT NULL,
	[AccountNumber] [nvarchar](255) NULL,
	[Currency] [nvarchar](255) NOT NULL,
	[Balance] [decimal](19, 5) NOT NULL,
	[CustomerId] [int] NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    UNIQUE NONCLUSTERED ([AccountNumber] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BankBalance]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalBalance] [decimal](19, 5) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BankCards]
(
	[Id] [int] NOT NULL,
	[CardHolder] [nvarchar](255) NOT NULL,
	[CardNumber] [nvarchar](255) NOT NULL,
	[CsvCode] [nvarchar](255) NOT NULL,
	[ExpirationMonth] [int] NOT NULL,
	[ExpirationYear] [int] NOT NULL,
	[PinCode] [nvarchar](255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Customers]
(
	[Id] [int] NOT NULL,
	[CustomerType] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PasswordHash] [nvarchar](255) NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[MerchantId] [uniqueidentifier] NULL,
	[MerchantName] [nvarchar](255) NULL,
	[ContactPerson] [nvarchar](255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    UNIQUE NONCLUSTERED ([UserName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[DeliveredEmails]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipientAddress] [nvarchar](255) NOT NULL,
	[DeliveredDateTime] [datetime] NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[HiLo]
(
	[NextHi] [int] NULL,
	[Entity] [varchar](128) NULL
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_HiLo_Entity] ON [dbo].[HiLo]
(
	[Entity] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Journals]
(
	[Id] [int] NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CustomerId] [int] NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LoginInfos]
(
	[LoginKey] [nvarchar](255) NOT NULL,
	[ProviderName] [nvarchar](255) NOT NULL,
	[CustomerId] [int] NULL,
    PRIMARY KEY CLUSTERED ([LoginKey] ASC, [ProviderName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ScheduledEmails]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipientAddress] [nvarchar](255) NOT NULL,
	[ScheduledDateTime] [datetime] NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](255) NOT NULL,
	[FailureReason] [nvarchar](255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Accounts]  WITH CHECK 
    ADD  CONSTRAINT [FK_Account_Customer] FOREIGN KEY([CustomerId])
    REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[LoginInfos]  WITH CHECK 
    ADD  CONSTRAINT [FK_LoginInfo_Customer] FOREIGN KEY([CustomerId])
    REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Journals]  
    WITH CHECK ADD  CONSTRAINT [FK_Journal_Customer] FOREIGN KEY([CustomerId])
    REFERENCES [dbo].[Customers] ([Id])
GO

-- 9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08   SHA256 hash for 'test'
INSERT INTO dbo.Customers(Id, CustomerType, UserName, Email, FirstName, LastName, PasswordHash, MerchantId, MerchantName, ContactPerson)
    SELECT 1, 'individual', 'evgeny.shmanev', 'evgeny.shmanev@aurea.com', 'Evgeny', 'Shmanev', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', null, null, null
    UNION ALL
    SELECT 2, 'individual', 'eshmanev', 'eshmanev@gmail.com', 'Eugene', 'Temp', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', null, null, null
    UNION ALL
    SELECT 3, 'individual', 'joseph.fill', 'joseph.fill@test.com', 'Joseph', 'Fill', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', null, null, null
    UNION ALL
    SELECT 4, 'individual', 'peter.petroff', 'peter.petroff@test.com', 'Peter', 'Petroff', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', null, null, null
    UNION ALL
    SELECT 5, 'individual', 'anatoly.green', 'anatoly.green@test.com', 'Anatoly', 'Green', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', null, null, null
    UNION ALL
    SELECT 6, 'merchant', 'merchant', 'shmanev@evriqum.com', null, null, '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', '5D9168B6-A03B-412E-A5D9-1AC5C5105BA9', 'Kid Toys Online Store', 'Ivan Ivanov'
GO

INSERT INTO dbo.Accounts(Id, AccountNumber, Currency, Balance, CustomerId)
    -- evgeny.shmanev
    SELECT 1, '451233157789211', 'USD', '602700', 1
    UNION ALL
    SELECT 2, '451233157789222', 'EUR', '402400', 1
    UNION ALL
    SELECT 3, '451233157789233', 'JPY', '201200', 1
    UNION ALL
    -- eshmanev
    SELECT 4, '751233157789211', 'USD', '2700', 2
    UNION ALL
    SELECT 5, '751233157789222', 'EUR', '2400', 2
    UNION ALL
    SELECT 6, '751233157789233', 'RUB', '140000', 2
    UNION ALL
    -- joseph.fill
    SELECT 7, '223145645987955', 'USD', '92000', 3
    UNION ALL
     -- peter.petroff
    SELECT 9, '645412313447822', 'EUR', '8200', 4
    UNION ALL
    SELECT 10, '645412313447833', 'RUB', '421000', 4
    UNION ALL
     -- anatoly.green
    SELECT 11, '445641994546411', 'USD', '71000', 5
    UNION ALL
    SELECT 12, '445641994546495', 'JPY', '68000', 5
    -- merchant
    UNION ALL
    SELECT 13, '999111111111211', 'USD', '0', 6
    UNION ALL
    SELECT 14, '999111111111222', 'EUR', '0', 6
    UNION ALL
    SELECT 15, '999111111111233', 'JPY', '0', 6
GO

INSERT INTO dbo.BankCards(Id, CardHolder, CardNumber, CsvCode, ExpirationMonth, ExpirationYear, PinCode)
    -- evgeny.shmanev
    SELECT 1, 'Evgeny Shmanev', '1111222233331111', '123', 1, 2020, '0000'
    UNION ALL
    SELECT 2, 'Evgeny Shmanev', '2111222233331111', '123', 1, 2020, '0000'
    UNION ALL
    SELECT 3, 'Evgeny Shmanev', '3111222233331111', '123', 1, 2020, '0000'
    UNION ALL
    -- eshmanev
    SELECT 4, 'Eugene Temp', '1111222233332222', '123', 1, 2020, '0000'
    UNION ALL
    SELECT 5, 'Eugene Temp', '2111222233332222', '123', 1, 2020, '0000'
    UNION ALL
    SELECT 6, 'Eugene Temp', '3111222233332222', '123', 1, 2020, '0000'
    UNION ALL
    -- joseph.fill
    SELECT 7, 'Joseph Fill', '1111222233333333', '123', 1, 2020, '0000'
    UNION ALL
     -- peter.petroff
    SELECT 9, 'Peter Petroff', '1111222233334444', '123', 1, 2020, '0000'
    UNION ALL
    SELECT 10, 'Peter Petroff', '2111222233334444', '123', 1, 2020, '0000'
    UNION ALL
     -- anatoly.green
    SELECT 11, 'Anatoly Green', '1111222233335555', '123', 1, 2020, '0000'
    UNION ALL
    SELECT 12, 'Anatoly Green', '2111222233335555', '123', 1, 2020, '0000'
GO

INSERT INTO dbo.HiLo(NextHi, Entity)
    SELECT 2, 'BankingSystem.DataTier.Entities.Account' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.BankBalance' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.BankCard' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.Individual' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.Merchant'
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.CustomerBase' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.DeliveredEmail' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.Journal' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.LoginInfo' 
    UNION ALL
    SELECT 2, 'BankingSystem.DataTier.Entities.ScheduledEmail'
GO