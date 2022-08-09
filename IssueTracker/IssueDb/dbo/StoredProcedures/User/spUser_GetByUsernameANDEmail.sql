CREATE PROCEDURE [dbo].[spUser_GetByUsernameANDEmail]
@UserName NVARCHAR(30),
@Email NVARCHAR(450)
AS
begin
	select Id,UserName, Email, Password, IsDeleted
	from dbo.[User]
	where isDeleted = 0 AND (UserName = @UserName OR Email = @Email);
end