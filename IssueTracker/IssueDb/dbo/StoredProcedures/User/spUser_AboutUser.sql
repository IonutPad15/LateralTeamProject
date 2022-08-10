CREATE PROCEDURE [dbo].[spUser_AboutUser]
@Id [uniqueidentifier]
AS
begin
	SELECT [t].[Id],[t].[UserName], [t].[Email], [t].[Password], [t].[IsDeleted],
				[t0].[Id], [t0].[UserId], [t0].[IssueId], [t0].[Body], [t0].[Created], [t0].[Updated], [t0].[IsDeleted]
	FROM
	(
		SELECT TOP(1) [u].[Id],[u].[UserName], [u].[Email], [u].[Password], [u].[IsDeleted]
		FROM dbo.[User] AS [u]
		WHERE [u].[Id]=@Id AND [u].[IsDeleted] = 0
	) AS [t]
	LEFT JOIN  (
		SELECT [c].[Id], [c].[UserId], [c].[IssueId], [c].[Body], [c].[Created], [c].[Updated], [c].[IsDeleted]
		FROM dbo.[Comment] AS [c]
		WHERE [c].[IsDeleted] = 0
	) AS [t0] ON [t0].[UserId] = @Id;
end