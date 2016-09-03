CREATE DATABASE DashBoardify;

GO
 USE DashBoardify
GO

CREATE LOGIN [DashboardifyUser]
WITH PASSWORD = '123456';

CREATE USER [DashboardifyUser] FOR LOGIN [DashboardifyUser];
ALTER ROLE [db_owner] ADD MEMBER [DashboardifyUser];
GO