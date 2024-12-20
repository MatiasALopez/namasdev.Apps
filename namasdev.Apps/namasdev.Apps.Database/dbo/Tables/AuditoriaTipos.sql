CREATE TABLE [dbo].[AuditoriaTipos] 
(
    [AuditoriaTipoId] SMALLINT      NOT NULL,
    [Nombre]          NVARCHAR (50) NOT NULL,
    
    CONSTRAINT [PK_AuditoriaTipos] PRIMARY KEY CLUSTERED ([AuditoriaTipoId] ASC)
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_AuditoriaTipos_Nombre] ON [dbo].[AuditoriaTipos]([Nombre] ASC)
go
