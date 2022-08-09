CREATE PROCEDURE [dbo].[spStatus_GetAll]
AS
begin
	select * 
	from dbo.[Status];
end
