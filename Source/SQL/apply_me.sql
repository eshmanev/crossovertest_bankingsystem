USE BankingSystemDB
GO

-- 9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08   SHA256 hash for 'test'
INSERT INTO dbo.Customers(Id, UserName, Email, FirstName, LastName, PasswordHash)
    SELECT 1, 'evgeny.shmanev', 'evgeny.shmanev@aurea.com', 'Evgeny', 'Shmanev', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 2, 'eshmanev', 'eshmanev@gmail.com', 'Eugene', 'Temp', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 3, 'joseph.fill', 'joseph.fill@test.com', 'Joseph', 'Fill', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 4, 'peter.petroff', 'peter.petroff@test.com', 'Peter', 'Petroff', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 5, 'anatoly.green', 'anatoly.green@test.com', 'Anatoly', 'Green', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
GO

INSERT INTO dbo.Accounts(Id, AccountNumber, Currency, Balance)
    -- evgeny.shmanev
    SELECT 1, '451233157789211', 'USD', '602700'
    UNION ALL
    SELECT 2, '451233157789222', 'EUR', '402400'
    UNION ALL
    SELECT 3, '451233157789233', 'JPY', '201200'
    UNION ALL
    -- eshmanev
    SELECT 4, '751233157789211', 'USD', '2700'
    UNION ALL
    SELECT 5, '751233157789222', 'EUR', '2400'
    UNION ALL
    SELECT 6, '751233157789233', 'RUB', '140000'
    UNION ALL
    -- joseph.fill
    SELECT 7, '223145645987955', 'USD', '92000'
    UNION ALL
     -- peter.petroff
    SELECT 9, '645412313447822', 'EUR', '8200'
    UNION ALL
    SELECT 10, '645412313447833', 'RUB', '421000'
    UNION ALL
     -- anatoly.green
    SELECT 11, '445641994546411', 'USD', '71000'
    UNION ALL
    SELECT 12, '445641994546495', 'JPY', '68000'
GO

INSERT INTO dbo.CustomerAccounts(Account_id, Customer_id)
 -- evgeny.shmanev
    SELECT 1, 1
    UNION ALL
    SELECT 2, 1
    UNION ALL
    SELECT 3, 1
    UNION ALL
    -- eshmanev
    SELECT 4, 2
    UNION ALL
    SELECT 5, 2
    UNION ALL
    SELECT 6, 2
    UNION ALL
    -- joseph.fill
    SELECT 7, 3
    UNION ALL
     -- peter.petroff
    SELECT 9, 4
    UNION ALL
    SELECT 10, 4
    UNION ALL
     -- anatoly.green
    SELECT 11, 5
    UNION ALL
    SELECT 12, 5
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

-- merchants
INSERT INTO dbo.Merchants(Id, Name)
    SELECT '5D9168B6-A03B-412E-A5D9-1AC5C5105BA9', 'Kid Toys Online Store'
GO

-- merchants accounts
INSERT INTO dbo.Accounts(Id, AccountNumber, Currency, Balance)
    SELECT 13, '999111111111211', 'USD', '0'
    UNION ALL
    SELECT 14, '999111111111222', 'EUR', '0'
    UNION ALL
    SELECT 15, '999111111111233', 'JPY', '0'
GO

INSERT INTO dbo.MerchantAccounts(Account_id, Merchant_id)
    SELECT 13, '5D9168B6-A03B-412E-A5D9-1AC5C5105BA9'
    UNION ALL
    SELECT 14, '5D9168B6-A03B-412E-A5D9-1AC5C5105BA9'
    UNION ALL
    SELECT 15, '5D9168B6-A03B-412E-A5D9-1AC5C5105BA9'
GO