CREATE PROCEDURE [dbo].[spHistory_Insert]
	@Type INT,
    @ProjectId INT,
    @UserId UNIQUEIDENTIFIER,
    @IssueId INT,
    @ReferenceType INT,
    @ReferenceId INT,
    @Field VARCHAR(1024),
    @OldValue VARCHAR (max),
    @NewValue VARCHAR (max),
    @Updated datetime2(7)
AS
begin
    insert into dbo.[History] (Type, ProjectId, UserId, IssueId, ReferenceType, ReferenceId, Field, OldValue, NewValue, Updated)
    values (@Type, @ProjectId, @UserId, @IssueId, @ReferenceType, @ReferenceId, @Field, @OldValue, @NewValue, @Updated)

    select CAST(SCOPE_IDENTITY() as int);
end
