if not exists (select 1 from dbo.[Priority])
begin
	insert into dbo.[Priority]
	values ('Minor'),
	('Medium'),
	('Major'),
	('Critical');
end

if not exists (select 1 from dbo.[IssueType])
begin
	insert into dbo.[IssueType]
	values ('Bug'),
	('Feature'),
	('Task');
end

if not exists (select 1 from dbo.[Status])
begin
	insert into dbo.[Status] 
	values ('ToDo'),
	('InProgress'),
	('Done'),
	('Closed');
end
if not exists (select 1 from dbo.[Role])
begin
	insert into dbo.[Role] 
	values ('Developer'),
	('Tester'),
	('Owner'),
	('Collaborator');
end


