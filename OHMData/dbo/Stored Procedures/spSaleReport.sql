CREATE PROCEDURE [dbo].[spSaleReport]

AS
begin
	set nocount on;

	select i.RoomType, i.RoomNumber, i.StayDays, o.SubTotal, o.Tax, o.Total, o.CreatedDate
	from dbo.CheckOut o
	inner join dbo.CheckIn i on i.Id = o.CheckInId
	where i.IsCheckedOut = 1;
end