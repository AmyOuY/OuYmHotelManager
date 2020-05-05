CREATE PROCEDURE [dbo].[spClient_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(256),
	@Phone nvarchar(50),
	@Address nvarchar(500)
	
AS
begin
	set nocount on;

	update dbo.Client
	set FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, [Address] = @Address
	where Id = @Id;
end