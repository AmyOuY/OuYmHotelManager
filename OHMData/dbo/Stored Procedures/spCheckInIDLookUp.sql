CREATE PROCEDURE [dbo].[spCheckInIDLookUp]
	@Client nvarchar(100),
	@Phone nvarchar(50)
AS
begin
	set nocount on;

	select Id 
	from dbo.CheckIn
	where Client = @Client and Phone = @Phone;
end
