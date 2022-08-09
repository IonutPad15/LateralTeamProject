CREATE PROCEDURE [dbo].[spComment_Update]
	@Id INT,
	@Body NVARCHAR(max),
	@Updated datetime2(7)
AS
begin
	update dbo.[Comment]
	set Body=@Body, Updated = @Updated
	where Id = @Id;
end
