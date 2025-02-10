create table dbo.EntidadesIndicesPropiedades
(
	EntidadIndicePropiedadId uniqueidentifier not null,
	EntidadIndiceId uniqueidentifier not null,
	EntidadPropiedadId uniqueidentifier not null,
	
	constraint PK_EntidadesIndicesPropiedades primary key clustered (EntidadIndiceId),
	constraint FK_EntidadesIndicesPropiedades_EntidadIndiceId foreign key (EntidadIndiceId) references dbo.EntidadesIndices (EntidadIndiceId) on delete cascade on update cascade,
	constraint FK_EntidadesIndicesPropiedades_EntidadPropiedadId foreign key (EntidadPropiedadId) references dbo.EntidadesPropiedades (EntidadPropiedadId)
)
go

create nonclustered index IX_EntidadesIndicesPropiedades_EntidadIndiceId on dbo.EntidadesIndicesPropiedades (EntidadIndiceId)
go

create unique nonclustered index IX_EntidadesIndicesPropiedades_EntidadIndiceIdYEntidadPropiedadId on dbo.EntidadesIndicesPropiedades (EntidadIndiceId,EntidadPropiedadId)
go
