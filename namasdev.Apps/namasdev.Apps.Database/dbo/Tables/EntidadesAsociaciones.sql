create table dbo.EntidadesAsociaciones
(
	EntidadAsociacionId uniqueidentifier not null,
	EntidadPrincipalId uniqueidentifier not null,
	EntidadPrincipalAsociacionMultiplicidadId smallint not null,
	EntidadDependienteId uniqueidentifier not null,
	EntidadDependienteAsociacionMultiplicidadId smallint not null,
	TablaAuxiliarNombre nvarchar(100) null,
	DeleteAsociacionReglaId smallint not null,
	UpdateAsociacionReglaId smallint not null,
	
	constraint PK_EntidadesAsociaciones primary key clustered (EntidadAsociacionId),
	constraint FK_EntidadesAsociaciones_EntidadPrincipalId foreign key (EntidadPrincipalId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesAsociaciones_EntidadPrincipalAsociacionMultiplicidadId foreign key (EntidadPrincipalAsociacionMultiplicidadId) references dbo.AsociacionMultiplicidades (AsociacionMultiplicidadId),
	constraint FK_EntidadesAsociaciones_EntidadDependienteId foreign key (EntidadDependienteId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesAsociaciones_EntidadDependienteAsociacionMultiplicidadId foreign key (EntidadDependienteAsociacionMultiplicidadId) references dbo.AsociacionMultiplicidades (AsociacionMultiplicidadId),
	constraint FK_EntidadesAsociaciones_DeleteAsociacionReglaId foreign key (DeleteAsociacionReglaId) references dbo.AsociacionReglas (AsociacionReglaId),
	constraint FK_EntidadesAsociaciones_UpdateAsociacionReglaId foreign key (UpdateAsociacionReglaId) references dbo.AsociacionReglas (AsociacionReglaId),
	constraint CK_EntidadesAsociaciones_TablaAuxiliarNombre check ((EntidadPrincipalAsociacionMultiplicidadId = 3 and EntidadDependienteAsociacionMultiplicidadId = 3 and TablaAuxiliarNombre is not null) or ((EntidadPrincipalAsociacionMultiplicidadId <> 3 or EntidadDependienteAsociacionMultiplicidadId <> 3) and TablaAuxiliarNombre is null))
)
go

create nonclustered index IX_EntidadesAsociaciones_EntidadPrincipalId on dbo.EntidadesAsociaciones (EntidadPrincipalId)
go
