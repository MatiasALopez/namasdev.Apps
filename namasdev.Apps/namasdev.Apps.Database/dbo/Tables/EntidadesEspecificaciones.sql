create table dbo.EntidadesEspecificaciones
(
	EntidadId uniqueidentifier not null,
	ArticuloId smallint not null,
	IDPropiedadTipoId smallint null,
	EsSoloLectura bit not null,
	BajaTipoId smallint not null,
	AuditoriaCreado bit not null,
	AuditoriaUltimaModificacion bit not null,

	constraint PK_EntidadesEspecificaciones primary key clustered (EntidadId),
	constraint FK_EntidadesEspecificaciones_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesEspecificaciones_ArticuloId foreign key (ArticuloId) references dbo.Articulos (ArticuloId),
	constraint FK_EntidadesEspecificaciones_IDPropiedadTipoId foreign key (IDPropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId),
	constraint FK_EntidadesEspecificaciones_BajaTipoId foreign key (BajaTipoId) references dbo.BajaTipos (BajaTipoId),
	constraint CK_EntidadesEspecificaciones_EsSoloLecturaYBajaTipoYAuditorias check ((EsSoloLectura = 0) or (EsSoloLectura = 1 and BajaTipoId = 3 and AuditoriaCreado = 0 and AuditoriaUltimaModificacion = 0))
)
go
