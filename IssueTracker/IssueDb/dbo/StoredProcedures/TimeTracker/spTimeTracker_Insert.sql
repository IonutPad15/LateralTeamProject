CREATE PROCEDURE [dbo].[spTimeTracker_Insert]
	@Name varchar(20),
    @Description varchar(max),
    @Date datetime,
    @Worked BigInt,
    @Billable BigInt,
    @UserId UNIQUEIDENTIFIER,
    @IssueId int
AS
begin
	insert into TimeTracker (Name, Description, Date , Worked, Billable, UserId, IssueId)
    values (@Name, @Description, @Date, @Worked, @Billable, @UserId, @IssueId);
end
