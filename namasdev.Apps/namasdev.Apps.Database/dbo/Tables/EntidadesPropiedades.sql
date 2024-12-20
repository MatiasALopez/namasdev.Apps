create table dbo.EntidadesPropiedades
(
	EntidadPropiedadId uniqueidentifier not null,
	EntidadId uniqueidentifier not null,
	Nombre nvarchar(100) not null,
	PropiedadTipoId smallint not null,
	PropiedadTipoEspecificaciones nvarchar(max) not null,
	PermiteNull bit not null,
	EsCalculado bit not null,
	CreadoPor nvarchar(128) not null,
	CreadoFecha datetime not null,
	UltimaModificacionPor nvarchar(128) not null,
	UltimaModificacionFecha datetime not null,
	BorradoPor nvarchar(128) null,
	BorradoFecha datetime null,
	Borrado AS (ISNULL(CONVERT(bit,CASE WHEN BorradoFecha IS NULL THEN 0 ELSE 1 END), 0)),

	constraint PK_EntidadesPropiedades primary key clustered (EntidadPropiedadId),
	constraint FK_EntidadesPropiedades_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesPropiedades_PropiedadTipoId foreign key (PropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId)
)
go

create nonclustered index IX_EntidadesPropiedades_EntidadId on dbo.EntidadesPropiedades (EntidadId)
go