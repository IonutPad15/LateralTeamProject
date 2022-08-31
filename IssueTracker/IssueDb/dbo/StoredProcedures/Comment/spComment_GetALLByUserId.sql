CREATE PROCEDURE [dbo].[spComment_GetAllByUserId]
	@UserId UNIQUEIDENTIFIER
AS
begin

	CREATE TABLE #comentariu
    (
	    [Id] INT NOT NULL,
	    [UserId] UNIQUEIDENTIFIER,
	    [IssueId] INT ,
	    [CommentId] INT,
	    [Author] NVARCHAR(30),
	    [Body] NVARCHAR(450) NOT NULL, 
	    [Created] [datetime2](7) NOT NULL,
	    [Updated] [datetime2](7) NOT NULL,
	    [IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
	    [FileId] VARCHAR(36) ,
        [GroupId] NVARCHAR(350) ,
        [FileIssueId] INT,
        [FileCommentId] INT,
        [FileIsDeleted] BIT DEFAULT (CONVERT([bit],(0))),
    )

    
    DECLARE @idColumn INT
	SELECT *
    INTO #comm_temp
    FROM dbo.[Comment]
	WHERE [Comment].IsDeleted = 0 AND [Comment].UserId = @UserId

    SELECT @idColumn = min( Id ) FROM #comm_temp
    
	WHILE @idColumn is not null
	BEGIN
		INSERT INTO #comentariu (Id, UserId, IssueId, CommentId, Author, Body, Created, Updated, IsDeleted, FileId, GroupId, FileIssueId, FileCommentId, FileIsDeleted)
	    (
		    SELECT *
		    FROM dbo.[Comment]
            LEFT JOIN [File] ON [File].FileCommentId = @idColumn AND [File].FileIsDeleted = 0
		    WHERE [Comment].IsDeleted = 0 AND [Comment].Id = @idColumn
		)
		SELECT @idColumn = min( Id ) FROM #comm_temp WHERE  Id > @idColumn
	end

    SELECT *
    FROM #comentariu
    
	
end
