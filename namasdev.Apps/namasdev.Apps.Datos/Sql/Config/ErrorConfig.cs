﻿using System.Data.Entity.ModelConfiguration;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class ErrorConfig : EntityTypeConfiguration<Error>
    {
        public ErrorConfig()
        {
            ToTable(Entidades.Metadata.ErrorMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.ErrorMetadata.BD.ID);

            Property(e => e.Mensaje)
                .IsRequired();

            Property(e => e.StackTrace)
                .IsRequired();

            Property(e => e.Source)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.ErrorMetadata.Propiedades.Source.TAMAÑO_MAX);

            Property(e => e.UserId)
                .HasMaxLength(128);
        }
    }
}
