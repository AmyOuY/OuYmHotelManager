CREATE PROCEDURE [dbo].[spRoomIDLookUp]
	@RoomNumber int
	
AS
begin
	set nocount on;

	select Id
	from dbo.Room 
	where RoomNumber = @RoomNumber;
end