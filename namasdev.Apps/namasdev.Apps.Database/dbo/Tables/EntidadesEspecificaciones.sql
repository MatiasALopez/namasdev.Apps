create table dbo.EntidadesEspecificaciones
(
	EntidadId uniqueidentifier not null,
	IdiomaArticuloId nchar(3) null,
	IDPropiedadTipoId smallint null,
	EsSoloLectura bit not null,
	BajaTipoId smallint not null,
	AuditoriaCreado bit not null,
	AuditoriaUltimaModificacion bit not null,

	constraint PK_EntidadesEspecificaciones primary key clustered (EntidadId),
	constraint FK_EntidadesEspecificaciones_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesEspecificaciones_IdiomaArticuloId foreign key (IdiomaArticuloId) references dbo.IdiomasArticulos (IdiomaArticuloId),
	constraint FK_EntidadesEspecificaciones_IDPropiedadTipoId foreign key (IDPropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId),
	constraint FK_EntidadesEspecificaciones_BajaTipoId foreign key (BajaTipoId) references dbo.BajaTipos (BajaTipoId),
	constraint CK_EntidadesEspecificaciones_EsSoloLecturaYBajaTipoYAuditorias check ((EsSoloLectura = 0) or (EsSoloLectura = 1 and BajaTipoId = 3 and AuditoriaCreado = 0 and AuditoriaUltimaModificacion = 0))
)
go
