--====
CREATE TABLE [dbo].[AspNetUsers] 
(
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetUsers_UserName] ON [dbo].[AspNetUsers]([UserName] ASC);
go
--====


--====
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
go

CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetRoles_Name] ON [dbo].[AspNetRoles]([Name] ASC);
GO
--====


--====
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]([UserId] ASC);
GO
--====


--====
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]([UserId] ASC);
GO
--====


--====
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]([RoleId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_UserId] ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO
--====


--====
CREATE TABLE [dbo].[Parametros]
(
	[Nombre] nvarchar(100) not null,
	[Valor] nvarchar(max) null,

	constraint [PK_Parametros] primary key clustered ([Nombre])
)
go
--====


--====
create table [dbo].[CorreosParametros]
(
	[Id] smallint not null,
	[Asunto] nvarchar(256) not null,
	[Contenido] nvarchar(max) not null,
	[CopiaOculta] nvarchar(max) null, 

    constraint [PK_CorreosParametros] primary key clustered ([Id])
)
go
--====

--===
CREATE TABLE [dbo].[Errores]
(
	[Id] uniqueidentifier NOT NULL,
	[Mensaje] nvarchar(max) NOT NULL,
	[StackTrace] nvarchar(max) NOT NULL,
	[Source] nvarchar(200) NOT NULL,
	[Argumentos] nvarchar(max) NULL,
	[FechaHora] datetime NOT NULL,
	[UserId] nvarchar(128) NULL,
 
	CONSTRAINT [PK_Errores] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
--===

--===
CREATE TABLE [dbo].[AuditoriaTipos] 
(
    [AuditoriaTipoId] SMALLINT      NOT NULL,
    [Nombre]          NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_AuditoriaTipos] PRIMARY KEY CLUSTERED ([AuditoriaTipoId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_AuditoriaTipos_Nombre] ON [dbo].[AuditoriaTipos]([Nombre] ASC)
go
--===

--===
CREATE TABLE [dbo].[Auditorias] 
(
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Tabla]           NVARCHAR (100)   NOT NULL,
    [AuditoriaTipoId] SMALLINT         NOT NULL,
    [Detalle]         NVARCHAR (MAX)   NOT NULL,
    [UserId]          NVARCHAR (128)   NOT NULL,
    [FechaHora]       DATETIME         NOT NULL,
    
    CONSTRAINT [PK_Auditorias] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Auditorias_AuditoriaTipoId] FOREIGN KEY ([AuditoriaTipoId]) REFERENCES [dbo].[AuditoriaTipos] ([AuditoriaTipoId])
)
go
--===

--===
create table [dbo].[Usuarios]
(
	[UsuarioId] nvarchar(128) NOT NULL,
	[Email] nvarchar(256) NOT NULL,
	[Nombres] nvarchar(100) NOT NULL,
	[Apellidos] nvarchar(100) NOT NULL,
	[NombresYApellidos] AS CAST(concat([Nombres],' ',[Apellidos]) AS nvarchar(200)), 
	[ApellidosYNombres] AS CAST(concat([Apellidos],' ',[Nombres]) AS nvarchar(200)), 
	[CreadoPor] nvarchar(128) NOT NULL,
	[CreadoFecha] datetime NOT NULL,
	[UltimaModificacionPor] nvarchar(128) NOT NULL,
	[UltimaModificacionFecha] datetime NOT NULL,
	[BorradoPor] nvarchar(128) NULL,
	[BorradoFecha] datetime NULL,
	[Borrado] AS (ISNULL(CONVERT(bit,CASE WHEN [BorradoFecha] IS NULL THEN 0 ELSE 1 END), 0)),

	constraint [PK_Usuarios] primary key clustered ([UsuarioId]),
	constraint [FK_Usuarios_UsuarioId] foreign key ([UsuarioId]) references [dbo].[AspNetUsers] ([Id]) on delete cascade
)
go
--===

--===
create table dbo.PropiedadTipos
(
	PropiedadTipoId smallint not null,
	Nombre nvarchar(50) not null,
	CLRType nvarchar(50) not null,
	TSQLType nvarchar(50) not null,
	
	constraint PK_PropiedadTipos primary key clustered (PropiedadTipoId)
)
go
--===

--===
CREATE TABLE [dbo].[BajaTipos] 
(
    [BajaTipoId] SMALLINT NOT NULL,
    [Nombre] NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_BajaTipos] PRIMARY KEY CLUSTERED ([BajaTipoId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_BajaTipos_Nombre] ON [dbo].[BajaTipos]([Nombre] ASC)
go
--===

--===
CREATE TABLE [dbo].[Articulos] 
(
    [ArticuloId] SMALLINT NOT NULL,
    [Nombre] NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED ([ArticuloId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Articulos_Nombre] ON [dbo].[Articulos]([Nombre] ASC)
go
--===

--===
create table dbo.Aplicaciones
(
	AplicacionId uniqueidentifier not null,
	Nombre nvarchar(100) not null,
	CreadoPor nvarchar(128) not null,
	CreadoFecha datetime not null,
	UltimaModificacionPor nvarchar(128) not null,
	UltimaModificacionFecha datetime not null,
	BorradoPor nvarchar(128) null,
	BorradoFecha datetime null,
	Borrado AS (ISNULL(CONVERT(bit,CASE WHEN BorradoFecha IS NULL THEN 0 ELSE 1 END), 0)),

	constraint PK_Aplicaciones primary key clustered (AplicacionId)
)
go
--===

--===
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
--===

--===
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
--===

--===
create table dbo.EntidadesEspecificaciones
(
	EntidadId uniqueidentifier not null,
	ArticuloId smallint not null,
	IDPropiedadTipoId smallint null,
	EsSoloLectura bit not null,
	BajaTipoId smallint not null,
	AuditoriaCreado bit not null,
	AuditoriaUltimaModificacion bit not null,

	constraint PK_EntidadesEspecificaciones primary key clustered (EntidadId),
	constraint FK_EntidadesEspecificaciones_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesEspecificaciones_ArticuloId foreign key (ArticuloId) references dbo.Articulos (ArticuloId),
	constraint FK_EntidadesEspecificaciones_IDPropiedadTipoId foreign key (IDPropiedadTipoId) references dbo.PropiedadTipos (PropiedadTipoId),
	constraint FK_EntidadesEspecificaciones_BajaTipoId foreign key (BajaTipoId) references dbo.BajaTipos (BajaTipoId),
	constraint CK_EntidadesEspecificaciones_EsSoloLecturaYBajaTipoYAuditorias check ((EsSoloLectura = 0) or (EsSoloLectura = 1 and BajaTipoId = 3 and AuditoriaCreado = 0 and AuditoriaUltimaModificacion = 0))
)
go
--===

--===
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
--===

--===
create table dbo.AsociacionMultiplicidades
(
	AsociacionMultiplicidadId smallint not null,
	Nombre nvarchar(50) not null,

	constraint PK_AsociacionMultiplicidades primary key clustered (AsociacionMultiplicidadId)
)
go

CREATE UNIQUE NONCLUSTERED INDEX [IX_AsociacionMultiplicidades_Nombre] ON [dbo].AsociacionMultiplicidades([Nombre] ASC)
go
--===

--===
create table dbo.AsociacionReglas
(
	AsociacionReglaId smallint not null,
	Nombre nvarchar(50) not null,

	constraint PK_AsociacionReglas primary key clustered (AsociacionReglaId)
)
go

CREATE UNIQUE NONCLUSTERED INDEX [IX_AsociacionReglas_Nombre] ON [dbo].AsociacionReglas([Nombre] ASC)
go
--===

--===
create table dbo.EntidadesClaves
(
	EntidadClaveId uniqueidentifier not null,
	EntidadId uniqueidentifier not null,
	EntidadPropiedadId uniqueidentifier not null,
	
	constraint PK_EntidadesClaves primary key clustered (EntidadClaveId),
	constraint FK_EntidadesClaves_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId),
	constraint FK_EntidadesClaves_EntidadPropiedadId foreign key (EntidadPropiedadId) references dbo.EntidadesPropiedades (EntidadPropiedadId)
)
go

create nonclustered index IX_EntidadesClaves_EntidadId on dbo.EntidadesClaves (EntidadId)
go

create unique nonclustered index IX_EntidadesClaves_EntidadIdYEntidadPropiedadId on dbo.EntidadesClaves (EntidadId,EntidadPropiedadId)
go
--===

--===
create table dbo.EntidadesAsociaciones
(
	EntidadAsociacionId uniqueidentifier not null,
	Nombre nvarchar(200) not null,
	OrigenEntidadId uniqueidentifier not null,
	OrigenEntidadPropiedadId uniqueidentifier not null,
	OrigenEntidadPropiedadNavegacionNombre nvarchar(100) null,
	OrigenAsociacionMultiplicidadId smallint not null,
	DestinoEntidadId uniqueidentifier not null,
	DestinoEntidadPropiedadId uniqueidentifier not null,
	DestinoEntidadPropiedadNavegacionNombre nvarchar(100) null,
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
--===

--===
create table dbo.EntidadesIndices
(
	EntidadIndiceId uniqueidentifier not null,
	EntidadId uniqueidentifier not null,
	Nombre nvarchar(200) not null,
	EsUnique bit not null,
	
	constraint PK_EntidadesIndices primary key clustered (EntidadIndiceId),
	constraint FK_EntidadesIndices_EntidadId foreign key (EntidadId) references dbo.Entidades (EntidadId)
)
go

create nonclustered index IX_EntidadesIndices_EntidadId on dbo.EntidadesClaves (EntidadId)
go

create unique nonclustered index IX_EntidadesIndices_EntidadIdYNombre on dbo.EntidadesIndices (EntidadId,Nombre)
go
--===

--===
create table dbo.EntidadesIndicesPropiedades
(
	EntidadIndicePropiedadId uniqueidentifier not null,
	EntidadIndiceId uniqueidentifier not null,
	EntidadPropiedadId uniqueidentifier not null,
	
	constraint PK_EntidadesIndicesPropiedades primary key clustered (EntidadIndicePropiedadId),
	constraint FK_EntidadesIndicesPropiedades_EntidadIndiceId foreign key (EntidadIndiceId) references dbo.EntidadesIndices (EntidadIndiceId) on delete cascade on update cascade,
	constraint FK_EntidadesIndicesPropiedades_EntidadPropiedadId foreign key (EntidadPropiedadId) references dbo.EntidadesPropiedades (EntidadPropiedadId)
)
go

create nonclustered index IX_EntidadesIndicesPropiedades_EntidadIndiceId on dbo.EntidadesIndicesPropiedades (EntidadIndiceId)
go

create unique nonclustered index IX_EntidadesIndicesPropiedades_EntidadIndiceIdYEntidadPropiedadId on dbo.EntidadesIndicesPropiedades (EntidadIndiceId,EntidadPropiedadId)
go
--===

--===
go
CREATE PROCEDURE [dbo].[uspClonarAplicacionVersion]
	@AplicacionVersionIdOrigen uniqueidentifier,
	@AplicacionVersionIdDestino uniqueidentifier,
	@UsuarioId nvarchar(128),
	@FechaHora datetime
AS
BEGIN
	insert into dbo.Entidades (EntidadId,AplicacionVersionId,Nombre,NombrePlural,Etiqueta,EtiquetaPlural,CreadoPor,CreadoFecha,UltimaModificacionPor,UltimaModificacionFecha)
		select newid(),@AplicacionVersionIdDestino,Nombre,NombrePlural,Etiqueta,EtiquetaPlural,@UsuarioId,@FechaHora,@UsuarioId,@FechaHora
		from dbo.Entidades 
		where AplicacionVersionId = @AplicacionVersionIdOrigen

	insert into dbo.EntidadesPropiedades (EntidadPropiedadId,EntidadId,Nombre,Etiqueta,PropiedadTipoId,PropiedadTipoEspecificaciones,PermiteNull,Orden,CalculadaFormula,GeneradaAlCrear,Editable,CreadoPor,CreadoFecha,UltimaModificacionPor,UltimaModificacionFecha)
		select 
			newid(),ed.EntidadId,eop.Nombre,eop.Etiqueta,eop.PropiedadTipoId,eop.PropiedadTipoEspecificaciones,eop.PermiteNull,eop.Orden,eop.CalculadaFormula,eop.GeneradaAlCrear,eop.Editable,@UsuarioId,@FechaHora,@UsuarioId,@FechaHora
		from 
			dbo.Entidades eo 
			join dbo.Entidades ed on eo.Nombre = ed.Nombre
			join dbo.EntidadesPropiedades eop on eo.EntidadId = eop.EntidadId
		where 
			eo.AplicacionVersionId = @AplicacionVersionIdOrigen
			and ed.AplicacionVersionId = @AplicacionVersionIdDestino
	
	return 0
END
go
--===
