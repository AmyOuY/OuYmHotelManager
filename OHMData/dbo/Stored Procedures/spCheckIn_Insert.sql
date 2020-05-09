CREATE PROCEDURE [dbo].[spCheckIn_Insert]
	@Id int = 0 output,
	@Client nvarchar(100),
	@Phone nvarchar(50),
	@RoomType nvarchar(10),
	@RoomNumber int,
	@RoomCapacity int,
	@RoomPrice money,
	@DateIn datetime2,
	@DateOut datetime2,
	@StayDays int,
	@GuestNumber int,
	@CreatedDate datetime2
AS
begin 
	set nocount on;

	insert into dbo.CheckIn (Client, Phone, RoomType, RoomNumber, RoomCapacity, RoomPrice, DateIn, DateOut, StayDays, GuestNumber, CreatedDate)
	values (@Client, @Phone, @RoomType, @RoomNumber, @RoomCapacity, @RoomPrice, @DateIn, @DateOut, @StayDays, @GuestNumber, @CreatedDate);

	select @Id = SCOPE_IDENTITY();
end