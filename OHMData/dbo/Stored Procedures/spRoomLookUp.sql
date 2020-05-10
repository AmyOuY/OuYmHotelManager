CREATE PROCEDURE [dbo].[spRoomLookUp]
	@RoomNumber int 

AS
begin
	set nocount on;

	select Id, RoomNumber, RoomType, RoomCapacity, RoomPrice, [Description], IsAvailable
	from dbo.Room 
	where RoomNumber = @RoomNumber;
end
