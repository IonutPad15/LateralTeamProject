CREATE PROCEDURE [dbo].[spTimeTracker_Insert]
	@Name varchar(20),
    @Description varchar(max),
    @Date datetime,
    @Worked smallint,
    @Billable smallint,
    @Remaining smallint,
    @UserId UNIQUEIDENTIFIER,
    @IssueId int
AS
begin
	insert into TimeTracker (Name, Description, Date , Worked, Billable, Remaining, UserId, IssueId)
    values (@Name, @Description, @Date, @Worked, @Billable, @Remaining, @UserId, @IssueId);
end
