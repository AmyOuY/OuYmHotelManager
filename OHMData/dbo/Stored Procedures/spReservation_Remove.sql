CREATE PROCEDURE [dbo].[spReservation_Remove]
	@Id int

AS
begin
	set nocount on;

	delete from dbo.Reservation
	where Id = @Id;
end
