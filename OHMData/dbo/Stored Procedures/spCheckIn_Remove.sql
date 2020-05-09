CREATE PROCEDURE [dbo].[spCheckIn_Remove]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.CheckIn
	where Id = @Id;
end