CREATE PROCEDURE [dbo].[spIssueType_GetAll]
AS
begin
	select *
	from dbo.[IssueType];
end