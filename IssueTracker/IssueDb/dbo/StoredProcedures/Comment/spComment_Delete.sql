CREATE PROCEDURE [dbo].[spComment_Delete]
	@Id int
AS
begin
	update dbo.[Comment]
	set IsDeleted = 1/*TRUE*/
	where Id = @Id;
end
