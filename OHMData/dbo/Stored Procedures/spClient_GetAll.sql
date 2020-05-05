CREATE PROCEDURE [dbo].[spClient_GetAll]
AS
begin
	set nocount on;

	select Id, FirstName, LastName, Email, Phone, [Address] 
	from dbo.Client 
	order by FirstName;

end
