CREATE PROCEDURE [dbo].[spComment_Update]
	@Id INT,
	@Body NVARCHAR(450),
	@Updated datetime2(7)
AS
begin
    if exists (select * from dbo.[Comment] where Id = @Id and IsDeleted = 0)
    begin
	    update dbo.[Comment]
	    set Body=@Body, Updated = @Updated
	    where Id = @Id;
    end
    else
        THROW 51000, 'The record does not exist.', 1;
end
