CREATE PROCEDURE [dbo].[spCheckIn_Update]
	@Id int,
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
	@IsCheckedOut bit,
	@CreatedDate datetime2
AS
begin
	set nocount on;

	update dbo.CheckIn
	set Client = @Client, Phone = @Phone, RoomType = @RoomType, RoomNumber = @RoomNumber, RoomCapacity = @RoomCapacity, RoomPrice = @RoomPrice, DateIn = @DateIn, DateOut = @DateOut, StayDays = @StayDays, GuestNumber = @GuestNumber, IsCheckedOut = @IsCheckedOut, CreatedDate = @CreatedDate
	where Id = @Id;
end
