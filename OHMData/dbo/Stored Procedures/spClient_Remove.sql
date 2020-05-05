CREATE PROCEDURE [dbo].[spClient_Remove]
	@Id int

AS
begin
	set nocount on;

	delete from dbo.Client
	where Id = @Id;
end