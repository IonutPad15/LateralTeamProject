CREATE PROCEDURE [dbo].[spHistory_Insert]
	@Type INT,
    @ProjectId INT,
    @Author NVARCHAR (30),
    @IssueId INT,
    @ReferenceType INT,
    @ReferenceId VARCHAR(36),
    @Field VARCHAR(1024),
    @OldValue VARCHAR (max),
    @NewValue VARCHAR (max),
    @Updated datetime2(7)
AS
begin
    insert into dbo.[History] (Type, ProjectId, Author, IssueId, ReferenceType, ReferenceId, Field, OldValue, NewValue, Updated)
    values (@Type, @ProjectId, @Author, @IssueId, @ReferenceType, @ReferenceId, @Field, @OldValue, @NewValue, @Updated)

    select CAST(SCOPE_IDENTITY() as int);
end
