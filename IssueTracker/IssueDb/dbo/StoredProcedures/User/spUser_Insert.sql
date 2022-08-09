CREATE PROCEDURE [dbo].[spUser_Insert]
	@UserName nvarchar(50),
	@Email nvarchar(450),
	@Password nvarchar(256)

AS
begin
	insert into dbo.[User] (UserName, Email, Password)
	values(@UserName, @Email, @Password);	
end 

