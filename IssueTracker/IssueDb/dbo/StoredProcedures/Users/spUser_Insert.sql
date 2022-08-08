CREATE PROCEDURE [dbo].[sbUser_Insert]
@Id [uniqueidentifier],
	@UserName nvarchar(50),
	@Email nvarchar(450),
	@Password nvarchar(256),
	@IsDeleted BIT

AS
begin
	insert into dbo.[User] (UserName, Email, Password, IsDeleted)
	values(@UserName, @Email, @Password, @IsDeleted);	
end 

