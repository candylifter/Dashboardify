USE DashBoardify
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ScreenShots')
BEGIN
	 DROP TABLE ScreenShots
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Items')
BEGIN
	 DROP TABLE Items
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DashBoards')
BEGIN
	 DROP TABLE DashBoards
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
BEGIN
	 DROP TABLE Users
END

CREATE TABLE Users
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, --1,1 means start from 1 and increase by 1
	Name VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	DateRegistered DATETIME NOT NULL DEFAULT GETDATE(),
	DateModified DATETIME NOT NULL
)

GO

CREATE TABLE DashBoards
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	Name VARCHAR(255) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DateModified DATETIME NOT NULL
)


CREATE TABLE Items
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	DashBoardId INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Website VARCHAR(255) NOT NULL,
	CheckInterval INT NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	XPath VARCHAR(255) NOT NULL,
	LastChecked DATETIME NOT NULL,
	Created DATETIME NOT NULL DEFAULT GETDATE(),
	Modified DATETIME NOT NULL,
	Content NVARCHAR(MAX) NOT NULL,

)
CREATE TABLE ScreenShots
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ItemId INT NOT NULL,
	ScrnshtURL VARCHAR(255) NOT NULL,
	DateTaken DATETIME NOT NULL DEFAULT GETDATE(),
)




ALTER TABLE DashBoards ADD CONSTRAINT FK_Users_DashBoards FOREIGN KEY (UserID) REFERENCES Users(Id) ON DELETE CASCADE
ALTER TABLE Items ADD CONSTRAINT FK_DashBoards_Items FOREIGN KEY (DashBoardID) REFERENCES DashBoards(Id) ON DELETE CASCADE
ALTER TABLE ScreenShots ADD CONSTRAINT FK_Items_ScreenShots FOREIGN KEY (ItemId) REFERENCES Items(Id) ON DELETE CASCADE



INSERT INTO Users (Name, Password, Email, DateModified) VALUES('Laba diena', 'asd56a+5d6asd', 'email@mail.lt', GETDATE())

DECLARE @User1Id INT = (SELECT Id FROM Users WHERE Name = 'Laba diena')


INSERT INTO DashBoards (Name, UserID, DateModified) VALUES('AUDINES', @User1Id, GETDATE())

DECLARE @DashId INT = (SELECT Id FROM DashBoards WHERE Name ='AUDINES')

INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,LastChecked,Modified,Content) VALUES(@DashId,'Autogidas','http://www.autogidas.lt/automobiliai/',5000,'/html[1]/body[1]/div[1]/div[8]/div[1]/div[2]/a[30]/div[1]',GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,LastChecked,Modified,Content) VALUES(@DashId,'Adform naujienos','http://site.adform.com/',5000,'/html[1]/body[1]/div[1]/section[1]/div[2]/div[1]/div[1]/article[1]',GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,LastChecked,Modified,Content) VALUES(@DashId,'Buzzfeed','https://www.buzzfeed.com/',5000,'/html[1]/body[1]/div[4]/div[1]/div[3]/div[1]/ul[1]/li[1]/div[1]',GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,LastChecked,Modified,Content) VALUES(@DashId,'Reddit frontpage','https://www.reddit.com/',10000,'/html[1]/body[1]/div[4]/div[4]/div[1]/div[1]',GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,LastChecked,Modified,Content) VALUES(@DashId,'Hacker News','https://news.ycombinator.com/',10000,'/html[1]/body[1]/center[1]/table[1]/tbody[1]/tr[3]/td[1]/table[1]/tbody[1]/tr[1]/td[3]',GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,LastChecked,Modified,Content) VALUES(@DashId,'Humble Bundle','https://www.humblebundle.com/',15000,'/html[1]/body[1]/div[1]/div[2]/div[3]/div[1]/div[2]/div[1]',GETDATE(),GETDATE(),'')

DECLARE @ItemId1 INT = (SELECT Id FROM Items WHERE Name = 'Autogidas')

INSERT INTO ScreenShots(ItemId, ScrnshtURL, DateTaken) VALUES (@ItemId1,'http://autogidas-img.dgn.lt/4_21_83702552/audi-80-b3-sedanas-1987.jpg',GETDATE())

