CREATE PROCEDURE [dbo].[spReservation_Update]
	@Id int,
	@ClientId int,
	@RoomType nvarchar(10),
	@RoomNumber int,
	@DateIn datetime2,
	@DateOut datetime2,
	@CreatedDate datetime2
AS
begin
	set nocount on;

	update dbo.Reservation
	set ClientId = @ClientId, RoomType = @RoomType, RoomNumber = @RoomNumber, DateIn = @DateIn, DateOut = @DateOut, CreatedDate = @CreatedDate
	where Id = @Id;
end