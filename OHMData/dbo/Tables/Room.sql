CREATE TABLE [dbo].[Room]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RoomNumber] INT NOT NULL, 
    [RoomType] NVARCHAR(10) NOT NULL, 
    [RoomCapacity] INT NOT NULL, 
    [RoomPrice] MONEY NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [IsAvailable] BIT NOT NULL DEFAULT 1
)
