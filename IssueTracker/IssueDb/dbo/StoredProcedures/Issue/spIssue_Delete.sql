CREATE PROCEDURE [dbo].[spIssue_Delete]
	@Id int
AS
begin
	if exists(select * from Issue where Id = @Id and IsDeleted = 0)
		Update Issue 
		set IsDeleted = 1 
		where Id = @Id
	else
		THROW 51000, 'The record does not exist.', 1;
end
