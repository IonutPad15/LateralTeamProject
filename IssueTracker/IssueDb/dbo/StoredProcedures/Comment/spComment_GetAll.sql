CREATE PROCEDURE [dbo].[spComment_GetAll]
AS
begin
	select * 
	from dbo.[Comment]
	where IsDeleted = 0/*FALSE*/;
end