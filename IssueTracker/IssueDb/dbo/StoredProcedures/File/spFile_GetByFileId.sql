CREATE PROCEDURE [dbo].[spFile_GetByFileId]
	@FileId VARCHAR(36)
AS
begin
	select *
	from dbo.[File]
	where [File].FileIsDeleted = 0 and FileId = @FileId;
end
