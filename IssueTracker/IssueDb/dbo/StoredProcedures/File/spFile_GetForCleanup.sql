CREATE PROCEDURE [dbo].[spFile_GetForCleanup]
    @Updated DATETIME
AS
begin
    select *
	from dbo.[File]
	where [File].FileIsDeleted = 1 and Updated > @Updated
end
