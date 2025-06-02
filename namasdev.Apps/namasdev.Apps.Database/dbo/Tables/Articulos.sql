CREATE TABLE [dbo].[Articulos] 
(
    [ArticuloId] SMALLINT NOT NULL,
    [Nombre] NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED ([ArticuloId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Articulos_Nombre] ON [dbo].[Articulos]([Nombre] ASC)
go
