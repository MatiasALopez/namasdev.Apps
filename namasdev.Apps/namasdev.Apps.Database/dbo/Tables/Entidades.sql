create table dbo.Entidades
(
	EntidadId uniqueidentifier not null,
	AplicacionVersionId uniqueidentifier not null,
	Nombre nvarchar(100) not null,
	CreadoPor nvarchar(128) not null,
	CreadoFecha datetime not null,
	UltimaModificacionPor nvarchar(128) not null,
	UltimaModificacionFecha datetime not null,
	BorradoPor nvarchar(128) null,
	BorradoFecha datetime null,
	Borrado AS (ISNULL(CONVERT(bit,CASE WHEN BorradoFecha IS NULL THEN 0 ELSE 1 END), 0)),

	constraint PK_Entidades primary key clustered (EntidadId),
	constraint FK_Entidades_AplicacionVersionId foreign key (AplicacionVersionId) references dbo.AplicacionesVersiones (AplicacionVersionId)
)
go