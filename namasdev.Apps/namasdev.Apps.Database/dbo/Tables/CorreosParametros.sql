create table [dbo].[CorreosParametros]
(
	[Id] smallint not null,
	[Asunto] nvarchar(256) not null,
	[Contenido] nvarchar(max) not null,
	[CopiaOculta] nvarchar(100) null, 

    constraint [PK_CorreosParametros] primary key clustered ([Id])
)
go
