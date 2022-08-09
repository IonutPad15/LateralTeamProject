CREATE PROCEDURE [dbo].[spProject_GetAll]
AS
begin
	select * 
	from dbo.[Project]
	where IsDeleted = 0;
end