create table dbo.IdiomasEntidadPropiedadesMetadata
(
    IdiomaId nchar(2) not null,
    CreadoPorNombre nvarchar(100) not null,
    CreadoPorEtiqueta nvarchar(100) not null,
    CreadoFechaNombre nvarchar(100) not null,
    CreadoFechaEtiqueta nvarchar(100) not null,
    UltimaModificacionPorNombre nvarchar(100) not null,
    UltimaModificacionPorEtiqueta nvarchar(100) not null,
    UltimaModificacionFechaNombre nvarchar(100) not null,
    UltimaModificacionFechaEtiqueta nvarchar(100) not null,
    BorradoPorNombre nvarchar(100) not null,
    BorradoPorEtiqueta nvarchar(100) not null,
    BorradoFechaNombre nvarchar(100) not null,
    BorradoFechaEtiqueta nvarchar(100) not null,
    BorradoNombre nvarchar(100) not null,
    BorradoEtiqueta nvarchar(100) not null,
    
    constraint PK_IdiomasEntidadPropiedadesMetadata primary key clustered (IdiomaId),
    constraint FK_IdiomasEntidadPropiedadesMetadata_IdiomaId foreign key (IdiomaId) references dbo.Idiomas (IdiomaId) on update cascade on delete cascade
)
go
