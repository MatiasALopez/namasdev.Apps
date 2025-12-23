CREATE TABLE [dbo].[Idiomas] 
(
    [IdiomaId] nchar(2) NOT NULL,
    [Nombre] nvarchar(50) NOT NULL,
    [UsaArticulos] bit NOT NULL,
    
    CONSTRAINT [PK_Idiomas] PRIMARY KEY CLUSTERED ([IdiomaId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Idiomas_Nombre] ON [dbo].Idiomas ([Nombre] ASC)
go
