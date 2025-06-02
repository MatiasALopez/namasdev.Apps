create table dbo.EntidadesClaves
(
	EntidadClaveId uniqueidentifier not null,
	EntidadId uniqueidentifier not null,
	EntidadPropiedadId uniqueidentifier not null,
	
	constraint PK_EntidadesClaves primary key clustered (EntidadClaveId),
	constraint FK_EntidadesClaves_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesClaves_EntidadPropiedadId foreign key (EntidadPropiedadId) references dbo.EntidadesPropiedades (EntidadPropiedadId)
)
go

create nonclustered index IX_EntidadesClaves_EntidadId on dbo.EntidadesClaves (EntidadId)
go

create unique nonclustered index IX_EntidadesClaves_EntidadIdYEntidadPropiedadId on dbo.EntidadesClaves (EntidadId,EntidadPropiedadId)
go
