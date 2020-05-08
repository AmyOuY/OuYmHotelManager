CREATE PROCEDURE [dbo].[spReservation_Insert]
	@Id int = 0 output,
	@ClientId int,
	@RoomType nvarchar(10),
	@RoomNumber int,
	@DateIn datetime2,
	@DateOut datetime2,
	@CreatedDate datetime2
AS
begin
	set nocount on;

	insert into dbo.Reservation (ClientId, RoomType, RoomNumber, DateIn, DateOut, CreatedDate)
	values (@ClientId, @RoomType, @RoomNumber, @DateIn, @DateOut, @CreatedDate);

	select @Id = SCOPE_IDENTITY();
end
