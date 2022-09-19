CREATE PROCEDURE [dbo].[spProject_Edit]
	@Id INT,
	@Title NVARCHAR(50),
	@Description NVARCHAR(max)
AS
begin
    if exists (select * from dbo.[Project] where Id = @Id and IsDeleted = 0)
    begin
	    update dbo.[Project]
	    set Title =@Title, Description = @Description
	    where Id = @Id
    end
    else
        THROW 51000, 'The record does not exist.', 1;
end
