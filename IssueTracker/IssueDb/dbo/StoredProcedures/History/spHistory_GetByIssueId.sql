CREATE PROCEDURE [dbo].[spHistory_GetByIssueId]
	@IssueId INT
AS
begin
    if exists(select * from dbo.[History] where IssueId = @IssueId)
    begin
	    select *
        from dbo.[History]
        where IssueId = @IssueId
        order by Updated
    end
    else
		THROW 51000, 'The record does not exist.', 1;
end
