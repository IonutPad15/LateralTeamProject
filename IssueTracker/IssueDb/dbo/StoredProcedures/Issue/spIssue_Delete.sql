CREATE PROCEDURE [dbo].[spIssue_Delete]
	@Id int
AS
begin
	Update Issue 
	set IsDeleted = 1 
	where Id = @Id
end
