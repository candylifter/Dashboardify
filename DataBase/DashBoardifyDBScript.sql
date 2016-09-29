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

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserSession')
BEGIN
	 DROP TABLE UserSession
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
BEGIN
	 DROP TABLE Users
END



CREATE TABLE Users
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, --1,1 means start from 1 and increase by 1
	Name NVARCHAR(255) NOT NULL,
	Password NVARCHAR(255) NOT NULL,
	Email NVARCHAR(255) NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	DateRegistered DATETIME NOT NULL DEFAULT GETDATE(),
	DateModified DATETIME NOT NULL DEFAULT GETDATE(),
)

GO

CREATE TABLE DashBoards
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	Name NVARCHAR(255) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DateModified DATETIME NOT NULL
)


CREATE TABLE Items
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	DashBoardId INT NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	Website NVARCHAR(255) NOT NULL,
	CheckInterval INT NOT NULL,
	IsActive BIT NOT NULL DEFAULT 1,
	XPath NVARCHAR(255) NOT NULL,
	CSS NVARCHAR(255) NOT NULL,
	NotifyByEmail BIT NOT NULL DEFAULT 1,
	Failed INT NOT NULL DEFAULT 0,
	LastChecked DATETIME NOT NULL,
	Created DATETIME NOT NULL DEFAULT GETDATE(),
	Modified DATETIME NOT NULL,
	Content NVARCHAR(MAX) NOT NULL,

)
CREATE TABLE ScreenShots
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ItemId INT NOT NULL,
	ScrnshtURL NVARCHAR(255) NOT NULL,
	DateTaken DATETIME NOT NULL DEFAULT GETDATE(),
)
CREATE TABLE UserSession
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Ticket NVARCHAR(255) NOT NULL,
	UserId INT NOT NULL,
	Expires DATETIME NOT NULL,
)




ALTER TABLE DashBoards ADD CONSTRAINT FK_Users_DashBoards FOREIGN KEY (UserID) REFERENCES Users(Id) ON DELETE CASCADE
ALTER TABLE Items ADD CONSTRAINT FK_DashBoards_Items FOREIGN KEY (DashBoardID) REFERENCES DashBoards(Id) ON DELETE CASCADE
ALTER TABLE ScreenShots ADD CONSTRAINT FK_Items_ScreenShots FOREIGN KEY (ItemId) REFERENCES Items(Id) ON DELETE CASCADE
ALTER TABLE UserSession ADD CONSTRAINT FK_Users_UserSession FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE



INSERT INTO Users (Name, Password, Email, DateModified) VALUES('Laba diena', 'asd56a+5d6asd', 'email@mail.lt', GETDATE())

DECLARE @User1Id INT = (SELECT Id FROM Users WHERE Name = 'Laba diena')


INSERT INTO DashBoards (Name, UserID, DateModified) VALUES('AUDINES', @User1Id, GETDATE())

DECLARE @DashId INT = (SELECT Id FROM DashBoards WHERE Name ='AUDINES')

INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,CSS,LastChecked,Modified,Content) VALUES(@DashId,'Autogidas','http://www.autogidas.lt/automobiliai/',5000,'/html[1]/body[1]/div[1]/div[8]/div[1]/div[2]/a[30]/div[1]', 'body > div > div:nth-child(8) > div > div.all-ads-block > a:nth-child(1) > div', GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,CSS,LastChecked,Modified,Content) VALUES(@DashId,'Adform naujienos','http://site.adform.com/',5000,'/html[1]/body[1]/div[1]/section[1]/div[2]/div[1]/div[1]/article[1]', 'body > div.sticky-wrap > section > div.custom-blocks > div > div > article:nth-child(1)', GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,CSS,LastChecked,Modified,Content) VALUES(@DashId,'Buzzfeed','https://www.buzzfeed.com/',5000,'/html[1]/body[1]/div[4]/div[1]/div[3]/div[1]/ul[1]/li[1]/div[1]', 'body > div.hp-layout > div > div.row.wrapper-3col > div.col1 > ul:nth-child(1) > li:nth-child(1) > div', GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,CSS,LastChecked,Modified,Content) VALUES(@DashId,'Reddit frontpage','https://www.reddit.com/',10000,'/html[1]/body[1]/div[4]/div[4]/div[1]/div[1]', '#thing_t3_52idnz', GETDATE(),GETDATE(),'')
INSERT INTO Items(DashBoardID, Name,Website, CheckInterval,XPath,CSS,LastChecked,Modified,Content) VALUES(@DashId,'Hacker News','https://news.ycombinator.com/',10000,'/html[1]/body[1]/center[1]/table[1]/tbody[1]/tr[3]/td[1]/table[1]/tbody[1]/tr[1]/td[3]', '#\31 2485650', GETDATE(),GETDATE(),'')

DECLARE @ItemId1 INT = (SELECT Id FROM Items WHERE Name = 'Autogidas')

INSERT INTO ScreenShots(ItemId, ScrnshtURL, DateTaken) VALUES (@ItemId1,'http://autogidas-img.dgn.lt/4_21_83702552/audi-80-b3-sedanas-1987.jpg',GETDATE())
