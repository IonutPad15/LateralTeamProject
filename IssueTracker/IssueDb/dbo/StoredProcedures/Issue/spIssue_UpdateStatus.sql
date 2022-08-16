CREATE PROCEDURE [dbo].[spIssue_UpdateStatus]
	@Id int,
	@StatusId int
AS
begin
	Update Issue set StatusId = @StatusId where Id = @Id
end
