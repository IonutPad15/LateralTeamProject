CREATE PROCEDURE [dbo].[sbUser_Get]
@Id [uniqueidentifier]
AS
begin
	select Id,UserName, Email, Password, isDeleted
	from dbo.[User]
	where Id=@Id AND isDeleted = 0;
end
