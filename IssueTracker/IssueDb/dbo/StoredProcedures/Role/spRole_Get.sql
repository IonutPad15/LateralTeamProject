CREATE PROCEDURE [dbo].[spRole_Get]
	@Id int
AS
begin
	select Id, Name
	from dbo.[Role]
	where Id=@Id;
end