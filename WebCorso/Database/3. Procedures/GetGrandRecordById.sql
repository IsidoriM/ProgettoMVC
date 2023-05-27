create procedure dbo.GetGrandRecordById
	@Id	int
as
begin
	set nocount on;

	select
		Id	,
		Name	
	from
		dbo.GrandRecords
	where
		Id = @Id;
		
			
	select
		Id				,
		GrandRecordId	,
		Name	
	from
		dbo.Records
	where
		GrandRecordId = @Id
	order by
		Id;


	select
		cr.Id		,
		cr.RecordId	,
		cr.Name
	from
		dbo.ChildRecords cr
		inner join dbo.Records r on r.Id = cr.RecordId
	where
		GrandRecordId = @Id
	order by
		cr.RecordId,
		cr.Id;


end;
