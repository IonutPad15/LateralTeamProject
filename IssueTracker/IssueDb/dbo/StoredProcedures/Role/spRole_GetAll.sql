CREATE PROCEDURE [dbo].[spRole_GetAll]
AS
begin
	select *
	from dbo.[Role];
end