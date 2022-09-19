CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id [uniqueidentifier]
AS
begin
    if exists (select * from dbo.[User] where Id = @Id AND isDeleted = 0)
    begin
	    update dbo.[User] 
	    set IsDeleted = 1
	    where Id = @Id;
    end
    else
        THROW 51000, 'The record does not exist.', 1;
end 
