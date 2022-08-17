CREATE PROCEDURE [dbo].[spParticipant_GetOwner]
@ProjectId INT
AS
begin
	select *
	from dbo.[Participant]
	left join dbo.[User] ON [User].Id = [Participant].UserId AND [User].IsDeleted = 0/*FALSE*/
	left join dbo.[Role] ON [Role].Id = [Participant].RoleId 
	where [Participant].IsDeleted = 0/*FALSE*/ AND [Participant].ProjectId = @ProjectId
		AND [Participant].RoleId = 3 /*OWNER*/
end

