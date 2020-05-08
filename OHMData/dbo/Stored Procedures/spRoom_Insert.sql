CREATE PROCEDURE [dbo].[spRoom_Insert]
	@Id int = 0 output,
	@RoomNumber int,
	@RoomType nvarchar(10),
	@RoomCapacity int,
	@RoomPrice money,
	@Description nvarchar(MAX),
	@IsAvailable bit
AS
begin
	set nocount on;

	Insert into dbo.Room (RoomNumber, RoomType, RoomCapacity, RoomPrice, Description, IsAvailable)
	values (@RoomNumber, @RoomType, @RoomCapacity, @RoomPrice, @Description, @IsAvailable);

	select @Id = SCOPE_IDENTITY();
end