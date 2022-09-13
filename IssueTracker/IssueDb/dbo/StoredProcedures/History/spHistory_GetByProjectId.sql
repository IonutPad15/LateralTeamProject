CREATE PROCEDURE [dbo].[spHistory_GetByProjectId]
	@ProjectId INT
AS
begin
    if exists(select * from dbo.[History] where ProjectId = @ProjectId)
    begin
	    select *
        from dbo.[History]
        where ProjectId = @ProjectId
        order by Updated
    end
    else
		THROW 51000, 'The record does not exist.', 1;
end
