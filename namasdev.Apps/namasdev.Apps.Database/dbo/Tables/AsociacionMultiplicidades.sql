create table dbo.AsociacionMultiplicidades
(
	AsociacionMultiplicidadId smallint not null,
	Nombre nvarchar(50) not null,

	constraint PK_AsociacionMultiplicidades primary key clustered (AsociacionMultiplicidadId)
)
go

CREATE UNIQUE NONCLUSTERED INDEX [IX_AsociacionMultiplicidades_Nombre] ON [dbo].AsociacionMultiplicidades([Nombre] ASC)
go
