CREATE PROCEDURE [dbo].[spClientIDLookUp]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Phone nvarchar(50)
AS
begin
	set nocount on;

	select Id 
	from dbo.Client
	where FirstName = @FirstName and LastName = @LastName and Phone = @Phone;
end
