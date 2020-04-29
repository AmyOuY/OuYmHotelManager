CREATE TABLE [dbo].[Reservation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL, 
    [RoomId] INT NOT NULL, 
    [DateIn] DATETIME2 NOT NULL, 
    [DateOut] DATETIME2 NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Reservation_ToClient] FOREIGN KEY (ClientId) REFERENCES Client(Id), 
    CONSTRAINT [FK_Reservation_ToRoom] FOREIGN KEY (RoomId) REFERENCES Room(Id)
)
