
SELECT Users.ID, Users.Name,DashBoards.Id as DASHID, Items.Name, Items.XPath FROM
Users JOIN DashBoards
ON DashBoards.UserId = Users.ID
JOIN Items ON Items.DashBoardId=DashBoards.Id;