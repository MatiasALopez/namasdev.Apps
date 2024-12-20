create table dbo.AplicacionesVersiones
(
	AplicacionVersionId uniqueidentifier not null,
	AplicacionId uniqueidentifier not null,
	Nombre nvarchar(50) not null,
	CreadoPor nvarchar(128) not null,
	CreadoFecha datetime not null,
	UltimaModificacionPor nvarchar(128) not null,
	UltimaModificacionFecha datetime not null,
	BorradoPor nvarchar(128) null,
	BorradoFecha datetime null,
	Borrado AS (ISNULL(CONVERT(bit,CASE WHEN BorradoFecha IS NULL THEN 0 ELSE 1 END), 0)),

	constraint PK_AplicacionesVersiones primary key clustered (AplicacionVersionId),
	constraint FK_AplicacionesVersiones_AplicacionId foreign key (AplicacionId) references dbo.Aplicaciones (AplicacionId)
)
go

create nonclustered index IX_AplicacionesVersiones_AplicacionId on dbo.AplicacionesVersiones (AplicacionId)
go