CREATE PROCEDURE [dbo].[spPriority_Get]
	@Id int 
AS 
	select Id, Type
	from dbo.[Priority]
	where Id=@Id;