create table dbo.PropiedadTipos
(
	PropiedadTipoId smallint not null,
	Nombre nvarchar(50) not null,
	CLRType nvarchar(50) not null,
	TSQLType nvarchar(50) not null,
	
	constraint PK_PropiedadTipos primary key clustered (PropiedadTipoId)
)
go
