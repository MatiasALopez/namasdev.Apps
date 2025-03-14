﻿using namasdev.Apps.Entidades;
using namasdev.Data.Entity.Config;

namespace namasdev.Apps.Datos.Sql.Config
{
    public class EntidadConfig : EntidadBorradoConfigBase<Entidad>
    {
        public EntidadConfig()
        {
            ToTable(Entidades.Metadata.EntidadMetadata.BD.TABLA);
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName(Entidades.Metadata.EntidadMetadata.BD.ID);

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadMetadata.Propiedades.Nombre.TAMAÑO_MAX);

            Property(p => p.NombrePlural)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadMetadata.Propiedades.NombrePlural.TAMAÑO_MAX);

            Property(p => p.Etiqueta)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadMetadata.Propiedades.Etiqueta.TAMAÑO_MAX);

            Property(p => p.EtiquetaPlural)
                .IsRequired()
                .HasMaxLength(Entidades.Metadata.EntidadMetadata.Propiedades.EtiquetaPlural.TAMAÑO_MAX);

            HasRequired(p => p.AplicacionVersion)
                .WithMany(p => p.Entidades)
                .HasForeignKey(p => p.AplicacionVersionId);

            HasRequired(p => p.PropiedadesDefault)
                .WithRequiredDependent(p => p.Entidad);
        }
    }
}
