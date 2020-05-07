CREATE PROCEDURE [dbo].[spReservationIDLookUp]
	@ClientId int

AS
begin
	set nocount on;

	select Id 
	from dbo.Reservation
	where ClientId = @ClientId;
end
