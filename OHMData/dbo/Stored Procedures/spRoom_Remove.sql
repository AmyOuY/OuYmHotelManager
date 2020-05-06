CREATE PROCEDURE [dbo].[spRoom_Remove]
	@Id int

AS
begin
	set nocount on;

	delete from dbo.Room
	where Id = @Id;
end