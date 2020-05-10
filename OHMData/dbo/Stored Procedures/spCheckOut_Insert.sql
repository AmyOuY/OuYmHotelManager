CREATE PROCEDURE [dbo].[spCheckOut_Insert]
	@Id int = 0 output,
	@CheckInId int,
	@SubTotal money,
	@Tax money,
	@Total money,
	@CreatedDate datetime2

AS
begin
	set nocount on;

	insert into dbo.CheckOut (CheckInId, SubTotal, Tax, Total, CreatedDate)
	values (@CheckInId, @SubTotal, @Tax, @Total, @CreatedDate);

	select @Id = SCOPE_IDENTITY();
end
