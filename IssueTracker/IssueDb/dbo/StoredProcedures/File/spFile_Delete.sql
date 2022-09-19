CREATE PROCEDURE [dbo].[spFile_Delete]
	@FileId VARCHAR(36),
    @Updated DATETIME
AS
begin
	IF EXISTS(SELECT * FROM dbo.[File] WHERE FileId = @FileId and FileIsDeleted = 0)
	begin
		update dbo.[File]
		set FileIsDeleted = 1, Updated = @Updated/*TRUE*/
		where FileId = @FileId
	end
	ELSE
	begin;
		THROW 51000, 'The record does not exist.', 1;
	end
end

