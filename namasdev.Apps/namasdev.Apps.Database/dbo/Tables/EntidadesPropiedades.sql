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
	CalculadaFormula nvarchar(2000) null,
	GeneradaAlCrear bit not null,
	Editable bit not null,

	constraint PK_EntidadesPropiedades primary key clustered (EntidadPropiedadId),
	constraint FK_EntidadesPropiedades_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId) on delete cascade on update cascade,
	constraint FK_EntidadesPropiedades_PropiedadTipoId foreign key (PropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId)
)
go

create nonclustered index IX_EntidadesPropiedades_EntidadId on dbo.EntidadesPropiedades (EntidadId)
go

create unique nonclustered index IX_EntidadesPropiedades_EntidadIdYNombre on dbo.EntidadesPropiedades (EntidadId,Nombre)
go