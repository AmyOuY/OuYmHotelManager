CREATE PROCEDURE [dbo].[spRoom_GetAll]

AS
begin
	set nocount on;

	select Id, RoomNumber, RoomType, RoomCapacity, RoomPrice, [Description], IsAvailable
	from dbo.Room
	order by RoomNumber;
end
