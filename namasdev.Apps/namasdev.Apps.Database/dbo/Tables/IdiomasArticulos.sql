CREATE TABLE [dbo].[IdiomasArticulos] 
(
    [IdiomaArticuloId] nchar(3) NOT NULL,
    [IdiomaId] nchar(2) NOT NULL,
    [Nombre] NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_IdiomasArticulos] PRIMARY KEY CLUSTERED ([IdiomaArticuloId] ASC),
    constraint [FK_IdiomasArticulos_IdiomaId] foreign key (IdiomaId) references dbo.Idiomas (IdiomaId) on update cascade on delete cascade
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_IdiomasArticulos_Nombre] ON [dbo].IdiomasArticulos ([Nombre] ASC)
go

CREATE NONCLUSTERED INDEX [IX_IdiomasArticulos_IdiomaId] ON [dbo].IdiomasArticulos (IdiomaId ASC)
go
