CREATE TABLE [dbo].[CheckOut]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [CheckInId] INT NOT NULL,
	[SubTotal] MONEY NOT NULL,
	[Tax] MONEY NOT NULL,
	[Total] MONEY NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_CheckOut_ToCheckIn] FOREIGN KEY (CheckInId) REFERENCES CheckIn(Id), 
    
)
