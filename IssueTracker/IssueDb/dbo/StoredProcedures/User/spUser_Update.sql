CREATE PROCEDURE [dbo].[spUser_Update]
	@Id [uniqueidentifier],
	@UserName nvarchar(50),
	@Email nvarchar(450),
	@Password nvarchar(256)
AS
begin
	update dbo.[User] 
	set Email = @Email, UserName=@UserName, Password = @Password
	where Id = @Id;
end 