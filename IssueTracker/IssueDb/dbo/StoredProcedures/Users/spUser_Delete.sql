CREATE PROCEDURE [dbo].[sbUser_Delete]
	@Id [uniqueidentifier],
	@UserName nvarchar(50),
	@Email nvarchar(450),
	@Password nvarchar(256)
AS
begin
	update dbo.[User] 
	set Email = @Email, UserName=@UserName, Password = @Password, IsDeleted = 0
	where Id = @Id;
end 