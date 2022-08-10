CREATE PROCEDURE [dbo].[spUser_Update]
	@Id [uniqueidentifier],
	@Password nvarchar(256)
AS
begin
	update dbo.[User] 
	set Password = @Password
	where Id = @Id;
end 
