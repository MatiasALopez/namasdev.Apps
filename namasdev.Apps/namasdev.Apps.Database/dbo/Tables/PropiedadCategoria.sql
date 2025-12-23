create table dbo.PropiedadCategorias
(
	PropiedadCategoriaId smallint not null,
	Nombre nvarchar(50) not null,
	
	constraint PK_PropiedadCategorias primary key clustered (PropiedadCategoriaId)
)
go

CREATE UNIQUE NONCLUSTERED INDEX [IX_PropiedadCategorias_Nombre] ON [dbo].PropiedadCategorias ([Nombre] ASC)
go
