CREATE PROCEDURE [dbo].[spHistory_GetByIssueId]
	@IssueId INT
AS
begin
	select *
    from dbo.[History]
    where IssueId = @IssueId
    order by Updated
end
