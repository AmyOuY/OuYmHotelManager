CREATE PROCEDURE [dbo].[spCheckInLookUp]
	@Client nvarchar(100),
	@Phone nvarchar(50)

AS
begin
	set nocount on;

	select Id, Client, Phone, RoomType, RoomNumber, RoomCapacity, RoomPrice, DateIn, DateOut, StayDays, GuestNumber, IsCheckedOut, CreatedDate
	from dbo.CheckIn
	where Client = @Client and Phone = @Phone;
end