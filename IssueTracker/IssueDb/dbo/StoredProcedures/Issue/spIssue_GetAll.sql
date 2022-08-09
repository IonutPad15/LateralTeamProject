CREATE PROCEDURE [dbo].[spIssue_GetAll]
AS
begin
	select * 
	from dbo.[Issue]
	where IsDeleted = 0;
end