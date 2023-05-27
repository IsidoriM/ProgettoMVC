create procedure dbo.GetGrandRecords
as
begin
	set nocount on;

	select
		Id	,
		Name	
	from
		dbo.GrandRecords
	order by
		Id;
		
			
	select
		Id				,
		GrandRecordId	,
		Name				
	from
		dbo.Records
	order by
		GrandRecordId,
		Id;


	select
		cr.Id		,
		cr.RecordId	,
		cr.Name				
	from
		dbo.ChildRecords cr
		inner join dbo.Records r on r.Id = cr.RecordId
	order by
		r.GrandRecordId,
		cr.RecordId,
		cr.Id;


end;
