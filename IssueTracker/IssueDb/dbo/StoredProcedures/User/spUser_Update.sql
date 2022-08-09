CREATE PROCEDURE [dbo].[spUser_Update]
	@Id [uniqueidentifier],
	@UserName nvarchar(50),
	@Email nvarchar(450),
	@Password nvarchar(256),
	@IsDeleted BIT
AS
begin
	update dbo.[User] 
	set Email = @Email, UserName=@UserName, Password = @Password, IsDeleted = @IsDeleted
	where Id = @Id;
end 
