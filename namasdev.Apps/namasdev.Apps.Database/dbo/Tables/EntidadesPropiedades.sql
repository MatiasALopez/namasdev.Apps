create table dbo.EntidadesPropiedades
(
    EntidadPropiedadId uniqueidentifier not null,
    EntidadId uniqueidentifier not null,
    Nombre nvarchar(100) not null,
    Etiqueta nvarchar(100) not null,
    PropiedadTipoId smallint not null,
    PropiedadTipoEspecificaciones nvarchar(max) null,
    PermiteNull bit not null,
    Orden smallint not null,
    CalculadaFormula nvarchar(max) null,
    GeneradaAlCrear bit not null,
    Editable bit not null,
    PropiedadCategoriaId smallint not null,

    constraint PK_EntidadesPropiedades primary key clustered (EntidadPropiedadId),
    constraint FK_EntidadesPropiedades_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId) on update cascade on delete cascade,
    constraint FK_EntidadesPropiedades_PropiedadTipoId foreign key (PropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId),
    constraint FK_EntidadesPropiedades_PropiedadCategoriaId foreign key (PropiedadCategoriaId) references dbo.PropiedadCategorias (PropiedadCategoriaId)
)
go

create unique nonclustered index UX_EntidadesPropiedades_EntidadIdYNombre on dbo.EntidadesPropiedades (EntidadId,Nombre)
go

create nonclustered index IX_EntidadesPropiedades_EntidadId on dbo.EntidadesPropiedades (EntidadId)
go
