CREATE PROCEDURE [dbo].[spIssueType_Get]
	@Id int 
AS 
begin
	select Id, Type
	from dbo.[IssueType]
	where @Id=Id;
end
