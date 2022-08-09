CREATE PROCEDURE [dbo].[spStatus_Get]
	@Id int
AS
begin
	select Id, Type
	from dbo.[Status] 
	where @Id=Id;
end