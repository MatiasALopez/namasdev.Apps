create table dbo.EntidadesIndices
(
	EntidadIndiceId uniqueidentifier not null,
	EntidadId uniqueidentifier not null,
	Nombre nvarchar(200) not null,
	EsUnique bit not null,
	Condiciones nvarchar(2000) null,
	
	constraint PK_EntidadesIndices primary key clustered (EntidadIndiceId),
	constraint FK_EntidadesIndices_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId)
)
go

create nonclustered index IX_EntidadesIndices_EntidadId on dbo.EntidadesClaves (EntidadId)
go

create unique nonclustered index IX_EntidadesIndices_EntidadIdYNombre on dbo.EntidadesIndices (EntidadId,Nombre)
go
