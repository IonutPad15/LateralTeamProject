CREATE PROCEDURE [dbo].[spHistory_GetByProjectId]
	@ProjectId INT
AS
begin
	select *
    from dbo.[History]
    where ProjectId = @ProjectId
    order by Updated
end
