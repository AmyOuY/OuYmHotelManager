CREATE PROCEDURE [dbo].[spClientLookUp]
	@ClientId int 

AS
begin
	set nocount on;

	select Id, ClientId, RoomType, RoomNumber, DateIn, DateOut, CreatedDate
	from dbo.Reservation
	where ClientId = @ClientId;
end
