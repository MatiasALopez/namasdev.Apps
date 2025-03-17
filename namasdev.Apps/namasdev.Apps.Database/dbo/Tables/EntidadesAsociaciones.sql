create table dbo.EntidadesAsociaciones
(
	EntidadAsociacionId uniqueidentifier not null,
	OrigenEntidadId uniqueidentifier not null,
	OrigenEntidadPropiedadId uniqueidentifier not null,
	OrigenAsociacionMultiplicidadId smallint not null,
	DestinoEntidadId uniqueidentifier not null,
	DestinoEntidadPropiedadId uniqueidentifier not null,
	DestinoAsociacionMultiplicidadId smallint not null,
	TablaAuxiliarNombre nvarchar(100) null,
	DeleteAsociacionReglaId smallint not null,
	UpdateAsociacionReglaId smallint not null,
	
	constraint PK_EntidadesAsociaciones primary key clustered (EntidadAsociacionId),
	constraint FK_EntidadesAsociaciones_OrigenEntidadId foreign key (OrigenEntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesAsociaciones_OrigenEntidadPropiedadId foreign key (OrigenEntidadPropiedadId) references dbo.EntidadesPropiedades (EntidadPropiedadId),
	constraint FK_EntidadesAsociaciones_OrigenAsociacionMultiplicidadId foreign key (OrigenAsociacionMultiplicidadId) references dbo.AsociacionMultiplicidades (AsociacionMultiplicidadId),
	constraint FK_EntidadesAsociaciones_DestinoEntidadId foreign key (DestinoEntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesAsociaciones_DestinoEntidadPropiedadId foreign key (DestinoEntidadPropiedadId) references dbo.EntidadesPropiedades (EntidadPropiedadId),
	constraint FK_EntidadesAsociaciones_DestinoAsociacionMultiplicidadId foreign key (DestinoAsociacionMultiplicidadId) references dbo.AsociacionMultiplicidades (AsociacionMultiplicidadId),
	constraint FK_EntidadesAsociaciones_DeleteAsociacionReglaId foreign key (DeleteAsociacionReglaId) references dbo.AsociacionReglas (AsociacionReglaId),
	constraint FK_EntidadesAsociaciones_UpdateAsociacionReglaId foreign key (UpdateAsociacionReglaId) references dbo.AsociacionReglas (AsociacionReglaId),
	constraint CK_EntidadesAsociaciones_TablaAuxiliarNombre check ((OrigenAsociacionMultiplicidadId = 3 and DestinoAsociacionMultiplicidadId = 3 and TablaAuxiliarNombre is not null) or ((OrigenAsociacionMultiplicidadId <> 3 or DestinoAsociacionMultiplicidadId <> 3) and TablaAuxiliarNombre is null))
)
go

create nonclustered index IX_EntidadesAsociaciones_OrigenEntidadId on dbo.EntidadesAsociaciones (OrigenEntidadId)
go
