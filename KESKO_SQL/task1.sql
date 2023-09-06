select * from Accounts, Contracts
where (Contracts.Id = Accounts.Contract_Id)
AND DATEDIFF(day, Contracts.DateFrom, Accounts.DateTimeFrom) < 0
OR (Contracts.Id = Accounts.Contract_Id)
AND DATEDIFF(day, Accounts.DateTimeTo, Contracts.DateTo) < 0