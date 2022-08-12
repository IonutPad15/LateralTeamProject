CREATE PROCEDURE [dbo].[spUser_AboutUser]
@Id [uniqueidentifier]
AS
begin
	SELECT [t].[Id],[t].[UserName], [t].[Email], [t].[Password], [t].[IsDeleted],
				[t0].[Id], [t0].[UserId], [t0].[IssueId], [t0].[Body], [t0].[Created], [t0].[Updated], [t0].[IsDeleted]
	FROM dbo.[User] AS [t]
	LEFT JOIN dbo.[Comment] 
	 AS [t0] ON [t0].[UserId] = @Id
	 Where [t].Id =@Id;
end