create table dbo.EntidadesPropiedadesDefault
(
	EntidadId uniqueidentifier not null,
	IDPropiedadTipoId smallint null,
	AuditoriaCreado bit not null,
	AuditoriaUltimaModificacion bit not null,
	AuditoriaBorrado bit not null,

	constraint PK_EntidadesPropiedadesDefault primary key clustered (EntidadId),
	constraint FK_EntidadesPropiedadesDefault_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesPropiedadesDefault_IDPropiedadTipoId foreign key (IDPropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId)
)
go
