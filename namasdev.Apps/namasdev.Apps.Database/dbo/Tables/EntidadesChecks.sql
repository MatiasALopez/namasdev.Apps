create table dbo.EntidadesChecks
(
	EntidadCheckId uniqueidentifier not null,
	Nombre nvarchar(200) not null,
	EntidadId uniqueidentifier not null,
	Expresion nvarchar(2000) not null,
	
	constraint PK_EntidadesChecks primary key clustered (EntidadCheckId),
	constraint FK_EntidadesChecks_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId)
)
go

create nonclustered index IX_EntidadesChecks_EntidadId on dbo.EntidadesChecks (EntidadId)
go
