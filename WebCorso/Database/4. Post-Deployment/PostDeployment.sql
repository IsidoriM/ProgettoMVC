/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/




-- data generation for dbo.GrandRecords table

insert into GrandRecords ([Name])
select l
from (values 		
		('(A)'),('(B)'),('(C)'),('(D)'),('(E)'),('(F)'),('(G)'),('(H)'),('(I)'),('(J)'),('(K)'),('(L)'),('(M)'),
		('(N)'),('(O)'),('(P)'),('(Q)'),('(R)'),('(S)'),('(T)'),('(U)'),('(V)'),('(W)'),('(X)'),('(Y)'),('(Z)')
	) t(l)
where
	t.l not in (select [Name] from GrandRecords);


-- data generation for dbo.Records table	

with E as 
(
    select [Name] from (values 
		('A'),('B'),('C'),('D'),('E'),('F'),('G'),('H'),('I'),('J'),('K'),('L'),('M'),
		('N'),('O'),('P'),('Q'),('R'),('S'),('T'),('U'),('V'),('W'),('X'),('Y'),('Z')
	) t([Name])    
) 
insert into dbo.Records (
	GrandRecordId	,
	[Name]			)
select
	GrandRecordId	=	G.Id,
	[Name]			=	E.[Name]
from
	GrandRecords G, E
where
	not exists (select * from Records ee where ee.GrandRecordId = G.Id and ee.[Name] = E.[Name] );


-- data generation for dbo.Records table	

with C as 
(
    select [Name] from (values 
		('a'),('b'),('c'),('d'),('e'),('f'),('g'),('h'),('i'),('j'),('k'),('l'),('m'),
		('n'),('o'),('p'),('q'),('r'),('s'),('t'),('u'),('v'),('w'),('x'),('y'),('z')
	) t([Name])    
) 
insert into dbo.ChildRecords (
	RecordId	,
	[Name]		)
select
	RecordId	=	E.Id,
	[Name]		=	C.[Name]
from
	Records E, C
where
	not exists (select * from ChildRecords cc where cc.RecordId = E.Id and cc.[Name] = C.[Name] );

GO

