create type dbo.RecordTableType as table
(
	Id				int				not null	primary key clustered,
	GrandRecordId	int				not null	,
	Name			varchar(30)		not null
);