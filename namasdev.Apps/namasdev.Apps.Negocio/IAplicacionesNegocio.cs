using System;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Negocio
{
    public interface IAplicacionesNegocio
    {
        Aplicacion Agregar(string nombre, string usuarioId);
        void Actualizar(Aplicacion aplicacion, string usuarioId);
        void MarcarComoBorrado(Guid aplicacionId, string usuarioLogueadoId);
        void DesmarcarComoBorrado(Guid aplicacionId);
    }
}
