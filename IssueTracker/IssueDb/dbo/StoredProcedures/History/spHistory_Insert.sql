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
	if (@Type < 1 OR @Type > 3)---1 for created, 2 for updated, 3 for deleted
        THROW 51001, 'The type is not good.', 1;
    else
    if (@ReferenceType < 1 OR @ReferenceType > 3)---1 for comment, 2 participant, 3 for file
        THROW 51001, 'The type is not good.', 1;
    else
    begin
        insert into dbo.[History] (Type, ProjectId, UserId, IssueId, ReferenceType, ReferenceId, Field, OldValue, NewValue, Updated)
        values (@Type, @ProjectId, @UserId, @IssueId, @ReferenceType, @ReferenceId, @Field, @OldValue, @NewValue, @Updated)

        select CAST(SCOPE_IDENTITY() as int);
    end
end
