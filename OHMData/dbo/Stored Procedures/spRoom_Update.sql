CREATE PROCEDURE [dbo].[spRoom_Update]
	@Id int,
	@RoomNumber int,
	@RoomType nvarchar(10),
	@RoomCapacity int,
	@RoomPrice money,
	@Description nvarchar(MAX),
	@IsAvailable bit

AS
begin
	set nocount on;

	update dbo.Room
	set RoomNumber = @RoomNumber, RoomType = @RoomType, RoomCapacity = @RoomCapacity, RoomPrice = @RoomPrice, Description = @Description, IsAvailable = @IsAvailable
	where Id = @Id;
end
