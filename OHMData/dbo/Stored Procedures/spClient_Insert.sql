CREATE PROCEDURE [dbo].[spClient_Insert]
	@Id int = 0 output,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(256),
	@Phone nvarchar(50),
	@Address nvarchar(500)
AS
begin
	set nocount on;

	insert into dbo.Client (FirstName, LastName, Email, Phone, Address)
	values (@FirstName, @LastName, @Email, @Phone, @Address);

	select @Id = SCOPE_IDENTITY();
end
