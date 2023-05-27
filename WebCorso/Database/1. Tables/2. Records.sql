create table dbo.Records
(
	Id				int				not null	identity,
	GrandRecordId	int				not null	,
	Name			varchar(30)		not null	,

	
	constraint PK_Records primary key clustered (Id),

	constraint FK_GrandRecords_GrandRecordId foreign key (GrandRecordId) references dbo.GrandRecords (Id) on delete cascade
);
