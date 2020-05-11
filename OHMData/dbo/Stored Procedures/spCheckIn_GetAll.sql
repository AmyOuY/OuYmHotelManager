CREATE PROCEDURE [dbo].[spCheckIn_GetAll]

AS
begin
	set nocount on;

	select Id, Client, Phone, RoomType, RoomNumber, RoomCapacity, RoomPrice, DateIn, DateOut, StayDays, GuestNumber, IsCheckedOut, CreatedDate
	from dbo.CheckIn
	order by Id;
end
