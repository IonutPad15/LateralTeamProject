CREATE PROCEDURE [dbo].[spFile_GetByFileId]
	@FileId VARCHAR(36)
AS
begin
	if exists(select TOP 1 * from dbo.[File] where FileId = @FileId)
		select *
		from dbo.[File]
		where [File].FileIsDeleted = 0 and FileId = @FileId;
	else
		THROW 5100, 'The record does not exist.', 1;
end
