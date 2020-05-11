CREATE TABLE [dbo].[CheckIn]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Client] NVARCHAR(100) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL, 
    [RoomType] NVARCHAR(10) NOT NULL, 
    [RoomNumber] INT NOT NULL, 
    [RoomCapacity] INT NOT NULL, 
    [RoomPrice] MONEY NOT NULL, 
    [DateIn] DATETIME2 NOT NULL, 
    [DateOut] DATETIME2 NOT NULL, 
    [StayDays] INT NOT NULL, 
    [GuestNumber] INT NOT NULL, 
	[IsCheckedOut] BIT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
