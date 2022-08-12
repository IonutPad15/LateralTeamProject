CREATE PROCEDURE [dbo].[spComment_Get]
	@Id INT
AS
begin
	select *
	from dbo.[Comment] 
	where IsDeleted = 0 AND [Comment].Id = @Id;
end	