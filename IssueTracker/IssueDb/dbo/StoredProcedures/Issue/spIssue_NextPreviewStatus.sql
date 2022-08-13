CREATE PROCEDURE [dbo].[spIssue_NextPreviewStatus]
	@Id int,
	@Method varchar(8),
	@StatusId int
AS
begin
	IF (@Method = 'next')
	begin
		if(@StatusId = 4)
		begin
			print 'This issue is max level of status!'
		end
		else
		begin
			Update Issue set StatusId = (@StatusId + 1) where Issue.Id = @Id
		end
	end
	ELSE
	begin
		if(@StatusId = 1)
		begin
		print 'This issue is low level of status!'
		end
		else
		begin
			Update Issue set StatusId = (@StatusId - 1) where Issue.Id = @Id
		end
	END
end
