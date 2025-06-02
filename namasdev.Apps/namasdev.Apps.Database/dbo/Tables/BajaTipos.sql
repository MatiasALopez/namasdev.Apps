CREATE TABLE [dbo].[BajaTipos] 
(
    [BajaTipoId] SMALLINT      NOT NULL,
    [Nombre]          NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_BajaTipos] PRIMARY KEY CLUSTERED ([BajaTipoId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_BajaTipos_Nombre] ON [dbo].[BajaTipos]([Nombre] ASC)
go
