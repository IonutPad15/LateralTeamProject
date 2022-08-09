CREATE PROCEDURE [dbo].[spProject_Edit]
	@Id INT,
	@Title NVARCHAR(50),
	@Description NVARCHAR(max)
AS
begin
	update dbo.[Project]
	set Title =@Title, Description = @Description
	where Id = @Id
end