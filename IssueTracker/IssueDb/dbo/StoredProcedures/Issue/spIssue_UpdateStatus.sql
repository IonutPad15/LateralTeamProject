CREATE PROCEDURE [dbo].[spIssue_UpdateStatus]
	@Id int,
	@StatusId int
AS
begin
	if exists(select * from Issue where Id = @Id and IsDeleted = 0)
		Update Issue set StatusId = @StatusId where Id = @Id
	else
		THROW 51000, 'The record does not exist.', 1;
end
