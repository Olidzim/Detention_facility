USE [Detention_facility]
GO

CREATE PROC [AddDetaineeToDetention]
@DetentionID int,
@DetaineeID int
AS
INSERT INTO DetentionsOfDetainees(DetentionID, DetaineeID)
VALUES (@DetentionID, @DetaineeID) 
GO

CREATE PROC [DeleteDelivery]
@DeliveryID int
AS
DBCC CHECKIDENT (DeliveriesOfDetainees, RESEED, 1)
DELETE FROM DeliveriesOfDetainees WHERE DeliveryID = @DeliveryID
GO

CREATE PROC [DeleteDetainee]
@DetaineeID int
AS
DBCC CHECKIDENT (Detainees, RESEED, 1)
DELETE FROM Detainees WHERE DetaineeID = @DetaineeID
GO

CREATE PROC [DeleteDetention]
@DetentionID int
AS
DBCC CHECKIDENT (Detentions, RESEED, 1)
DELETE FROM Detentions WHERE DetentionID = @DetentionID
GO

CREATE PROC [DeleteEmployee]
@EmployeeID int
AS
DBCC CHECKIDENT (Employees, RESEED, 1)
DELETE FROM Employees WHERE EmployeeID = @EmployeeID
GO

CREATE PROC [DeleteRelease]
@ReleaseID int
AS
DBCC CHECKIDENT (ReleasesOfDetainees, RESEED, 1)
DELETE FROM ReleasesOfDetainees WHERE ReleaseID = @ReleaseID
GO

Create proc [GetDeliveriesOfDetainees]
AS
Select * FROM DeliveriesOfDetainees
GO

CREATE proc [GetDeliveryByID]
@DeliveryID int
AS
Select * From DeliveriesOfDetainees where DeliveryID = @DeliveryID
GO

CREATE proc [GetDetaineeByID]
@DetaineeID int
AS
Select * From Detainees where DetaineeID = @DetaineeID
GO

Create proc [GetDetainees]
AS
Select * FROM Detainees
GO

CREATE Proc [GetDetaineesByDetentionID]
@DetentionID int
AS
Select * From Detainees Inner Join DetentionsOfDetainees ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID Where DetentionsOfDetainees.DetentionID = @DetentionID
GO

CREATE PROC [GetDetentionByDate]
@DetentionDate datetime
AS
SELECT DetentionsOfDetainees.DetentionID, Detainees.DetaineeID, Detainees.FirstName, Detainees.LastName, Detainees.Patronymic, Detentions.DetentionDate
FROM Detainees 
Inner Join DetentionsOfDetainees 
ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID 
Inner Join Detentions 
ON DetentionsOfDetainees.DetentionID = Detentions.DetentionID 
WHERE Detentions.DetentionDate = @DetentionDate
GO


CREATE proc [GetDetentionByID]
@DetentionID int
AS
Select * From dETENTIONS where DetentionId = @DetentionID
GO

CREATE PROC [GetDetentionByLastName]
@LastName nvarchar(50)
AS
SELECT DetentionsOfDetainees.DetentionID, Detainees.DetaineeID, Detainees.FirstName, Detainees.LastName, Detainees.Patronymic, Detentions.DetentionDate
FROM Detainees 
Inner Join DetentionsOfDetainees 
ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID 
Inner Join Detentions 
ON DetentionsOfDetainees.DetentionID = Detentions.DetentionID 
WHERE Detainees.LastName = @LastName
GO

CREATE PROC [GetDetentionByResidencePlace]
@ResidencePlace nvarchar(50)
AS
SELECT DetentionsOfDetainees.DetentionID, Detainees.DetaineeID, Detainees.FirstName, Detainees.LastName, Detainees.Patronymic, Detentions.DetentionDate
FROM Detainees 
Inner Join DetentionsOfDetainees 
ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID 
Inner Join Detentions 
ON DetentionsOfDetainees.DetentionID = Detentions.DetentionID 
WHERE Detainees.ResidencePlace = @ResidencePlace
GO

Create proc [GetDetentions]
AS
Select * FROM Detentions
GO

CREATE proc [GetEmployeeByID]
@EmployeeID int
AS
Select * From Employees where EmployeeId = @EmployeeID
GO

Create proc [GetEmployees]
AS
Select * FROM Employees
GO

CREATE proc [GetReleaseByID]
@ReleaseID int
AS
Select * From ReleasesOfDetainees where ReleaseID = @ReleaseID
GO

Create proc [GetReleasesOfDetainees]
AS
Select * FROM ReleasesOfDetainees
GO

CREATE proc [InsertDelivery]
	@DetaineeID int,
	@DetentionID int,
	@PlaceAddress nvarchar(50),
	@DeliveredByEmployeeID int,
	@DeliveryDate datetime
AS
INSERT INTO DeliveriesOfDetainees 
	( 
	DetaineeID,
	DetentionID,
	PlaceAddress,
	DeliveredByEmployeeID,
	DeliveryDate 
	)
VALUES 
	( 
	@DetaineeID,
	@DetentionID,
	@PlaceAddress,
	@DeliveredByEmployeeID,
	@DeliveryDate 
	)
GO

CREATE PROC [InsertDetainee]
	@FirstName nvarchar(50),
	@lastName nvarchar(50),
	@Patronymic nvarchar(50),
	@Birthdate datetime,
	@MaritalStatus nvarchar(50),
	@Job nvarchar(50),
	@MobilePhoneNumber nvarchar(50),
	@HomePhoneNumber nvarchar(50),
	@Photo image = NULL,
	@ExtraInfo  nvarchar(50) = NULL,
	@ResidencePlace  nvarchar(50)
AS
INSERT INTO Detainees
	(
	FirstName,
	LastName,
	Patronymic,
	BirthDate,
	MaritalStatus,
	Job,
	MobilePhoneNumber,
	HomePhoneNumber,
	Photo,
	ExtraInfo,
	ResidencePlace
	)
VALUES 
	(
	@FirstName,
	@lastName,
	@Patronymic,
	@Birthdate,
	@MaritalStatus,
	@Job,
	@MobilePhoneNumber,
	@HomePhoneNumber,
	@Photo,
	@ExtraInfo ,
	@ResidencePlace
	)
GO

CREATE PROC [InsertDetention]	
	@DetentionDate datetime,
	@DetainedByEmployeeID int
AS
INSERT INTO Detentions
	(
	DetentionDate, 
	DetainedByEmployeeID
	)
VALUES 
	(
	@DetentionDate, 
	@DetainedByEmployeeID
	)
GO

CREATE PROC [InsertEmployee]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Patronymic nvarchar(50),
	@Position nvarchar(50),
	@EmployeeRank nvarchar(50)
AS
INSERT INTO Employees
	(
	FirstName,
	LastName,
	Patronymic,
	Position,
	EmployeeRank
	)
VALUES 
	(
	@FirstName,
	@LastName,
	@Patronymic,
	@Position,
	@EmployeeRank
	)

GO

CREATE proc [InsertRelease]
	@DetaineeID int,
	@DetentionID int,
	@ReleasedByEmployeeID int,
	@ReleaseDate datetime
AS
INSERT INTO ReleasesOfDetainees 
	( 
	DetaineeID,
	DetentionID,
	ReleasedByEmployeeID,
	ReleaseDate
	)
VALUES 
	(
	@DetaineeID,
	@DetentionID,
	@ReleasedByEmployeeID,
	@ReleaseDate 
	)
GO

CREATE PROC [UpdateDelivery]
	@DeliveryID int,
	@DetaineeID int,
	@DetentionID int,
	@ReleasedByEmployeeID int,
	@PlaceAddress nvarchar(50),
	@DeliveryDate datetime
AS
UPDATE 
	DeliveriesOfDetainees
SET 
	DetaineeID = @DetaineeID,
	DetentionID = @DetentionID,
	DeliveryDate = @DeliveryDate,
	DeliveredByEmployeeID  = @DeliveredByEmployeeID,
	PlaceAddress = @PlaceAddress
WHERE 
	DeliveryID = @DeliveryID
GO

CREATE PROC [UpdateDetainee]
	@DetaineeID int,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@Patronymic nvarchar(50) = NULL,
	@BirthDate date = NULL,
	@MaritalStatus nvarchar(50) = NULL,
	@Job nvarchar(50) = NULL,
	@MobilePhoneNumber nvarchar(50) = NULL,
	@HomePhoneNumber nvarchar(50) = NULL,
	@Photo image = NULL,
	@ExtraInfo nvarchar(50) = NULL,
	@ResidencePlace nvarchar(50) = NULL
AS
UPDATE 
	Detainees
SET 
	FirstName = @FirstName, 
	LastName = @LastName, 
	Patronymic = @Patronymic,
	BirthDate  = @BirthDate ,
	MaritalStatus = @MaritalStatus,
	Job = @Job,
	MobilePhoneNumber = @MobilePhoneNumber,
	HomePhoneNumber = @HomePhoneNumber,
	Photo = @Photo,
	ExtraInfo = @ExtraInfo,
	ResidencePlace = @ResidencePlace
WHERE 
	DetaineeID = @DetaineeID
GO

CREATE PROC [UpdateDetention]
	@DetentionID int,
	@DetentionDate datetime,
	@DetainedByEmployeeID int
AS
UPDATE 
	Detentions
SET 
	DetentionDate = @DetentionDate,
	DetainedByEmployeeID = @DetainedByEmployeeID
WHERE 
	DetentionID = @DetentionID
GO

CREATE PROC [UpdateEmployee]
	@EmployeeID int,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@Patronymic nvarchar(50) = NULL,
	@Position nvarchar(50) = NULL,
	@EmployeeRank nvarchar(50) = NULL
AS
UPDATE 
	Employees
SET 
	FirstName = @FirstName, 
	LastName = @LastName, 
	Patronymic = @Patronymic,
	Position = @Position,
	EmployeeRank = @EmployeeRank
WHERE 
	EmployeeID = @EmployeeID
GO

CREATE PROC [UpdateRelease]
	@ReleaseID int,
	@DetaineeID int,
	@ReleasedByEmployeeID int,
	@ReleaseDate datetime,
	@DetentionID int
AS
UPDATE 
	ReleasesOfDetainees
SET 
	DetaineeID = @DetaineeID,
	ReleaseDate = @ReleaseDate,
	ReleasedByEmployeeID  = @ReleasedByEmployeeID ,
	DetentionID = @DetentionID
WHERE 
	ReleaseID = @ReleaseID
GO

CREATE PROC [GetDetentionsByDate]
@DetentionDate datetime
AS
SELECT detentions.*
FROM Detentions AS detentions
Inner Join DetentionsOfDetainees 
ON DetentionsOfDetainees.DetentionID = detentions.DetentionID 
Inner Join Detainees 
ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID 
WHERE Detentions.DetentionDate = @DetentionDate
GO

CREATE PROC [GetDetentionsByLastName]
@LastName nvarchar(50)
AS
SELECT detentions.*
FROM Detentions AS detentions
Inner Join DetentionsOfDetainees 
ON DetentionsOfDetainees.DetentionID = detentions.DetentionID 
Inner Join Detainees 
ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID 
WHERE Detainees.LastName = @LastName
GROUP BY detentions.DetentionID, detentions.DetentionDate, detentions.DetainedByEmployeeID
GO

CREATE PROC [GetDetentionsByResidencePlace]
@ResidencePlace nvarchar(50)
AS
SELECT detentions.*
FROM Detentions AS detentions
Inner Join DetentionsOfDetainees 
ON DetentionsOfDetainees.DetentionID = detentions.DetentionID 
Inner Join Detainees 
ON Detainees.DetaineeID = DetentionsOfDetainees.DetaineeID 
WHERE Detainees.ResidencePlace = @ResidencePlace
GROUP BY detentions.DetentionID, detentions.DetentionDate, detentions.DetainedByEmployeeID
GO

CREATE PROC [CheckDetaineeInDetention]
@DetentionID int,
@DetaineeID int
AS
SELECT DetentionID FROM DetentionsOfDetainees
WHERE DetentionsOfDetainees.DetaineeID = @DetaineeID AND DetentionsOfDetainees.DetentionID = @DetentionID
GO

CREATE PROC [FindUser]
@Login nvarchar,
@Password nvarchar
AS
SELECT *
FROM Users
Where Users.Login = @Login AND Users.Password = @Password
GO

CREATE proc [GetUserByID]
@UserID int
AS
Select * From Users where UserID = @UserID
GO

CREATE PROC [InsertUser]
	@Login nvarchar,
	@Password nvarchar,
	@Role nvarchar,
	@Email nvarchar
AS
INSERT INTO Users
	(
	Login, 
	Password,
	Role,
	Email
	)
VALUES 
	(
	@Login, 
	@Password,
	@Role,
	@Email
	)
GO

CREATE PROC [UpdateUser]
	@UserID int,
	@Login nvarchar(50),
	@Password nvarchar(50),
	@Role nvarchar(50),
	@Email nvarchar(50)
AS
UPDATE 
	Users
SET 
	Login = @Login,
	Password = @Password,
	Email = @Email, 
	Role = @Role
WHERE 
	UserID = @UserID
GO

CREATE PROC [dbo].[UpdateUserPassword]
	@UserID int,
	@Password nvarchar(50)
AS
UPDATE 
	Users
SET 
	Password = @Password
WHERE 
	UserID = @UserID
GO
