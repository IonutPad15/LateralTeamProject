CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id [uniqueidentifier]
AS
begin
	update dbo.[User] 
	set IsDeleted = 1
	where Id = @Id;
end 