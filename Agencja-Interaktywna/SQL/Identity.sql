DELETE FROM Client WHERE ClientId > 0

DELETE FROM Employee WHERE EmployeeId > 0

DELETE FROM Programmer WHERE EmployeeId > 0

DELETE FROM Positioner WHERE EmployeeId > 0

DELETE FROM Tester WHERE EmployeeId > 0

DELETE FROM Graphician WHERE EmployeeId > 0

DELETE FROM Boss WHERE EmployeeId > 0

DELETE FROM Person WHERE PersonId > 0

DBCC CHECKIDENT ('Person', RESEED, 0)

GO