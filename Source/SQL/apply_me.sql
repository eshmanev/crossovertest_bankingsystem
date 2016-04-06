USE BankingSystemDB
GO

-- 9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08   SHA256 hash for 'test'
INSERT INTO dbo.Customers(Id, UserName, Email, FirstName, LastName, PasswordHash)
    SELECT 1, 'bill.gates', 'bill.gates@test.com', 'Bill', 'Gates', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 2, 'ivan.ivanov', 'ivan.ivanov@test.com', 'Ivan', 'Ivanov', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 3, 'joseph.fill', 'joseph.fill@test.com', 'Joseph', 'Fill', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 4, 'peter.petroff', 'peter.petroff@test.com', 'Peter', 'Petroff', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
    UNION ALL
    SELECT 5, 'anatoly.green', 'anatoly.green@test.com', 'Anatoly', 'Green', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
GO

INSERT INTO dbo.Accounts(Id, AccountNumber, Currency, Balance, Customer_id)
    -- bill.gates
    SELECT 1, '451233157789211', 'USD', '602700', 1
    UNION ALL
    SELECT 2, '451233157789222', 'EUR', '402400', 1
    UNION ALL
    SELECT 3, '451233157789233', 'JPY', '201200', 1
    UNION ALL
    -- ivan.ivanov
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
GO