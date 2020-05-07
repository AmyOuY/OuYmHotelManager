CREATE PROCEDURE [dbo].[spReservation_GetAll]

AS
begin
	set nocount on;

	select Id, ClientId, RoomType, RoomNumber, DateIn, DateOut, CreatedDate
	from dbo.Reservation
	order by ClientId;
end
