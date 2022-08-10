CREATE PROCEDURE [dbo].[spUser_AboutUser]
@Id [uniqueidentifier]
AS
begin
	select [t].[Id],[t].[UserName], [t].[Email], [t].[Password], [t].[IsDeleted],
	[t0].[Id], [t0].[UserId], [t0].[IssueId], [t0].[Body], [t0].[Created], [t0].[Updated], [t0].[IsDeleted]
	from
	(
	select TOP(1) [u].[Id],[u].[UserName], [u].[Email], [u].[Password], [u].[IsDeleted]
	from dbo.[User] AS [u]
	where [u].[Id]=@Id AND [u].[IsDeleted] = 0)
	AS [t]
	INNER JOIN  (
	select [c].[Id], [c].[UserId], [c].[IssueId], [c].[Body], [c].[Created], [c].[Updated], [c].[IsDeleted]
	from dbo.[Comment] AS [c]
	where [c].[IsDeleted] = 0)
	AS [t0] ON [t0].[UserId] = @Id;
end