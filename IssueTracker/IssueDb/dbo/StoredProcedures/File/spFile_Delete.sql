CREATE PROCEDURE [dbo].[spFile_Delete]
	@FileId VARCHAR(36)
AS
begin
	IF EXISTS(SELECT * FROM dbo.[File] WHERE FileId = @FileId)
	begin
		update dbo.[File]
		set FileIsDeleted = 1 /*TRUE*/
		where FileId = @FileId
	end
	ELSE
	begin;
		THROW 51000, 'The record does not exist.', 1;
	end
end
