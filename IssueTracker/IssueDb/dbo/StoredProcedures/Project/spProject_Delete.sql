CREATE PROCEDURE [dbo].[spProject_Delete]
	@Id INT
AS
begin
    if exists (select * from dbo.[Project] where Id = @Id and IsDeleted = 0)
    begin
	    update dbo.[Project]
	    set IsDeleted = 1
	    where Id=@Id;
    end
    else
        THROW 51000, 'The record does not exist.', 1;
end
