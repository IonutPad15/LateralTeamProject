CREATE PROCEDURE [dbo].[spUser_GetByCredentials]
@NameEmail NVARCHAR(450),
@Password NVARCHAR(max)

AS
begin
	select Id,UserName, Email, Password, IsDeleted
	from dbo.[User]
	where isDeleted = 0 AND Password = @Password AND (UserName = @NameEmail OR Email = @NameEmail);
end
