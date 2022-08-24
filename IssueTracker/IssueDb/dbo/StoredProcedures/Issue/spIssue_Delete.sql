CREATE PROCEDURE [dbo].[spIssue_Delete]
	@Id int
AS
begin
	if exists(select * from Issue where Id = @Id)
		Update Issue 
		set IsDeleted = 1 
		where Id = @Id
	else
		THROW 5100, 'The record does not exist.', 1;
end
