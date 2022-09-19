CREATE PROCEDURE [dbo].[spUser_Update]
	@Id [uniqueidentifier],
	@UserName nvarchar(50),
	@Email nvarchar(450),
	@Password nvarchar(256)
AS
begin
    if exists (select * from dbo.[User] where Id = @Id AND IsDeleted = 0)
    begin
	    update dbo.[User] 
	    set Email = @Email, UserName=@UserName, Password = @Password
	    where Id = @Id;
    end
    else
        THROW 51000, 'The record does not exist.', 1;
end 
