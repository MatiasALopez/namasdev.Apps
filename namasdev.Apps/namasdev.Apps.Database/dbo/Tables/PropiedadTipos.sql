create table dbo.PropiedadTipos
(
	PropiedadTipoId smallint not null,
	Nombre nvarchar(100) not null,
	
	constraint PK_PropiedadTipos primary key clustered (PropiedadTipoId)
)
go
