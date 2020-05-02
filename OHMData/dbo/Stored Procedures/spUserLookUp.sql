CREATE PROCEDURE [dbo].[spUserLookUp]
	@Id nvarchar(128)

AS
begin
	set nocount on;

	select Id, FirstName, LastName, Email, CreatedDate
	from dbo.[User]
	where Id = @Id;
end
