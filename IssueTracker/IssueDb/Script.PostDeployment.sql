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
	('ReadyForTesting'),
	('Testing'),
	('Done');
end
if not exists (select 1 from dbo.[Role])
begin
	insert into dbo.[Role] 
	values ('Developer'),
	('Tester'),
	('Owner'),
	('Collaborator');
end
if not exists (select 1 from dbo.[User])
begin
	insert into dbo.[User](UserName,Email,Password,IsDeleted)
	values('Mihai','mihai@yahoo.com','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',0);
end


