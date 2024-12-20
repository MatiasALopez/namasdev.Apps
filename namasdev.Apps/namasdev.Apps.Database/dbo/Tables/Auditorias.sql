CREATE TABLE [dbo].[Auditorias] 
(
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Tabla]           NVARCHAR (100)   NOT NULL,
    [AuditoriaTipoId] SMALLINT         NOT NULL,
    [Detalle]         NVARCHAR (MAX)   NOT NULL,
    [UserId]          NVARCHAR (128)   NOT NULL,
    [FechaHora]       DATETIME         NOT NULL,
    
    CONSTRAINT [PK_Auditorias] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Auditorias_AuditoriaTipoId] FOREIGN KEY ([AuditoriaTipoId]) REFERENCES [dbo].[AuditoriaTipos] ([AuditoriaTipoId])
)
go
