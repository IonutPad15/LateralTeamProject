CREATE PROCEDURE [dbo].[spProject_Delete]
	@Id INT
AS
begin
	update dbo.[Project]
	set IsDeleted = 1
	where Id=@Id;
end
