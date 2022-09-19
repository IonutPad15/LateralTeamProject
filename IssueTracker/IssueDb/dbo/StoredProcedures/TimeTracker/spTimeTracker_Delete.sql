CREATE PROCEDURE [dbo].[spTimeTracker_Delete]
    @Id int
AS
begin
	if(Exists(select * from TimeTracker where Id = @Id))
        Update TimeTracker set IsDeleted = 1 where Id = @Id;
    else
        throw 51000, 'The record does not exist.', 1;
end
