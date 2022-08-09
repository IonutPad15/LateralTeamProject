CREATE PROCEDURE [dbo].[spProject_Get]
	@Id INT
AS
begin
	select Id, Title, Description, Created, IsDeleted
	from dbo.[Project]
	where IsDeleted = 0;
end	