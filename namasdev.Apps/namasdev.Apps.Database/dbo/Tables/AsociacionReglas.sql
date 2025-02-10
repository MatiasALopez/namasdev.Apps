create table dbo.AsociacionReglas
(
	AsociacionReglaId smallint not null,
	Nombre nvarchar(50) not null,

	constraint PK_AsociacionReglas primary key clustered (AsociacionReglaId)
)
go

CREATE UNIQUE NONCLUSTERED INDEX [IX_AsociacionReglas_Nombre] ON [dbo].AsociacionReglas([Nombre] ASC)
go
