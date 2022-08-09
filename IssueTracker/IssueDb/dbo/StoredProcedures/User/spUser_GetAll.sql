CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
begin
	select * 
	from dbo.[User]
	where IsDeleted = 0;
end
