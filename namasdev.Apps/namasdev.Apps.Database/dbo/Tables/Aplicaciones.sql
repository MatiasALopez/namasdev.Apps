create table dbo.Aplicaciones
(
	AplicacionId uniqueidentifier not null,
	Nombre nvarchar(100) not null,
	IdiomaId nchar(2) not null,
    CreadoPor nvarchar(128) not null,
	CreadoFecha datetime not null,
	UltimaModificacionPor nvarchar(128) not null,
	UltimaModificacionFecha datetime not null,
	BorradoPor nvarchar(128) null,
	BorradoFecha datetime null,
	Borrado AS (ISNULL(CONVERT(bit,CASE WHEN BorradoFecha IS NULL THEN 0 ELSE 1 END), 0)),

	constraint PK_Aplicaciones primary key clustered (AplicacionId),
	constraint FK_Aplicaciones_IdiomaId foreign key (IdiomaId) references dbo.Idiomas (IdiomaId)
)
go
