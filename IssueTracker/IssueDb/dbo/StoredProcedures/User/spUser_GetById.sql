CREATE PROCEDURE [dbo].[spUser_GetById]
@Id [uniqueidentifier]
AS
begin
	select Id,UserName, Email, Password, IsDeleted
	from dbo.[User]
	where Id=@Id AND isDeleted = 0;
end
