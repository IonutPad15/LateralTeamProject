CREATE PROCEDURE [dbo].[spComment_Delete]
	@Id int
AS
begin
    if exists (select * from dbo.[Comment] where Id = @Id and IsDeleted = 0)
    begin
	    update dbo.[Comment]
	    set IsDeleted = 1
	    where Id = @Id;
    end
    else
    begin;
		THROW 51000, 'The record does not exist.', 1;
	end
end
