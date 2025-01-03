create table dbo.Entidades
(
	EntidadId uniqueidentifier not null,
	AplicacionVersionId uniqueidentifier not null,
	Nombre nvarchar(100) not null,
	NombrePlural nvarchar(120) not null,
	Etiqueta nvarchar(120) not null,
	EtiquetaPlural nvarchar(140) not null,
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

create unique nonclustered index IX_Entidades_AplicacionVersionIdYNombre on dbo.Entidades(AplicacionVersionId,Nombre)
go

create nonclustered index IX_Entidades_AplicacionVersionId on dbo.Entidades (AplicacionVersionId)
go
