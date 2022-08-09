CREATE PROCEDURE [dbo].[spProject_Insert]
	@Title NVARCHAR(50),
	@Description NVARCHAR(max),
	@Created datetime2(7)
AS
begin
	insert into dbo.[Project] (Title, Description, Created)
	values (@Title, @Description, @Created);
end