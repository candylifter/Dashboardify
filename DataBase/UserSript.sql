CREATE DATABASE DashBoardify;

GO
 USE DashBoardify
GO

CREATE LOGIN [DashboardifyUser]
WITH PASSWORD = 'xc6AjzBx6QA2pKUU';

CREATE USER [DashboardifyUser] FOR LOGIN [DashboardifyUser];
ALTER ROLE [db_owner] ADD MEMBER [DashboardifyUser];
GO